using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		//code that makes the pickup slowly rotate on the spot
		transform.Rotate (new Vector3 (0, 45, 0) * Time.deltaTime);
	}
}
