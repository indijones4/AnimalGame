using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAI : MonoBehaviour {

	public PlayerScript player;
	public float moveSpeed = 0.5f;
	public int minDist = 3;
	public bool recruited = false;
	public bool deposited = false;

	private Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (recruited == true && deposited == false) 
		{
			transform.LookAt (player.transform);
			if (Vector3.Distance (transform.position, player.transform.position) >= minDist) 
			{
				//transform.position += transform.forward * moveSpeed * Time.deltaTime;
				Vector3 move = new Vector3 (transform.forward.x, 0, transform.forward.z);
				rb.velocity += move * moveSpeed;
			} 
			else 
			{
				rb.velocity = Vector3.zero;
			}
		}
		if (deposited == true) 
		{
			player.IncrementDeposited();
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
