using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//variables
	public static GameController instance = null;
	public bool gameOver = false;
	public Text dieText;
	public Text winText;

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
	
	// Update is called once per frame
	void Update () 
	{
		//restarts the game after spacebar is pressed
		if (gameOver == true && Input.GetKeyDown (KeyCode.Space)) 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			gameOver = false;
			Time.timeScale = 1;
		}
		//quits the game if esc is pressed
		if (Input.GetKey (KeyCode.Escape)) 
		{
			Application.Quit ();
		}
	}

	//a corountinue to be called in the playerscript for when the player dies
	public void PlayerDied()
	{
		gameOver = true;
		dieText.text = "You died... \n press spacebar to try again";
		Time.timeScale = 0;
	}

	//a corountinue to be called in the playerscript for when the player wins
	public void PlayerWon()
	{
		gameOver = true;
		winText.text = "YOU WON! \n press spacebar to try again";
		Time.timeScale = 0;
	}

	//no idea what this does really but it makes the other things work
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//fetching the texts by searching for their tags
		dieText = GameObject.FindGameObjectWithTag ("GameOverText").GetComponent<Text>();
		winText = GameObject.FindGameObjectWithTag ("WinText").GetComponent<Text>();
	}
}
