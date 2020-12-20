using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server.DataClasses;
using UnityEngine;

namespace NewUnityProject.Network.Server.Requests
{
	//
	public class UserLogin : MonoBehaviour 
	{
		// 
		[SerializeField] private HandlerPoolBase _handlerPool;
		//
		[SerializeField] private UserTokenData _data;
		//
		[SerializeField] private RequestResult<UserTokenData> _requestResult;
		//
		[SerializeField] private string _action = "Login";



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
				_data = _requestResult.Result;
				LoadingCompleteCall();
			}
		}





        #region events


        // 
        public delegate void LoadingCompleteDelegate(UserTokenData data);

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
