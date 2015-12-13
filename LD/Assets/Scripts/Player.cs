﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	public bool RIGHT = false, LEFT = false;

	private CommandIssuer ci;
	private bool GROUNDED = false;

	private int slideCount = 0;

	public float dashSpeed = 0;
	public int punch = 0;
	private BoxCollider2D col;

	public void setBoundingBox(float size){
		col.size   = new Vector2 (0.12f,  size);
		col.offset = new Vector2 (0.025f, size / 2f);
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		ci = GameObject.Find ("cmds").GetComponent<CommandIssuer> ();
		ci.setPlayer (this);
		col = this.GetComponent<BoxCollider2D> ();
	}

	private void jump(float dx){
		if (GROUNDED) {
			rb.AddForce (new Vector2 (dx * 100, 425));
			GROUNDED = false;
		}
	}

	public bool canWork(){
		return punch == 0;
	}

	void OnCollisionEnter2D(Collision2D coll) {
        if (GROUNDED) return;
		if (punch==0)
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
		if (slideCount > 0)
			return;
		dashSpeed = 0;
		ci.idle ();
	}

	public void startSlide(float mult){
		slideCount = 10;
		ci.setSprite (ci.KICK_SLIDE);
		dashSpeed = 0.3f * mult;
	}

	private const float MAX_SPEED = 6.5f;
	private const float WALK_SPEED = 0.1f;
	// Update is called once per frame
	void Update () {
		if (slideCount > 0)
			slideCount--;
		if (punch > 0) {
			punch--;
			if(punch==0){
				ci.idle ();
			}
		}
		if (dashSpeed == 0) {
			setBoundingBox(0.18f);
			if (RIGHT && !ci.isSprite(ci.STAND_2)) {
				rb.AddForce (100*new Vector2 (WALK_SPEED, 0));
			}
			if (LEFT && !ci.isSprite(ci.STAND_2)) {
				rb.AddForce (100*new Vector2 (-WALK_SPEED, 0));
			}
		} else {
			setBoundingBox(0.13f);
			if(punch==0 && (!LEFT && !RIGHT))
				stopSlide();
			dashSpeed *= 0.95f;
			rb.AddForce (100*new Vector2 (dashSpeed, 0));
			if(Mathf.Abs(dashSpeed) < 0.01)
				stopSlide();

		}
		if(rb.velocity.magnitude > MAX_SPEED)
			rb.velocity *= MAX_SPEED / rb.velocity.magnitude;
	}

}
