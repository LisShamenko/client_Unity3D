using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server
{
    // 
    public class HandlerItemReady
    {
		private HandlerPoolBase _handlerPool;
        public ResponseHandler Handler { get; private set; }

        //
        public HandlerItemReady(HandlerPoolBase handlerPool)
        {
			_handlerPool = handlerPool;
			Handler = new ResponseHandler();
        }

		// 
        public void PushBack()
        {
			_handlerPool.PushHandler(this);
        }
    }
}
