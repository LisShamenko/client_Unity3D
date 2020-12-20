using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server.DataClasses
{
	//
	[Serializable]
	public class UserTokenData
	{
		public string UserId;
		public string Token;
	}
}
