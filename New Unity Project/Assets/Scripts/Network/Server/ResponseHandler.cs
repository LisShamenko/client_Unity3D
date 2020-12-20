using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server
{
	//
	public class ResponseHandler
	{

		#region internal types

		// 
		private enum HandlerState
		{
			None,
			Loading,
			Complete,
			Stop,
			Unauthorized,
			Timeout,
		}

		#endregion 





		// 
		public String Data { set; get; }
		// 
		public Exception Exception { get; set; }
		// 
		private HandlerState _handlerState;



		// 
		public ResponseHandler()
		{
			_handlerState = HandlerState.None;
		}

		

		// 
		public void Start() { _handlerState = HandlerState.Loading; }
		// 
		public void SetUnauthorizedState() { _handlerState = HandlerState.Unauthorized; }
		// 
		public void Complete() { _handlerState = HandlerState.Complete; }		
		// 
		public void Stop() { _handlerState = HandlerState.Stop; }



		// 
        public IEnumerator WaitUntilComplete(float timeout = 10000f)
        {
            DateTime endTime = DateTime.UtcNow + TimeSpan.FromMilliseconds((double)timeout);

			Debug.Log("--- wait until complete --- utc now = " + DateTime.UtcNow + " --- end time = " + endTime);
			Debug.Log("--- before --- _handlerState = " + _handlerState);

            while (_handlerState == HandlerState.Loading && endTime > DateTime.UtcNow)
            {
                yield return (object)null;
            }

			Debug.Log("--- after --- END --- _handlerState = " + _handlerState);

			// 
			if (_handlerState == HandlerState.Loading) 
				_handlerState = HandlerState.Timeout;

			Debug.Log("--- END --- _handlerState = " + _handlerState);
        }

		/// <summary>true - запрос данных завершился успешно.</summary>
		public bool IsComplete() { return (_handlerState == HandlerState.Complete); }

	}
}