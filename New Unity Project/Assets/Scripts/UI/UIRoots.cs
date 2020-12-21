using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server.DataClasses;
using NewUnityProject.UI.Auth;
using NewUnityProject.UI.FatalError;
using NewUnityProject.UI.GameUI;
using NewUnityProject.UI.Loding;
using NewUnityProject.UI.Results;
using UnityEngine;

namespace NewUnityProject.UI 
{
	// 
	public class UIRoots : MonoBehaviour 
	{
		// 
		[SerializeField] private FatalErrorPanel _fatalErrorPanel;		
		// 
		[SerializeField] private LodingPanel _lodingPanel;
		// 
		[SerializeField] private AuthPanel _authPanel;
		// 
		[SerializeField] private ResultsPanel _resultsPanel;
		// 
		[SerializeField] private GamePanel _gamePanel;

		// 
		private UIPanel[] _allPanels;



		// 
		public void ResolveDependencies(
			AuthPanel.UserRegisterDelegate userRegisterCallback,
			AuthPanel.UserLoginDelegate userLoginCallback,
			ResultsPanel.UpdateResultsDelegate updateResultsCallback,
			ResultsPanel.StartGameDelegate startGameCallback,
			Game.StopGameDelegate stopGameCallback)
		{
			_authPanel.ResolveDependencies(userRegisterCallback, userLoginCallback);
			_resultsPanel.ResolveDependencies(updateResultsCallback, startGameCallback);
			_gamePanel.ResolveDependencies(stopGameCallback);

			// 
			_allPanels = new UIPanel[] { _fatalErrorPanel, _lodingPanel, _authPanel, _resultsPanel, _gamePanel};
			HideAllPanel();
		}



		// 
		private void HideAllPanel()
		{
			for (int i = 0; i < _allPanels.Length; i++)
			{
				_allPanels[i].Hide();
			}
		}
		// 
		private void ShowAllPanel()
		{
			for (int i = 0; i < _allPanels.Length; i++)
			{
				_allPanels[i].Show();
			}
		}



		// 
		public void ShowLodingPanel() 
		{
			HideAllPanel();
			_lodingPanel.Show();
		}
		// 
		public void ShowGamePanel() 
		{ 
			HideAllPanel();
			_gamePanel.Show();
			_gamePanel.StartGame();
		}
		// 
		public void ShowAuthPanel(string message = "") 
		{
			HideAllPanel();
			_authPanel.Show();
			_authPanel.SetMessage(message);
		}
		// 
		public void ShowFatalErrorPanel(string message) 
		{
			HideAllPanel();
			_fatalErrorPanel.Show();
			_fatalErrorPanel.SetMessage(message);
		}
		// 
		public void ShowResultsPanel(UserResultData[] results) 
		{
			HideAllPanel();
			_resultsPanel.Show();
			_resultsPanel.SetResults(results);
		}

	}
}
