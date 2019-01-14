using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour {

	private Vector3 initialPos;
	public float initialSpeed = 10f;
	public Vector3 previousPos = Vector3.zero;
	private float bounciness;
	public GameObject count;
	public GameObject best;
	public GameObject gi;
	private string collisionName;
	private float lastHitTime;

	// Use this for initialization
	void Start () {
		initialPos = this.transform.localPosition;
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.value * 10 - 5, 5, initialSpeed);
		bounciness = this.GetComponent<Collider> ().material.bounciness;
		lastHitTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("r") || this.transform.position.y <-3 || Time.time - lastHitTime > 10) {
			lastHitTime = Time.time;
			this.transform.localPosition = initialPos;
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.value * 10 - 5, 5, initialSpeed);
			count.GetComponent<Text> ().text = "0";
			float scale = gi.GetComponent<GameInfo> ().scale;
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("Racket")) {
				if (go.gameObject.GetComponent<IA> () && go.gameObject.GetComponent<IA> ().isActiveAndEnabled) {//IA1
					if (go.transform.position.z > 0) {
						go.transform.position = new Vector3 (0, 9.5f*scale, 37.5f*scale);
					} else {
						go.transform.position = new Vector3 (0, 9.5f*scale, -37.5f*scale);
					}
					go.gameObject.GetComponent<IA> ().initialValues ();
				} else if (go.gameObject.GetComponent<IA2> () && go.gameObject.GetComponent<IA2> ().isActiveAndEnabled) {//IA2
					if (go.transform.position.z > 0) {
						go.transform.position = new Vector3 (0, 9.5f*scale, 37.5f*scale);
					} else {
						go.transform.position = new Vector3 (0, 9.5f*scale, -37.5f*scale);
					}
					go.gameObject.GetComponent<IA> ().initialValues();
				}
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Racket" && collision.gameObject.name != collisionName) {
				lastHitTime = Time.time;	
				Text text = count.GetComponent<Text> ();
				text.text = (int.Parse (text.text) + 1).ToString ();
				collisionName = collision.gameObject.name;
				if (int.Parse (text.text) > int.Parse (best.GetComponent<Text> ().text)) {
					best.GetComponent<Text> ().text = text.text;
				}
			if (collision.gameObject.GetComponent<IA> () && collision.gameObject.GetComponent<IA> ().isActiveAndEnabled) {//IA1
				Vector3 rDir = collision.gameObject.transform.forward.normalized;
				Vector3 ballV = this.GetComponent<Rigidbody> ().velocity;
				ballV = ((Vector3.Dot (ballV.normalized, rDir) * rDir) * 2 + ballV.normalized) * ballV.magnitude * bounciness;
				this.GetComponent<Rigidbody> ().AddForce (collision.gameObject.GetComponent<IA> ().power *1.5f/ this.GetComponent<Rigidbody> ().velocity.magnitude * collision.gameObject.transform.forward);
			} else if (collision.gameObject.GetComponent<IA2> () && collision.gameObject.GetComponent<IA2> ().isActiveAndEnabled) {//IA2
				Vector3 rDir = collision.gameObject.transform.forward.normalized;
				Vector3 ballV = this.GetComponent<Rigidbody> ().velocity;
				ballV = ((Vector3.Dot (ballV.normalized, rDir) * rDir) * 2 + ballV.normalized) * ballV.magnitude * bounciness;
				this.GetComponent<Rigidbody> ().AddForce (collision.gameObject.GetComponent<IA2> ().power / this.GetComponent<Rigidbody> ().velocity.magnitude * collision.gameObject.transform.forward);
			} else {//Player
				Vector3 rDir = collision.gameObject.transform.forward.normalized;
				Vector3 ballV = this.GetComponent<Rigidbody> ().velocity;
				ballV = ((Vector3.Dot (ballV.normalized, rDir) * rDir) * 2 + ballV.normalized) * ballV.magnitude * bounciness;
				this.GetComponent<Rigidbody> ().AddForce (1700 / this.GetComponent<Rigidbody> ().velocity.magnitude * collision.gameObject.transform.forward);
			}
		} else if(collision.gameObject.tag == "Table"){
			Vector3 rDir = Vector3.up.normalized;
			Vector3 ballV = this.GetComponent<Rigidbody> ().velocity;
			ballV = ((Vector3.Dot (ballV.normalized, rDir) * rDir) * 2 + ballV.normalized) * ballV.magnitude * bounciness;
		}
	}
}
