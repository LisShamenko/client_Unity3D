using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server
{
    //
    [Serializable]
    public class RequestHeaders
    {
        /// <summary>Идентификатор пользователя.</summary>
        public string UserId;
        /// <summary>Токен доступа.</summary>
        public string Token;

		//
		public RequestHeaders(string id, string token)
		{
			UserId = id;
			Token = token;
		}
    }   
}