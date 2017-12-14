using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	public Text HealthText;
	public Text collect1;
	public Text collect2;
	private Animator animate;

	private GameController control;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		location = GetComponent<Transform> ();
		HealthText.text = "Health = " + playerHealth;
		collect1.text = "You need " + (finishscore - score) + " more treasure";
		collect2.text = "You need " + (neededDeposited - deposited) + " more friends";
		control = GameObject.FindWithTag ("Control").GetComponent<GameController> ();
		animate = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.W)) 
		{
			animate.SetBool ("Move", true);
			transform.Translate (speed * Time.deltaTime * Vector3.forward);
			//Vector3 move = new Vector3(transform.forward.x, 0, transform.forward.z);
			//rb.velocity += move * speed;
		} 
		else 
		{
			animate.SetBool ("Move", false);
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
			animate.SetTrigger ("Attack");
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
			animate.SetTrigger("Die");
			control.PlayerDied ();
		}
	}

	//code for hitting things
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			score += 1;
			collect1.text = "You need " + (finishscore - score) + " more treasure";
			other.gameObject.SetActive (false);
		}
		if(other.gameObject.CompareTag ("Lair") && score >= finishscore && deposited >= neededDeposited)
		{
			control.PlayerWon ();
		}
		if(other.gameObject.CompareTag ("EAttack"))
		{
			animate.SetTrigger ("TakeDamge");
			playerHealth -= 1;
			HealthText.text = "Health = " + playerHealth;
			//Vector3 move = new Vector3(transform.forward.x, 0, transform.forward.z);
			//rb.velocity += move * -speed;
		}
	}

	public void IncrementDeposited ()
	{
		deposited++;
		collect2.text = "You need " + (neededDeposited - deposited) + " more friends";
	}
}
