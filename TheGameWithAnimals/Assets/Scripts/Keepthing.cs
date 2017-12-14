using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keepthing : MonoBehaviour {

	public static Keepthing instance = null;

	//awake is called before start
	void Awake () 
	{
		//this code is to ensure only one GameController is present
		if (instance == null) 
		{
			instance = this;
		} 
		else if (instance != this) 
		{
			Destroy (gameObject);
		}
		//this will stop the game controller being destroyed and remade every scene load which would reset all the variables.
		DontDestroyOnLoad (gameObject);
	}
}
