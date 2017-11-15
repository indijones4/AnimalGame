using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAI : MonoBehaviour {

	public Transform Player;
	public int moveSpeed = 1;
	public int minDist = 3;
	public bool recruited = false;
	public bool deposited = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (recruited == true && deposited == false) 
		{
			transform.LookAt (Player);
			if (Vector3.Distance (transform.position, Player.position) >= minDist) 
			{
				transform.position += transform.forward * moveSpeed * Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.CompareTag ("Player"))
		{
			recruited = true;
		}
		if(other.gameObject.CompareTag ("Lair"))
		{
			deposited = true;
		}	
	}
}
