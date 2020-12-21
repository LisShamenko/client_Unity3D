using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server.DataClasses;
using UnityEngine;

namespace NewUnityProject.Network.Server.Requests
{
	//
	public class UserRegister : MonoBehaviour 
	{
		// 
		[SerializeField] private HandlerPoolBase _handlerPool;
		// 
		[SerializeField] private RequestResult<UserTokenData> _requestResult;		
		//
		[SerializeField] private string _action = "Register";



        /// <summary>Разрешить зависимости.</summary>
        public void ResolveDependencies(HandlerPoolBase handlerPool)
        {
			_handlerPool = handlerPool;
        }



		// 
		public void ExecuteRequestAsync(AuthenticationData data)
		{
			StartCoroutine(ExecuteRequest(data));
		}
		private IEnumerator ExecuteRequest(AuthenticationData data)
		{
			HandlerItemReady handlerReady = ServerClient.
				ExecutePostRequest<AuthenticationData>(
					_handlerPool, _action, data);

			// 
			yield return StartCoroutine(handlerReady.Handler.WaitUntilComplete());
			if (handlerReady.Handler.IsComplete())
			{
				_requestResult = ServerClient.ReceiveRequestResult<UserTokenData>(handlerReady);
				LoadingCompleteCall();
			}
		}





        #region events


        // 
        public delegate void LoadingCompleteDelegate(RequestResult<UserTokenData> requestResult);

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
