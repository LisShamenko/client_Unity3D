using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server.DataClasses;
using UnityEngine;

namespace NewUnityProject.Network.Server.Requests
{
	//
	public class GameVersion : MonoBehaviour 
	{
		// 
		[SerializeField] private HandlerPoolBase _handlerPool;
		//
		[SerializeField] private RequestResult<GameVersionData> _requestResult;
		//
		[SerializeField] private string _action = "GameVersion";



        /// <summary>Разрешить зависимости.</summary>
        public void ResolveDependencies(HandlerPoolBase handlerPool)
        {
			_handlerPool = handlerPool;
        }



		// 
		public void ExecuteRequestAsync()
		{
			StartCoroutine(ExecuteRequest());
		}
		private IEnumerator ExecuteRequest()
		{
			HandlerItemReady handlerReady = ServerClient.ExecuteGetRequest(_handlerPool, _action);
			yield return StartCoroutine(handlerReady.Handler.WaitUntilComplete());
			if (handlerReady.Handler.IsComplete())
			{
				_requestResult = ServerClient.ReceiveRequestResult<GameVersionData>(handlerReady);
				LoadingCompleteCall();
			}
		}





        #region events


        // 
        public delegate void LoadingCompleteDelegate(RequestResult<GameVersionData> requestResult);

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
