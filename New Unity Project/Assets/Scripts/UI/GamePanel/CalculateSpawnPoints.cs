using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.UI.GameUI
{
	public class CalculateSpawnPoints : MonoBehaviour 
	{
		// 
		[SerializeField] private RectTransform _rect;

		//
		[SerializeField] private int _countPoints;

		// (x+, 0) (0, y-) (x-, 0) (0, y+)
		// (0, y-) (x-, 0) (0, y+) (x+, 0)
		private int[] _xSign;
		private int[] _ySign;
		private Vector2[] _starts;

		// 
		[SerializeField] private Vector2[] _points;



		// 
		public void ResolveDependencies()
		{
			Debug.Log("width = " + _rect.rect.width);
			Debug.Log("height = " + _rect.rect.height);
			// 
			_xSign = new int[] { 1, 0, -1, 0 };
			_ySign = new int[] { 0, -1, 0, 1 };
			_starts = new Vector2[] { 
				new Vector2(0.0f, 0.0f), 
				new Vector2(_rect.rect.width, 0.0f), 
				new Vector2(_rect.rect.width, -_rect.rect.height), 
				new Vector2(0.0f, -_rect.rect.height) 
			};

			// 
			CalculatePoints();
		}



		// 
		private void CalculatePoints()
		{
			_points = new Vector2[_countPoints * 4];
			float wOffset = _rect.rect.width / _countPoints;
			float hOffset = _rect.rect.height / _countPoints;
			for (int i = 0, k = 0; i < 4; i++)
			{
				float xHalf = (wOffset / 2.0f) * _xSign[i];
				float yHalf = (hOffset / 2.0f) * _ySign[i];

				// 
				for (int j = 0; j < _countPoints; j++)
				{
					_points[k] = _starts[i] + new Vector2(xHalf, yHalf);

					// 
					xHalf += wOffset * _xSign[i];
					yHalf += hOffset * _ySign[i];
					k++;
				}
			}
		}

		// 
		public SpawnPoint GetRandomPoint() 
		{
			int i = Random.Range(0, _points.Length);
			int dir = (i / _countPoints) + 1;
			dir = (dir < 4) ? dir : 0;

			//
			float distance = (dir == 0 || dir == 2) ? distance = _rect.rect.width :	distance = _rect.rect.height;
			return new SpawnPoint(distance, _xSign[dir], _ySign[dir], _points[i]);
		}
	}

	// 
	public class SpawnPoint 
	{
		public float MaxDistance;
		public int XDir;
		public int YDir;
		public Vector2 Point;

		// 
		public SpawnPoint(float maxDistance, int x, int y, Vector2 point)
		{
			MaxDistance = maxDistance;
			XDir = x;
			YDir = y;
			Point = point;
		}
	}

}