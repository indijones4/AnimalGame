using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform player; 




	// Use this for initialization
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform; 

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
