using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour 
{
	[Inject] MapViewEvents mapEvents;

	// Use this for initialization
	void Start () 
	{
		Debug.Log("test");
		Debug.Log(mapEvents);
		
		var g = new Greeter("gg");
		g.ShowInjection();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
