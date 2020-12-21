using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewUnityProject.UI.FatalError
{
	public class FatalErrorPanel : UIPanel 
	{
		// 
		[SerializeField] private Text _message;

		// 
		public void SetMessage(string message)
		{
			_message.text = message;
		}

		// 
		public void OnButtonQuitClick()
		{
			Application.Quit();
		}
	}
}