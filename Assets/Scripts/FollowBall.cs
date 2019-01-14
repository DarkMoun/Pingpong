using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameInfo gi = GameObject.FindGameObjectWithTag ("GameInfo").GetComponent<GameInfo> ();
		transform.position = new Vector3(gi.ball.transform.position.x,-9f,gi.ball.transform.position.z);
	}
}
