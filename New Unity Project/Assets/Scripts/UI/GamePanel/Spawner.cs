using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.UI.GameUI
{
	// 
	public class Spawner : MonoBehaviour 
	{
		//
		[SerializeField] private ButtonsPool _buttonsPool;
		// 
		[SerializeField] private CalculateSpawnPoints _calculateSpawnPoints;
		// 
		[SerializeField] private Game _game;

		// 
		[SerializeField] private ButtonProperties _badProperties;
		// 
		[SerializeField] private ButtonProperties _goodProperties;
		// 
		[SerializeField] private ButtonMovement _buttonPrefab;
		// 
		[SerializeField] private int _countPerMoment;
		// 
		[SerializeField] private float _spawnTime;
		// 
		[SerializeField] private float _speed;		

		// 
		private int _currentCount;
		// 
		private bool _isBad;
		// 
		private float _timerSpawn;




		// 
		public void ResolveDependencies(ButtonsPool buttonsPool, CalculateSpawnPoints calculateSpawnPoints, Game game)
		{
			_buttonsPool = buttonsPool;
			_calculateSpawnPoints = calculateSpawnPoints;
			_game = game;
			_isBad = true;
		}



		// 
		private void Update() 
		{
			_timerSpawn += Time.deltaTime;
			if (_timerSpawn >= _spawnTime && _currentCount < _countPerMoment)
			{
				_isBad = !_isBad;
				ButtonProperties buttonProps = _isBad ? _badProperties : _goodProperties;
				SpawnPoint spawnPoint = _calculateSpawnPoints.GetRandomPoint();

				// 
				ButtonMovement buttonPrefab = _buttonsPool.Pop();
				buttonPrefab.StartMove(spawnPoint, buttonProps);
				buttonPrefab.SetSpeed(_speed);
				_currentCount++;
				_timerSpawn = 0.0f;
			}
		}




		// 
		public void StartSpawn()
		{
			this.enabled = true;
			_timerSpawn = _spawnTime;
			_currentCount = 0;
		}
		// 
		public void StopSpawn()
		{
			this.enabled = false;
		}

		// 
		public void ButtonPushBack(ButtonProperties backProps, bool isClick)
		{
			bool isGameOver = _game.CheckButtonClick(backProps, isClick);
			if (isGameOver) 
				_buttonsPool.PushAll();
			else
				_currentCount--;
		}

	}
}