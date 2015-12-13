﻿using UnityEngine;
using System.Collections;

public class CommandIssuer : MonoBehaviour {

	private int t = 0, tmax = 12;
	private Player p;
	private Command cmd;

	private int ts = 0, tsmax = 50;
	private int tw = 0, twmax = 13;

    private int tt = 0, ttmax = 110;

	private SpriteRenderer sr;
	public Sprite STAND_1, STAND_2, KICK_FLY, KICK_SLIDE, PUNCH, WALK1, WALK2, WALK3, WALK4, WALK5, WALK6;

	private string[] cmds;

	// Use this for initialization
	void Start () {
		cmd = new Command ();
		cmds = new string[]{"l","r","ll","rr","lrl","rlr"};

		STAND_1    = Resources.Load <Sprite> ("Sprites/Stand1");
		STAND_2    = Resources.Load <Sprite> ("Sprites/Stand2");
		KICK_FLY   = Resources.Load <Sprite> ("Sprites/FlyingKick");
		KICK_SLIDE = Resources.Load <Sprite> ("Sprites/SlideKick");
		PUNCH      = Resources.Load <Sprite> ("Sprites/FistPunch");
		WALK1      = Resources.Load <Sprite> ("Sprites/Walk1_1");
		WALK2      = Resources.Load <Sprite> ("Sprites/Walk2_1");
		WALK3      = Resources.Load <Sprite> ("Sprites/Walk3_1");
		WALK4      = Resources.Load <Sprite> ("Sprites/Walk4_1");
		WALK5      = Resources.Load <Sprite> ("Sprites/Walk5_1");
		WALK6      = Resources.Load <Sprite> ("Sprites/Walk6_1");
	}

	public bool isSprite(Sprite s){
		return sr.sprite.Equals (s);
	}

	public void setSprite(Sprite s){
		sr.sprite = s;
	}

	public void punch(){
		p.punch = 30;
		setSprite (PUNCH);
		p.dashSpeed = p.transform.localScale.x < 0 ? 0.15f : -0.15f;
	}

	public void setPlayer(Player pl){
		this.p = pl;
		this.sr = p.GetComponent<SpriteRenderer> ();
	}
	
	void issue(){	
		bool valid = false;
		foreach (string s in cmds) {
			if(cmd.getCmd().Equals(s)){
				valid = true;
				break;
			}
		}
		if (!valid || !p.canWork())
			return;
		if (cmd.len () > 1) {
			
			if(cmd.isCmd("rlr") || cmd.isCmd("lrl") && p.punch==0)
				setSprite(KICK_SLIDE);

			if(cmd.isCmd("rr") || cmd.isCmd("ll") && p.punch==0)
				setSprite(KICK_FLY);

			p.issueCommand (cmd);
			cmd = new Command ();
		}
	}

	public void idle(){
		setSprite (STAND_1);
		p.dashSpeed = 0;
	}

	// Update is called once per frame
	void Update () {

        if(++tt >= ttmax)
        {
            tt = 0;
            GameObject go = (GameObject)Instantiate(Resources.Load("Ball"));
            BallController bc = go.GetComponent<BallController>();
            bc.spawn(p);
        }

		if (p.LEFT && p.transform.localScale.x < 0)
			faceLeft ();
		
		if (p.RIGHT && p.transform.localScale.x > 0)
			faceRight ();

		if (!p.LEFT && !p.RIGHT && ++ts >= tsmax) {
			ts=0;

			if(isSprite(STAND_1) || isSprite (WALK1) || isSprite (WALK2) || isSprite (WALK3) || isSprite (WALK4) || isSprite (WALK5) || isSprite (WALK6)){
				setSprite(STAND_2);
			} else if(isSprite(STAND_2)) {
				setSprite(STAND_1);}
		}

		if ((p.LEFT || p.RIGHT) && p.dashSpeed==0 && ++tw >= twmax) {
			tw=0;

			if(isSprite (STAND_2)){
				setSprite (STAND_1);
			} else if(isSprite(STAND_1)){
				setSprite (WALK1);
			} else if(isSprite(WALK1)){
				setSprite (WALK2);
			} else if(isSprite(WALK2)){
				setSprite (WALK3);
			} else if(isSprite(WALK3)){
				setSprite (WALK4);
			} else if(isSprite(WALK4)){
				setSprite (WALK5);
			} else if(isSprite(WALK5)){
				setSprite (WALK6);
			} else if(isSprite(WALK6)){
				setSprite (STAND_1);
			}
		}

		if (p == null)
			return;
		if (++t == tmax) 
			cmd = new Command ();

		if (p.canWork() && (

		    (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightArrow))
		 || (Input.GetKey(KeyCode.LeftArrow)     && Input.GetKeyDown(KeyCode.RightArrow) && cmd.empty() && (p.dashSpeed == 0 || Mathf.Abs(p.dashSpeed) > 0.025))
		 || (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)     && cmd.empty() && (p.dashSpeed == 0 || Mathf.Abs(p.dashSpeed) > 0.025))
			)) {
			punch();
			return;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			t = 0;
			cmd.addRight ();
			faceRight();
			p.RIGHT = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			t = 0;
			cmd.addLeft ();
			faceLeft();
			p.LEFT = true;
		}

		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			p.RIGHT = false;
			if(p.dashSpeed>0 && p.punch==0)
				p.stopSlide();
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			p.LEFT = false;
			if(p.dashSpeed<0 && p.punch==0)
				p.stopSlide();
		}

		issue ();
	}
	
	private void faceRight(){
		p.transform.localScale = new Vector3 (-10, 10, 1);
	}

	private void faceLeft(){
		p.transform.localScale = new Vector3 (10, 10, 1);
	}
}
