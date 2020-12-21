using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewUnityProject.UI.GameUI
{
	public class ButtonMovement : MonoBehaviour 
	{
		// 
		[SerializeField] private ButtonsPool _buttonsPool;
		// 
		[SerializeField] private Spawner _spawner;

		// 
		[SerializeField] private RectTransform _self;
		// 
		[SerializeField] private Text _text;
		
		// 
		private ButtonProperties _buttonProperties;
		// 
		private SpawnPoint _spawnPoint;
		// 
		private float _speed;
		// 
		private float _distance;
		// 
		private bool _isClick;



		// 
		public void ResolveDependencies(ButtonsPool buttonsPool, Spawner spawner)
		{
			_buttonsPool = buttonsPool;
			_spawner = spawner;
		}



		// 
		private void Update() 
		{
			float offset = _speed * Time.deltaTime;
			_distance += offset;
			if (_distance >= _spawnPoint.MaxDistance)
			{
				_spawner.ButtonPushBack(_buttonProperties, false);
				_buttonsPool.Push(this);
			}
			_self.anchoredPosition += new Vector2(offset * _spawnPoint.XDir, offset * _spawnPoint.YDir);
		}

		// 
		public void StartMove(SpawnPoint spawnPoint, ButtonProperties buttonProperties)
		{
			_spawnPoint = spawnPoint;
			_buttonProperties = buttonProperties;
			_distance = 0.0f;
			_isClick = false;
			_self.anchoredPosition = _spawnPoint.Point;
			_text.text = buttonProperties.Bonus.ToString();
		}

		// 
		public void SetSpeed(float speed)
		{
			_speed = speed;
		}



		// 
		public void OnButtonClick()
        {
			if (!_isClick) 
			{
				_isClick = true;
				_spawner.ButtonPushBack(_buttonProperties, true);
				_buttonsPool.Push(this);
			}
        }
	}
}