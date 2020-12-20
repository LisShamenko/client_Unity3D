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
		[SerializeField] private GameVersionData _data;
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
				_data = _requestResult.Result;
				LoadingCompleteCall();
			}
		}





        #region events


        // 
        public delegate void LoadingCompleteDelegate(GameVersionData data);

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
