using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server.DataClasses;
using UnityEngine;

namespace NewUnityProject.Network.Server.Requests
{
	//
	public class GameResults : MonoBehaviour 
	{
		// 
		[SerializeField] private HandlerPoolBase _handlerPool;
		//
		[SerializeField] private RequestResult<GameResultsData> _requestResult;		
		//
		[SerializeField] private string _action = "Results";



        /// <summary>Разрешить зависимости.</summary>
        public void ResolveDependencies(HandlerPoolBase handlerPool)
        {
			_handlerPool = handlerPool;
        }



		// 
		public void ExecuteRequestAsync(RequestHeaders headers)
		{
			StartCoroutine(ExecuteRequest(headers));
		}
		private IEnumerator ExecuteRequest(RequestHeaders headers)
		{
			HandlerItemReady handlerReady = ServerClient.ExecuteGetRequest(_handlerPool, headers, _action);

			// 
			yield return StartCoroutine(handlerReady.Handler.WaitUntilComplete());
			if (handlerReady.Handler.IsComplete())
			{
				_requestResult = ServerClient.ReceiveRequestResult<GameResultsData>(handlerReady);
				LoadingCompleteCall();
			}
		}





        #region events


        // 
        public delegate void LoadingCompleteDelegate(RequestResult<GameResultsData> requestResult);

        //
        private event LoadingCompleteDelegate LoadingCompleteEvent;

		// 		
		public void SetLoadingComplete(LoadingCompleteDelegate loadCallback)
		{
			LoadingCompleteEvent = loadCallback;
		}
        //
        private void LoadingCompleteCall()
        {
            if (LoadingCompleteEvent != null)
                LoadingCompleteEvent(_requestResult);
        }


        #endregion   

	}
}
