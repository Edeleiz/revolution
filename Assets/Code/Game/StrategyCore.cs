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
		private void Start()
		{
			var mapSource = Resources.Load("Data/mapProperties");
			
			var mapData = new MapData();
			mapData.Source = mapSource;
			
			_mapController = new MapController();
			_mapController.MapView = _mapView;
			_mapController.Data = mapData;
			_mapController.Initialize();
		}
	
		// Update is called once per frame
		void Update() 
		{
		}
	}
}
