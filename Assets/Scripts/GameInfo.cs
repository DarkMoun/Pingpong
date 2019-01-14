using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

	public GameObject ball;
	public GameObject table;
	public GameObject uI;
	public float scale = 0.1f;
	public bool spectateMode = true;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3 (scale, scale, scale);
	}
	
	// Update is called once per frame
	void Update () {
		if (spectateMode) {
			Camera.main.transform.position = new Vector3 (-11, 8, 2);
			Camera.main.transform.rotation = Quaternion.Euler(40,90,0);
		} else {
			Camera.main.transform.position = new Vector3 (0, 6.5f, -14.5f);
			Camera.main.transform.rotation = Quaternion.Euler(30,0,0);
		}

		if (Input.GetKeyDown ("v")) {
			spectateMode = !spectateMode;
		}
	}
}
