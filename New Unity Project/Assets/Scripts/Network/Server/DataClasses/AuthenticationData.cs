using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server.DataClasses
{
    //
    [Serializable]
    public class AuthenticationData
    {
        public string Name;
		public string Password;

		public AuthenticationData(string name, string password) 
		{
			Name = name;
			Password = password;
		}
    }
}