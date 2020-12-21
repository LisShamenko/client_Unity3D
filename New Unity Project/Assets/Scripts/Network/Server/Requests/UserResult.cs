using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server.DataClasses;
using UnityEngine;

namespace NewUnityProject.Network.Server.Requests
{
	//
	public class UserResult : MonoBehaviour 
	{
		// 
		[SerializeField] private HandlerPoolBase _handlerPool;
		//
		[SerializeField] private RequestResult<string> _requestResult;
		//
		[SerializeField] private string _action = "Result";



        /// <summary>Разрешить зависимости.</summary>
        public void ResolveDependencies(HandlerPoolBase handlerPool)
        {
			_handlerPool = handlerPool;
        }



		// 
		public void ExecuteRequestAsync(RequestHeaders headers, UserResultRequest data)
		{
			StartCoroutine(ExecuteRequest(headers, data));
		}
		private IEnumerator ExecuteRequest(RequestHeaders headers, UserResultRequest data)
		{
			HandlerItemReady handlerReady = ServerClient.
				ExecutePostRequest<UserResultRequest>(
					_handlerPool, _action, headers, data);

			// 
			yield return StartCoroutine(handlerReady.Handler.WaitUntilComplete());
			if (handlerReady.Handler.IsComplete())
			{
				_requestResult = ServerClient.ReceiveRequestResult<string>(handlerReady);
				LoadingCompleteCall();
			}
		}





        #region events


        // 
        public delegate void LoadingCompleteDelegate(RequestResult<string> requestResult);

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
