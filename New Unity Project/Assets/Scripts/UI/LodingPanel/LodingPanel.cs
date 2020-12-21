using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.UI.Loding
{
	// 
	public class LodingPanel : UIPanel 
	{
		// 
		[SerializeField] private RectTransform _rotor;

		// 
		private void Update() 
		{
			_rotor.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
		}
	}
}