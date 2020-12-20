using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server
{
    // 
    public class HandlerPoolBase
    {
		// 
        private List<HandlerItemReady> _freeHandlers; 

        // 
        public HandlerPoolBase()
        {
			_freeHandlers = new List<HandlerItemReady>();
        }

        // 
        public bool PullHandler(out HandlerItemReady handlerReady)
        {
			if (_freeHandlers.Count > 0)
			{
				int i = _freeHandlers.Count - 1;
				handlerReady = _freeHandlers[i];
				_freeHandlers.RemoveAt(i);
				return true;
			}
			else
			{
				handlerReady = new HandlerItemReady(this);
				return true;
			}
        }
        // 
        public void PushHandler(HandlerItemReady handlerReady)
        {
			_freeHandlers.Add(handlerReady);
        }
    }
}
