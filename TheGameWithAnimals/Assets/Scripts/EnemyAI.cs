using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour 
{
	public Transform Player;
	public int moveSpeed = 1;
	public int minDist = 3;
	public int maxDist = 10;
	public int minAttackDist = 1;
	public int maxAttackDist = 2;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance (transform.position, Player.position) <= maxDist)
		{
			transform.LookAt (Player);

			if (Vector3.Distance (transform.position, Player.position) >= minDist) 
			{
				transform.position += transform.forward * moveSpeed * Time.deltaTime;
			}
		}
	}
}
