using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour 
{
	//Declaring the many variables
	public float speed = 20;
	public float turnSpeed = 100;
	public float jumpSpeed = 10;
	public Rigidbody rb;
	public Transform location;
	public int playerHealth = 5;
	public int playerAttack = 1;
	public bool canDoubleJump = false;
	public int score = 0;
	public int finishscore = 17;
	public int deposited = 0;
	public int neededDeposited = 1;

	//private Animation dragonAni;

	public GameController control;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		location = GetComponent<Transform> ();
		//dragonAni = GetComponentInChildren(Animation);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.W)) 
		{
			transform.Translate (speed * Time.deltaTime * Vector3.forward);
			//Vector3 move = new Vector3(transform.forward.x, 0, transform.forward.z);
			//rb.velocity += move * speed;
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate (0, turnSpeed * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Rotate (0, -turnSpeed * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			transform.Translate (-speed * Time.deltaTime * Vector3.forward);
		}

		if (Input.GetMouseButtonDown(0)) 
		{
			transform.GetChild (2).gameObject.SetActive (true);
		} 
		else 
		{
			transform.GetChild (2).gameObject.SetActive (false);
		}

		//jump code
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			//declaring a variable for the raycast offset
			float raycastOffset = 0.01f;
			//Applying the offset and doing the Raycast 
			if (Physics.Raycast(transform.position + raycastOffset * transform.up, -transform.up, 2 * raycastOffset))
			//if (Physics.Raycast(transform.position, Vector3.down))
			{
				rb.velocity += Vector3.up * jumpSpeed;
				//transform.Translate (jumpSpeed * Time.deltaTime * Vector3.up);
				canDoubleJump = true;
			}
			//some code that allows a double jump but stop any extra jump
			else if (canDoubleJump == true) 
			{
				rb.velocity += Vector3.up * jumpSpeed;
				canDoubleJump = false;
			}	
		}
		if (playerHealth <= 0) 
		{
			control.PlayerDied ();
		}
	}

	//code for hitting things
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			score += 1;
			other.gameObject.SetActive (false);
		}
		if(other.gameObject.CompareTag ("Lair") && score >= finishscore && deposited >= neededDeposited)
		{
			control.PlayerWon ();
		}
		if(other.gameObject.CompareTag ("EAttack"))
		{
			playerHealth -= 1;
			//Vector3 move = new Vector3(transform.forward.x, 0, transform.forward.z);
			//rb.velocity += move * -speed;
		}
	}

	public void IncrementDeposited ()
	{
		deposited++;
	}
}
