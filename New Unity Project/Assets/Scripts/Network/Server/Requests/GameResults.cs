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
		[SerializeField] private GameResultsData _data;
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
				_data = _requestResult.Result;
				LoadingCompleteCall();
			}
		}





        #region events


        // 
        public delegate void LoadingCompleteDelegate(GameResultsData data);

		// 		
		public void SetLoadingComplete(LoadingCompleteDelegate loadCallback)
		{
			LoadingCompleteEvent = loadCallback;
		}

        //
        private event LoadingCompleteDelegate LoadingCompleteEvent;
        //
        private void LoadingCompleteCall()
        {
            if (LoadingCompleteEvent != null)
                LoadingCompleteEvent(_data);
        }


        #endregion   

	}
}
