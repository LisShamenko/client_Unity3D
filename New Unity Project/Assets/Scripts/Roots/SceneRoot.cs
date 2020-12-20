using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server;
using NewUnityProject.Network.Server.DataClasses;
using UnityEngine;

namespace NewUnityProject.Roots
{
	// 
	public class SceneRoot : MonoBehaviour 
	{
		// 
		[SerializeField] private NetworkRoot _networkRoot;
		// 
		[SerializeField] private string _userId;
		// 
		[SerializeField] private string _token;

		// 
		private void Start() 
		{
			_networkRoot.ResolveDependencies(
				delegate(GameVersionData data) 
				{
					if (data == null) 
						Debug.Log("--- GameVersionData --- NULL ---");
					else 
						Debug.Log("--- GameVersionData --- Version = " + data.Version + " ---");
				}, 
				delegate(UserTokenData data) 
				{
					Debug.Log("--- data --- UserId = " + data.UserId + " --- Token = " + data.Token + " ---");
					_userId = data.UserId;
					_token = data.Token;
				},
				delegate(UserTokenData data) 
				{
					Debug.Log("--- data --- UserId = " + data.UserId + " --- Token = " + data.Token + " ---");
					_userId = data.UserId;
					_token = data.Token;
				},
				delegate(string data) 
				{
					Debug.Log("--- data --- data = " + data + " ---");
				},				
				delegate(GameResultsData data) 
				{
					foreach (UserResultData result in data.Results)
					{
						Debug.Log("--- data --- Name = " + result.Name + " --- Result = " + result.Result + " ---");
					}
				});
		}



		// 
		private void Update() 
		{
			if (Input.GetKeyDown(KeyCode.F2)) _networkRoot.GameVersionExecute();
			if (Input.GetKeyDown(KeyCode.F3)) _networkRoot.UserRegisterExecute(new AuthenticationData("111", "222"));
			if (Input.GetKeyDown(KeyCode.F4)) _networkRoot.UserLoginExecute(new AuthenticationData("111", "222"));
			if (Input.GetKeyDown(KeyCode.F5)) _networkRoot.UserResultExecute(new RequestHeaders(_userId, _token), new UserResultRequest(_userId, 8484));
			if (Input.GetKeyDown(KeyCode.F6)) _networkRoot.GameResultsExecute(new RequestHeaders(_userId, _token));
		}

	}
}