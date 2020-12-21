using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.UI.GameUI
{
	public class ButtonsPool : MonoBehaviour 
	{
		//
		[SerializeField] private Spawner _spawner;		
		// 
		private Queue<ButtonMovement> _queue;
		//
		private List<ButtonMovement> _freeButtons;
		// 
		[SerializeField] private ButtonMovement _buttonPrefab;
		// 
		[SerializeField] private Transform _buttonsParent;



		// 
		public void ResolveDependencies(Spawner spawner, ButtonMovement buttonPrefab, Transform buttonsParent)
		{
			_spawner = spawner;
			_buttonPrefab = buttonPrefab;
			_buttonsParent = buttonsParent;
			_queue = new Queue<ButtonMovement>();
			_freeButtons = new List<ButtonMovement>();
		}



		// 
		public void PushAll()
		{
			for (int i = 0; i < _freeButtons.Count; i++)
			{
				Push(_freeButtons[i]);
			}
		}
		
		// 
		public void Push(ButtonMovement buttonMovement)
		{
			buttonMovement.gameObject.SetActive(false);
			buttonMovement.transform.position = new Vector3(-100.0f, -100.0f, -100.0f);
			_queue.Enqueue(buttonMovement);
			_freeButtons.Remove(buttonMovement);
		}
		// 
		public ButtonMovement Pop()
		{
			ButtonMovement buttonMovement;	
			if (_queue.Count <= 0)
			{
				buttonMovement = GameObject.Instantiate<ButtonMovement>(_buttonPrefab);
				buttonMovement.transform.SetParent(_buttonsParent);
				buttonMovement.transform.position = new Vector3(-100.0f, -100.0f, -100.0f);
				buttonMovement.ResolveDependencies(this, _spawner);
			}
			else
			{
				buttonMovement = _queue.Dequeue();
				buttonMovement.gameObject.SetActive(true);
			}

			// 
			_freeButtons.Add(buttonMovement);
			return buttonMovement;
		}
	}
}