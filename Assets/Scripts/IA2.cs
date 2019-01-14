using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA2 : MonoBehaviour {

	private bool isHitting = false;
	private bool hasHit = false;
	public float range = 2f;
	public float power = 1700f;
	private Vector3 posBeforeHit = Vector3.zero;
	private Vector3 target;
	private float targetH = 4.5f;
	public float speed = 0.8f;
	public float reactionTime = 0.5f;
	private bool canHit = false;
	private float accuracy = 0;
	private bool positionBack = true;
	public Vector3 initialPos;

	// Use this for initialization
	void Start () {
		posBeforeHit = transform.position;
		initialPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		GameInfo gi = GameObject.FindGameObjectWithTag ("GameInfo").GetComponent<GameInfo> ();
		Vector3 direction = Vector3.RotateTowards (transform.forward, new Vector3(gi.table.transform.position.x, transform.position.y+1,gi.table.transform.position.z) - transform.position, 1, 0f);
		transform.rotation = Quaternion.LookRotation (direction);
		if (!positionBack && (transform.position -posBeforeHit).magnitude < 0.5) {
			positionBack = true;
		}
		if (canHit) {
			positionBack = false;
			float distance = (gi.ball.transform.position - transform.position).magnitude;
			if (distance < range - 1 && !isHitting) {
				posBeforeHit = transform.position;
				isHitting = true;
				target = new Vector3(gi.table.transform.position.x + (Random.value*2-1)*gi.table.transform.localScale.z/4*gi.scale, transform.position.y+targetH-gi.ball.transform.position.y,(gi.table.transform.position+(gi.table.transform.position-transform.position)/2).z);
				transform.position = Vector3.MoveTowards (transform.position, new Vector3(target.x, transform.position.y, target.z), 1.5f*gi.scale);
			} else if (isHitting) {
				if ((transform.position - posBeforeHit).magnitude <= range && !hasHit) {
					transform.position = Vector3.MoveTowards (transform.position, new Vector3(target.x, transform.position.y, target.z), 1.5f*gi.scale);
				} else if ((gi.ball.transform.position - transform.position).magnitude <= gi.ball.transform.localScale.x/2+this.transform.localScale.z/2) {
					hasHit = true;
					transform.position = Vector3.MoveTowards (transform.position, posBeforeHit, 1.5f*gi.scale);
				} else if (hasHit) {
					transform.position = Vector3.MoveTowards (transform.position, posBeforeHit, 1.5f*gi.scale);
				}
				if (transform.position == posBeforeHit) {
					isHitting = false;
					hasHit = false;
					canHit = false;
				}
			} else {
				float a = (gi.ball.transform.position.x - target.x) / (gi.ball.transform.position.z - target.z);
				float b = target.x - a * target.z;
				transform.position = Vector3.MoveTowards (transform.position, new Vector3(/*gi.ball.transform.position.x*/a*transform.position.z+b,gi.ball.transform.position.y,transform.position.z), speed);
			}
		} else {
			if (Vector3.Dot(gi.ball.transform.position - gi.ball.GetComponent<BallBehaviour>().previousPos, this.transform.forward) < 0 && positionBack) {
				Invoke ("CanHit", reactionTime);
			}
		}
		gi.ball.GetComponent<BallBehaviour> ().previousPos = gi.ball.transform.position;
	}

	void CanHit(){
		canHit = true;
	}

	public void initialValues(){
		isHitting = false;
		hasHit = false;
		canHit = false;
		positionBack = true;
		posBeforeHit = transform.position;
	}
}
