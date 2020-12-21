using System;
using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server;
using NewUnityProject.Network.Server.DataClasses;
using NewUnityProject.UI;
using UnityEngine;

namespace NewUnityProject.Roots
{
	// 
	public class SceneRoot : MonoBehaviour 
	{
		// 
		[SerializeField] private NetworkRoot _networkRoot;
		// 
		[SerializeField] private UIRoots _uiRoots;
		


		

		// 
		[SerializeField] private string _userId;
		// 
		[SerializeField] private string _token;

		// 
		private void Start() 
		{
			_uiRoots.ResolveDependencies(
				delegate(string name, string password) // событие: регистрация пользователя
				{
					_uiRoots.ShowLodingPanel();
					_networkRoot.UserRegisterExecute(new AuthenticationData(name, password));
				},
				delegate(string name, string password) // событие: вход пользователя
				{
					_uiRoots.ShowLodingPanel();
					_networkRoot.UserLoginExecute(new AuthenticationData(name, password));
				},
				delegate() // событие: обновить результаты
				{
					_uiRoots.ShowLodingPanel();
					_networkRoot.GameResultsExecute(new RequestHeaders(_userId, _token));
				},
				delegate() // событие: начать игру
				{
					_uiRoots.ShowGamePanel();
				},
				delegate(int result) // событие: закончить игру
				{
					_uiRoots.ShowLodingPanel();
					_networkRoot.UserResultExecute(
						new RequestHeaders(_userId, _token), 
						new UserResultRequest(_userId, result));
				}			
			);

			// 
			_networkRoot.ResolveDependencies(
				delegate(RequestResult<GameVersionData> requestResult) // версия игры
				{
					if (requestResult.Status == Status.Success)
					{
						Debug.Log("--- GameVersionData --- Version = " + requestResult.Result.Version + " ---");
						_uiRoots.ShowAuthPanel();
					}
					else
					{						
						Debug.Log("--- GameVersionData --- NULL ---");
						_uiRoots.ShowFatalErrorPanel("Отсутствует соединение по сети!");
					}
				}, 
				delegate(RequestResult<UserTokenData> requestResult) // регистрация
				{
					if (requestResult.Status == Status.Success)
					{
						Debug.Log("--- data --- UserId = " + requestResult.Result.UserId + " --- Token = " + requestResult.Result.Token + " ---");
						_userId = requestResult.Result.UserId;
						_token = requestResult.Result.Token;
						_uiRoots.ShowLodingPanel();
						_networkRoot.GameResultsExecute(new RequestHeaders(_userId, _token));
					}
					else
					{
						_uiRoots.ShowAuthPanel(requestResult.Message);
					}
				},
				delegate(RequestResult<UserTokenData> requestResult) // логин
				{
					if (requestResult.Status == Status.Success)
					{
						Debug.Log("--- data --- UserId = " + requestResult.Result.UserId + " --- Token = " + requestResult.Result.Token + " ---");
						_userId = requestResult.Result.UserId;
						_token = requestResult.Result.Token;
						_uiRoots.ShowLodingPanel();
						_networkRoot.GameResultsExecute(new RequestHeaders(_userId, _token));						
					}
					else
					{
						_uiRoots.ShowAuthPanel(requestResult.Message);
					}
				},
				delegate(RequestResult<string> requestResult) // отправить результат 
				{
					if (requestResult.Status == Status.Success)
					{
						Debug.Log("--- data --- data = " + requestResult.Result + " ---");
						_networkRoot.GameResultsExecute(new RequestHeaders(_userId, _token));
					}
					else
					{
						_uiRoots.ShowFatalErrorPanel(requestResult.Message);
					}
				},				
				delegate(RequestResult<GameResultsData> requestResult) // вернуть результаты
				{
					if (requestResult.Status == Status.Success)
					{
						foreach (UserResultData result in requestResult.Result.Results)
						{
							Debug.Log("--- data --- Name = " + result.Name + " --- Result = " + result.Result + " ---");
						}
						_uiRoots.ShowResultsPanel(requestResult.Result.Results);
					}
					else
					{
						_uiRoots.ShowFatalErrorPanel(requestResult.Message);
					}
				});

			// 
			_uiRoots.ShowLodingPanel();
			_networkRoot.GameVersionExecute();
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