using Code.Game.Map.Controller;
using Code.Game.Map.Data;
using Code.Game.Map.View;
using UnityEngine;

namespace Code.Game
{
	public class StrategyCore : MonoBehaviour
	{
		[SerializeField]
		private MapView _mapView;

		private MapController _mapController;
	
		// Use this for initialization
		void Awake()
		{
			var mapSource = Resources.Load("Data/mapProperties");
			
			var mapData = new MapData();
			mapData.Source = mapSource;
			
			_mapController = new MapController();
			_mapController.Data = mapData;
			_mapController.MapView = _mapView;
			_mapController.Initialize();
		}
	
		// Update is called once per frame
		void Update() 
		{
			if (Input.GetMouseButtonUp(0))
			{
				_mapController.MakeAction();
			}
		}
	}
}
