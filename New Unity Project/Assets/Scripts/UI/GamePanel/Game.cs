using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewUnityProject.UI.GameUI
{
	public class Game : MonoBehaviour
	{
		//
		[SerializeField] private Spawner _spawner;	

		// 
		[SerializeField] private Text _textScores;
		// 
		[SerializeField] private Text _textLives;

		// 
		[SerializeField] private int _startPoints;
		// 
		[SerializeField] private int _currentPoints;
		// 
		[SerializeField] private int _gameScore;
		


		// 
		public void ResolveDependencies(Spawner spawner)
		{
			_spawner = spawner;
		}



		// 
		public void StartGame()
		{
			_currentPoints = _startPoints;
			_textLives.text = _currentPoints.ToString();
			_gameScore = 0;
			_textScores.text = _gameScore.ToString();
			_spawner.StartSpawn();
		}

		// 
		public void StopGame()
		{
			_spawner.StopSpawn();
			StopGameCall(_gameScore);
		}



		// 
		public bool CheckButtonClick(ButtonProperties backProps, bool isClick)
		{
			Debug.Log("--- CheckButtonClick --- Bonus = " + backProps.Bonus + " --- isClick = " + isClick);
			if (isClick && backProps.Bonus > 0) 
			{
				_gameScore += (int)Mathf.Pow(2.0f, (6 - _currentPoints));
				_textScores.text = _gameScore.ToString();
			}

			// 
			if (isClick)
			{
				if (backProps.Bonus > 0) 
				{
					_currentPoints += backProps.Bonus;
					_currentPoints = (_currentPoints > 5) ? 5 : _currentPoints;
				}
			}
			else
			{
				_currentPoints -= 1;
			}
			_textLives.text = _currentPoints.ToString();

			// 
			if (_currentPoints <= 0) 
			{
				StopGame();
				return true;
			}
			return false;
		}





        #region events


        // 
        public delegate void StopGameDelegate(int result);

        //
        private event StopGameDelegate StopGameEvent;

		// 		
		public void SetStopGameEvent(StopGameDelegate stopGameDelegate)
		{
			StopGameEvent = stopGameDelegate;
		}
        //
        private void StopGameCall(int result)
        {
            if (StopGameEvent != null)
                StopGameEvent(result);
        }


        #endregion 





//#region events
//public delegate void StopGameDelegate(WaitStopGameDelegate callback);
//public delegate void WaitStopGameDelegate(string data);
//private event StopGameDelegate StopGameEvent;
//public void SetStopGameEvent(StopGameDelegate stopGameDelegate) { StopGameEvent = stopGameDelegate; }
//private void StopGameCall(WaitStopGameDelegate callback) { if (StopGameEvent != null) StopGameEvent(callback); }
//#endregion  


	}
}