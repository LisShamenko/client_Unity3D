using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.UI.GameUI
{
	// 
	public class GamePanel : UIPanel 
	{
		//
		[SerializeField] private Spawner _spawner;		
		//
		[SerializeField] private ButtonsPool _buttonsPool;
		// 
		[SerializeField] private CalculateSpawnPoints _calculateSpawnPoints;
		// 
		[SerializeField] private Game _game;

		// 
		[SerializeField] private ButtonMovement _buttonPrefab;
		// 
		[SerializeField] private Transform _buttonsParent;



		// 
		public void ResolveDependencies(
			Game.StopGameDelegate stopGameCallback)
		{
			_calculateSpawnPoints.ResolveDependencies();
			_buttonsPool.ResolveDependencies(_spawner, _buttonPrefab, _buttonsParent);
			_spawner.ResolveDependencies(_buttonsPool, _calculateSpawnPoints, _game);

			// 
			_game.ResolveDependencies(_spawner);
			_game.SetStopGameEvent(stopGameCallback);
		}



		// 
		public void StartGame()
		{
			_game.StartGame();
		}

	}
}