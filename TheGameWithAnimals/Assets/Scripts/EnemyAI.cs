using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour 
{
	public Transform Player;
	public float moveSpeed = 0.5f;
	public int minDist = 2;
	public int maxDist = 10;
	public int maxAttackDist = 3;
	public int enemyHealth = 1;
	public int enemyAttack = 1;

	private Rigidbody rb;

	//public float timeBetweenMove = 1f;
	//private float timeBetweenMoveCounter;
	//public float timeToMove = 1f;
	//private float timeToMoveCounter;

	//private bool moving; 

	//private Vector3 moveDirection;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		GameObject temp = GameObject.Find ("PlayerModel");
		Player = temp.transform;
		//timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		//timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance (transform.position, Player.position) <= maxDist) 
		{
			transform.LookAt (Player);

			if (Vector3.Distance (transform.position, Player.position) >= minDist) 
			{
				//transform.position += transform.forward * moveSpeed * Time.deltaTime;
				Vector3 move = new Vector3 (transform.forward.x, 0, transform.forward.z);
				rb.velocity += move * moveSpeed;
			}
			if (Vector3.Distance (transform.position, Player.position) <= maxAttackDist) 
			{
				transform.GetChild (0).gameObject.SetActive (true);
			}
		} 
		else 
		{
			transform.GetChild (0).gameObject.SetActive (false);
		}
		//else 
		//{
		//	//code for idle movement 
		//	if (moving) 
		//	{
		//		timeBetweenMoveCounter -= Time.deltaTime;
		//		rb.velocity = moveDirection;
		//		if (timeToMoveCounter < 0f) 
		//		{
		//			moving = false;
		//			timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		//		}
				//transform.Rotate (0, random * Time.deltaTime, 0);
				//transform.Translate (moveSpeed * Time.deltaTime * Vector3.forward);
		//	} 
		//	else 
		//	{
		//		timeBetweenMoveCounter -= Time.deltaTime;
		//		rb.velocity = Vector3.zero;
		//		if (timeBetweenMoveCounter < 0f) 
		//		{
		//			moving = true;
		//			timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeToMove * 1.25f);
		//			moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, 0f, Random.Range (-1f, 1f) * moveSpeed);
		//		}
		//	}
		//}
		if (enemyHealth <= 0)
		{
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Attack")) 
		{
			enemyHealth -= 1;
			//Vector3 move = new Vector3(transform.forward.x, 0, transform.forward.z);
			//rb.velocity += move * -speed;
		}
	}
}
