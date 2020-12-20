using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server;
using NewUnityProject.Network.Server.DataClasses;
using NewUnityProject.Network.Server.Requests;
using UnityEngine;

namespace NewUnityProject.Roots
{
	public class NetworkRoot : MonoBehaviour 
	{
		// 
		[SerializeField] private HandlerPoolBase _handlerPool;
		// 
		[SerializeField] private GameVersion _gameVersion;
		// 
		[SerializeField] private UserRegister _userRegister;
		// 
		[SerializeField] private UserLogin _userLogin;
		// 
		[SerializeField] private UserResult _userResult;		
		// 
		[SerializeField] private GameResults _gameResults;

		// 
		[SerializeField] private bool _isGameVersionLoading = false;
		[SerializeField] private bool _isUserRegisterLoading = false;
		[SerializeField] private bool _isUserLoginLoading = false;
		[SerializeField] private bool _isUserResultLoading = false;
		[SerializeField] private bool _isGameResultsLoading = false;



		// 
		public void ResolveDependencies(
			GameVersion.LoadingCompleteDelegate gameVersionCallback, 
			UserRegister.LoadingCompleteDelegate userRegisterCallback, 
			UserLogin.LoadingCompleteDelegate userLoginCallback, 
			UserResult.LoadingCompleteDelegate userResultCallback, 
			GameResults.LoadingCompleteDelegate gameResultsCallback)
		{
			ServerClient.ServerURL = new Uri("http://127.0.0.1:3000/");

			// 
			_handlerPool = new HandlerPoolBase();
			_gameVersion.ResolveDependencies(_handlerPool);
			_userRegister.ResolveDependencies(_handlerPool);
			_userLogin.ResolveDependencies(_handlerPool);
			_userResult.ResolveDependencies(_handlerPool);
			_gameResults.ResolveDependencies(_handlerPool);

			// 
			_gameVersion.SetLoadingComplete(delegate(GameVersionData data) 
			{ 
				gameVersionCallback(data);
				_isGameVersionLoading = false;
			});

			// 
			_userRegister.SetLoadingComplete(delegate(UserTokenData data) 
			{ 
				userRegisterCallback(data);
				_isUserRegisterLoading = false;
			});

			// 
			_userLogin.SetLoadingComplete(delegate(UserTokenData data) 
			{ 
				userLoginCallback(data);
				_isUserLoginLoading = false;
			});

			// 
			_userResult.SetLoadingComplete(delegate(string data) 
			{ 
				userResultCallback(data);
				_isUserResultLoading = false;
			});	

			// 
			_gameResults.SetLoadingComplete(delegate(GameResultsData data) 
			{ 
				gameResultsCallback(data);
				_isGameResultsLoading = false;
			});				
		}
	


		// 
		public void GameVersionExecute()
		{
			if (!_isGameVersionLoading) 
			{
				_isGameVersionLoading = true;
				_gameVersion.ExecuteRequestAsync();
			}
		}
		// 
		public void UserRegisterExecute(AuthenticationData data)
		{
			if (!_isUserRegisterLoading) 
			{
				_isUserRegisterLoading = true;
				_userRegister.ExecuteRequestAsync(data);
			}
		}
		//
		public void UserLoginExecute(AuthenticationData data)
		{
			if (!_isUserLoginLoading) 
			{
				_isUserLoginLoading = true;
				_userLogin.ExecuteRequestAsync(data);
			}
		}
		// 
		public void UserResultExecute(RequestHeaders headers, UserResultRequest data)
		{
			if (!_isUserResultLoading) 
			{
				_isUserResultLoading = true;
				_userResult.ExecuteRequestAsync(headers, data);
			}
		}			
		// 
		public void GameResultsExecute(RequestHeaders headers)
		{
			if (!_isGameResultsLoading) 
			{
				_isGameResultsLoading = true;
				_gameResults.ExecuteRequestAsync(headers);
			}
		}		
	}
}
