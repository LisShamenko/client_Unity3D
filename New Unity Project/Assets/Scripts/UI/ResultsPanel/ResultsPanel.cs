using System.Collections;
using System.Collections.Generic;
using NewUnityProject.Network.Server.DataClasses;
using UnityEngine;
using UnityEngine.UI;

namespace NewUnityProject.UI.Results
{
	public class ResultsPanel : UIPanel
	{
		// 
		[SerializeField] private ScoreItem _scoreItemPrefab;
		// 
		[SerializeField] private Transform _scoresParent;
		// 
		[SerializeField] private ScoreItemPool _scoreItemPool;
		// 
		[SerializeField] private Text _lastResult;

		

		// 
		public void ResolveDependencies(
			UpdateResultsDelegate updateResultsCallback,
			StartGameDelegate startGameCallback)
		{
			_scoreItemPool.ResolveDependencies(_scoreItemPrefab, _scoresParent);

			// 
			SetUpdateResultsEvent(updateResultsCallback);
			SetStartGameEvent(startGameCallback);
		}



		// 
		public void SetResults(UserResultData[] results)
		{
			_scoreItemPool.PushAll();
			for (int i = 0; i < results.Length; i++)
			{
				ScoreItem item = _scoreItemPool.Pop();
				item.SetScores(results[i].Name, results[i].Result.ToString());
			}
		}



		// 
		public void OnUpdateButtonClick()
		{
			UpdateResultsCall();
		}
		// 
		public void OnStartGameButtonClick()
		{
			StartGameCall();
		}
		// 
		public void OnButtonQuitClick()
		{
			Application.Quit();
		}





        #region events


        // 
        public delegate void UpdateResultsDelegate();
		public delegate void StartGameDelegate();


        //
        private event UpdateResultsDelegate UpdateResultsEvent;
        //
        private event StartGameDelegate StartGameEvent;


		// 		
		public void SetUpdateResultsEvent(UpdateResultsDelegate setDelegate)
		{
			UpdateResultsEvent = setDelegate;
		}
		// 		
		public void SetStartGameEvent(StartGameDelegate setDelegate)
		{
			StartGameEvent = setDelegate;
		}


        //
        private void UpdateResultsCall()
        {
            if (UpdateResultsEvent != null)
                UpdateResultsEvent();
        }
        //
        private void StartGameCall()
        {
            if (StartGameEvent != null)
                StartGameEvent();
        }


        #endregion

	}
}
