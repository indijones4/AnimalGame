using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAI : MonoBehaviour {

	public PlayerScript player;
	public Transform playermodel;
	public float moveSpeed = 0.5f;
	public int minDist = 3;
	public bool recruited = false;
	public bool deposited = false;

	private Rigidbody rb;
	private Animator animate;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		animate = GetComponentInChildren<Animator> ();
		player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
		playermodel = GameObject.FindWithTag ("PlayerModel").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (recruited == true && deposited == false) 
		{
			transform.LookAt (playermodel.transform);
			if (Vector3.Distance (transform.position, playermodel.transform.position) >= minDist)
			{
				animate.SetBool ("Move", true);
				//transform.position += transform.forward * moveSpeed * Time.deltaTime;
				Vector3 move = new Vector3 (transform.forward.x, 0, transform.forward.z);
				rb.velocity += move * moveSpeed;
			} 
			else 
			{
				animate.SetBool ("Move", false);
				rb.velocity = Vector3.zero;
			}
		}
		if (deposited == true) 
		{
			player.IncrementDeposited();
			this.enabled = false;
			animate.SetBool("Move",false);
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
