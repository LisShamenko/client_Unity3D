using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server.DataClasses
{
    //
    [Serializable]
    public class UserResultRequest
    {
        public string UserId;
		public int Data;

        // 
        public UserResultRequest(string id, int data)
        {
            UserId = id;
			Data = data;
        }
    }
}

