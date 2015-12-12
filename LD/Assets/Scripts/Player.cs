﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	public bool RIGHT = false, LEFT = false;

	private CommandIssuer ci;
	private bool GROUNDED = false;

	public float dashSpeed = 0;
	public int punch = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		ci = GameObject.Find ("cmds").GetComponent<CommandIssuer> ();
		ci.setPlayer (this);
	}

	private void jump(float dx){
		if (GROUNDED) {
			rb.AddForce (new Vector2 (dx * 100, 200));
			GROUNDED = false;
		}
	}

	public bool canWork(){
		return punch == 0;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		startSlide (RIGHT ? 1 : -1);
		GROUNDED = true;

	}

	public void issueCommand(Command cmd){

		if (!(punch == 0))
			return;

		if (cmd.isCmd ("rr"))
			jump (1);
		if (cmd.isCmd ("ll"))
			jump (-1);

		if(cmd.isCmd("lrl"))
			startSlide(-1);
		
		if(cmd.isCmd("rlr"))
			startSlide(1);


	}

	public void stopSlide(){
		dashSpeed = 0;
		ci.idle ();
	}

	public void startSlide(float mult){
		ci.setSprite (ci.KICK_SLIDE);
		dashSpeed = 0.3f * mult;
	}

	private const float WALK_SPEED = 0.1f;
	// Update is called once per frame
	void Update () {
		if (punch > 0) {
			punch--;
			if(punch==0){
				ci.idle ();
			}
		}
		if (dashSpeed == 0) {
			if (RIGHT && !ci.isSprite(ci.STAND_2)) {
				transform.Translate (new Vector3 (WALK_SPEED, 0, 0));
			}
			if (LEFT && !ci.isSprite(ci.STAND_2)) {
				transform.Translate (new Vector3 (-WALK_SPEED, 0, 0));
			}
		} else {
			if(punch==0 && (!LEFT && !RIGHT))
				stopSlide();
			dashSpeed *= 0.95f;
			transform.Translate(new Vector3(dashSpeed, 0,0));
			if(Mathf.Abs(dashSpeed) < 0.01)
				stopSlide();

		}

	}
}
