﻿using UnityEngine;
using System.Collections;

public class CommandIssuer : MonoBehaviour {

	private int t = 0, tmax = 20;
	private Player p;
	private Command cmd;

	public GameController gc;

	private int ts = 0, tsmax = 50;
	private int tw = 0, twmax = 13;
	public  int th = 0;

    private int headband = 0;

	private static int LAST_SCORE = 0;
    private static int PROGRESS = 0;

	private ScoreController sc, sc_menu;

	private GameObject ko, black, menu;

	private SpriteRenderer sr_black, sr_menu;
	private float menuAlpha=0;

	private SpriteRenderer btnLeft, btnRight;

	private float blackAlpha = 1.5f;
	public bool LOADING = true, MENU = true;

	private SpriteRenderer sr, sr_credits;
	public Sprite NEW_MENU, BTN_OFF, BTN_ON;

    public Sprite STAND_1, STAND_2, KICK_FLY, KICK_SLIDE, PUNCH, DEAD, HURT, WALK1, WALK2, WALK3, WALK4, WALK5, WALK6;
    public Sprite GOLD_STAND_1, GOLD_STAND_2, GOLD_KICK_FLY, GOLD_KICK_SLIDE, GOLD_PUNCH, GOLD_DEAD, GOLD_HURT, GOLD_WALK1, GOLD_WALK2, GOLD_WALK3, GOLD_WALK4, GOLD_WALK5, GOLD_WALK6;
    public Sprite GREEN_STAND_1, GREEN_STAND_2, GREEN_KICK_FLY, GREEN_KICK_SLIDE, GREEN_PUNCH, GREEN_DEAD, GREEN_HURT, GREEN_WALK1, GREEN_WALK2, GREEN_WALK3, GREEN_WALK4, GREEN_WALK5, GREEN_WALK6;
    public Sprite PURPLE_STAND_1, PURPLE_STAND_2, PURPLE_KICK_FLY, PURPLE_KICK_SLIDE, PURPLE_PUNCH, PURPLE_DEAD, PURPLE_HURT, PURPLE_WALK1, PURPLE_WALK2, PURPLE_WALK3, PURPLE_WALK4, PURPLE_WALK5, PURPLE_WALK6;

    public Sprite HP6, HP5, HP4, HP3, HP2, HP1;

	public AudioClip aSTART, aPUNCH, aPUNCH_1, aPUNCH_2, aKICK, aKICK_1, aWALK_1, aWOOSH, aGONG, aREADY, aSPLAT, aOW;
	public AudioClip mLOW, mNORMAL;
	public AudioClip aD1, aD2, aD3, aD4, aD5, aD6, aD7;  

	private string[] cmds;
	private AudioSource source;
	public AudioSource bgmusic, rattles;
	private int kills = 0;

	public SpriteRenderer health;

	public void newKill(){
		sc.drawNumber (++kills);
	}

	// Use this for initialization
	void Start () {

        Camera.main.aspect = 2.25f;

        if (LAST_SCORE >= 10) PROGRESS = Mathf.Max(1, PROGRESS);
        if (LAST_SCORE >= 25) PROGRESS = Mathf.Max(2, PROGRESS);
        if (LAST_SCORE >= 55) PROGRESS = Mathf.Max(3, PROGRESS);

        headband = PROGRESS;

        GameObject go_scmenu = GameObject.Find ("scMenu");
        Time.timeScale = 1f;
		gc = GameObject.Find ("gc").GetComponent<GameController> ();
		sc = GameObject.Find ("kills").GetComponent<ScoreController> ();
		sc_menu = go_scmenu.GetComponent<ScoreController> ();
		btnLeft  = GameObject.Find ("btnLeft").GetComponent<SpriteRenderer> ();
		health = GameObject.Find ("health").GetComponent<SpriteRenderer> ();
		btnRight = GameObject.Find ("btnRight").GetComponent<SpriteRenderer> ();
        sr_credits = GameObject.Find("Credits").GetComponent<SpriteRenderer>();
        setAlpha(sr_credits, 0f);
        rattles = GetComponent<AudioSource> ();

		sc_menu.ax = 1;

		menu = (GameObject)GameObject.Find ("menu");
		Vector3 v = menu.transform.position;
		v.z = -6;
		menu.transform.position = v;
		sr_menu = menu.GetComponent<SpriteRenderer> ();
        
		 v = go_scmenu.transform.position;
		v.z = 7;
		go_scmenu.transform.position = v;

		black = (GameObject)GameObject.Find ("black");

		v = black.transform.position;
		v.z = -5;
		black.transform.position = v;
		sr_black = black.GetComponent<SpriteRenderer> ();

        //--


        v = health.transform.position;
        v.z = 1000;
        health.transform.position = v;

        ko = (GameObject)GameObject.Find ("KO_0");
		
		cmd = new Command ();
		cmds = new string[]{"l","r","ll","rr","lrl","rlr"};

        NEW_MENU   = Resources.Load <Sprite> ("Sprites/LDMenu");


        STAND_1    = Resources.Load <Sprite> ("Sprites/Stand1");
		STAND_2    = Resources.Load <Sprite> ("Sprites/Stand2");
		KICK_FLY   = Resources.Load <Sprite> ("Sprites/FlyingKick");
		KICK_SLIDE = Resources.Load <Sprite> ("Sprites/SlideKick");
		PUNCH      = Resources.Load <Sprite> ("Sprites/FistPunch");
		DEAD       = Resources.Load <Sprite> ("Sprites/Dead");
		HURT       = Resources.Load <Sprite> ("Sprites/Hurt");
		WALK1      = Resources.Load <Sprite> ("Sprites/Walk1_1");
		WALK2      = Resources.Load <Sprite> ("Sprites/Walk2_1");
		WALK3      = Resources.Load <Sprite> ("Sprites/Walk3_1");
		WALK4      = Resources.Load <Sprite> ("Sprites/Walk4_1");
		WALK5      = Resources.Load <Sprite> ("Sprites/Walk5_1");
		WALK6      = Resources.Load <Sprite> ("Sprites/Walk6_1");

        GREEN_STAND_1 = Resources.Load<Sprite>("Sprites/GreenPlayer/Stand1");
        GREEN_STAND_2 = Resources.Load<Sprite>("Sprites/GreenPlayer/Stand2");
        GREEN_KICK_FLY = Resources.Load<Sprite>("Sprites/GreenPlayer/FlyingKick");
        GREEN_KICK_SLIDE = Resources.Load<Sprite>("Sprites/GreenPlayer/SlideKick");
        GREEN_PUNCH = Resources.Load<Sprite>("Sprites/GreenPlayer/FistPunch");
        GREEN_DEAD = Resources.Load<Sprite>("Sprites/GreenPlayer/Dead");
        GREEN_HURT = Resources.Load<Sprite>("Sprites/GreenPlayer/Hurt");
        GREEN_WALK1 = Resources.Load<Sprite>("Sprites/GreenPlayer/Walk1_1");
        GREEN_WALK2 = Resources.Load<Sprite>("Sprites/GreenPlayer/Walk2_1");
        GREEN_WALK3 = Resources.Load<Sprite>("Sprites/GreenPlayer/Walk3_1");
        GREEN_WALK4 = Resources.Load<Sprite>("Sprites/GreenPlayer/Walk4_1");
        GREEN_WALK5 = Resources.Load<Sprite>("Sprites/GreenPlayer/Walk5_1");
        GREEN_WALK6 = Resources.Load<Sprite>("Sprites/GreenPlayer/Walk6_1");

        GOLD_STAND_1 = Resources.Load<Sprite>("Sprites/GoldPlayer/Stand1");
        GOLD_STAND_2 = Resources.Load<Sprite>("Sprites/GoldPlayer/Stand2");
        GOLD_KICK_FLY = Resources.Load<Sprite>("Sprites/GoldPlayer/FlyingKick");
        GOLD_KICK_SLIDE = Resources.Load<Sprite>("Sprites/GoldPlayer/SlideKick");
        GOLD_PUNCH = Resources.Load<Sprite>("Sprites/GoldPlayer/FistPunch");
        GOLD_DEAD = Resources.Load<Sprite>("Sprites/GoldPlayer/Dead");
        GOLD_HURT = Resources.Load<Sprite>("Sprites/GoldPlayer/Hurt");
        GOLD_WALK1 = Resources.Load<Sprite>("Sprites/GoldPlayer/Walk1_1");
        GOLD_WALK2 = Resources.Load<Sprite>("Sprites/GoldPlayer/Walk2_1");
        GOLD_WALK3 = Resources.Load<Sprite>("Sprites/GoldPlayer/Walk3_1");
        GOLD_WALK4 = Resources.Load<Sprite>("Sprites/GoldPlayer/Walk4_1");
        GOLD_WALK5 = Resources.Load<Sprite>("Sprites/GoldPlayer/Walk5_1");
        GOLD_WALK6 = Resources.Load<Sprite>("Sprites/GoldPlayer/Walk6_1");
        
        PURPLE_STAND_1 = Resources.Load<Sprite>("Sprites/PurplePlayer/Stand1");
        PURPLE_STAND_2 = Resources.Load<Sprite>("Sprites/PurplePlayer/Stand2");
        PURPLE_KICK_FLY = Resources.Load<Sprite>("Sprites/PurplePlayer/FlyingKick");
        PURPLE_KICK_SLIDE = Resources.Load<Sprite>("Sprites/PurplePlayer/SlideKick");
        PURPLE_PUNCH = Resources.Load<Sprite>("Sprites/PurplePlayer/FistPunch");
        PURPLE_DEAD = Resources.Load<Sprite>("Sprites/PurplePlayer/Dead");
        PURPLE_HURT = Resources.Load<Sprite>("Sprites/PurplePlayer/Hurt");
        PURPLE_WALK1 = Resources.Load<Sprite>("Sprites/PurplePlayer/Walk1_1");
        PURPLE_WALK2 = Resources.Load<Sprite>("Sprites/PurplePlayer/Walk2_1");
        PURPLE_WALK3 = Resources.Load<Sprite>("Sprites/PurplePlayer/Walk3_1");
        PURPLE_WALK4 = Resources.Load<Sprite>("Sprites/PurplePlayer/Walk4_1");
        PURPLE_WALK5 = Resources.Load<Sprite>("Sprites/PurplePlayer/Walk5_1");
        PURPLE_WALK6 = Resources.Load<Sprite>("Sprites/PurplePlayer/Walk6_1");

        HP1 = Resources.Load<Sprite> ("Sprites/HP/1_HP");
		HP2 = Resources.Load<Sprite> ("Sprites/HP/2_HP");
		HP3 = Resources.Load<Sprite> ("Sprites/HP/3_HP");
        HP4 = Resources.Load<Sprite> ("Sprites/HP/4_HP");
        HP5 = Resources.Load<Sprite> ("Sprites/HP/5_HP");
        HP6 = Resources.Load<Sprite> ("Sprites/HP/6_HP");

        BTN_OFF    = Resources.Load <Sprite> ("Sprites/ButtonUp");
		BTN_ON     = Resources.Load <Sprite> ("Sprites/ButtonDown");
		
		aPUNCH      = (Resources.Load("Sounds/Punch") as AudioClip);
		aPUNCH_1    = (Resources.Load("Sounds/Punch1") as AudioClip);
		aPUNCH_2    = (Resources.Load("Sounds/Punch2") as AudioClip);
		aKICK       = (Resources.Load("Sounds/Kick") as AudioClip);
		aKICK_1     = (Resources.Load("Sounds/Kick1") as AudioClip);
		aWALK_1     = (Resources.Load("Sounds/Walk1") as AudioClip);
		aWOOSH      = (Resources.Load("Sounds/Woosh") as AudioClip);
		aGONG       = (Resources.Load("Sounds/gong") as AudioClip);
        aSTART      = (Resources.Load("Sounds/Start") as AudioClip);
        aREADY      = (Resources.Load("Sounds/READY") as AudioClip);
		aSPLAT      = (Resources.Load("Sounds/Splat") as AudioClip);
		aOW         = (Resources.Load("Sounds/Oww") as AudioClip);

		aD1 = Resources.Load<AudioClip> ("Sounds/Death1");
		aD2 = Resources.Load<AudioClip> ("Sounds/Death2");
		aD3 = Resources.Load<AudioClip> ("Sounds/Death3");
		aD4 = Resources.Load<AudioClip> ("Sounds/Death4");
		aD5 = Resources.Load<AudioClip> ("Sounds/Death5");
		aD6 = Resources.Load<AudioClip> ("Sounds/Death6");
		aD7 = Resources.Load<AudioClip> ("Sounds/Death7");
		
		mLOW        = (Resources.Load("Sounds/BackgroundMusic_LowHP") as AudioClip);
		mNORMAL     = (Resources.Load("Sounds/BackgroundMusic_Normal") as AudioClip);

		hideKO ();
	}

	public void playDeathRattle(){
		int i = Random.Range (0, 6);

		if (i == 0) playSound (aD1, rattles);
		if (i == 1) playSound (aD2, rattles);
		if (i == 2) playSound (aD3, rattles);
		if (i == 3) playSound (aD4, rattles);
		if (i == 4) playSound (aD5, rattles);
		if (i == 5) playSound (aD6, rattles);
		if (i == 6) playSound (aD7, rattles);
	}

	public void playSound(AudioClip a){
		playSound (a, source);
	}

	public void playSound(AudioClip a, AudioSource src){
		src.clip = a;
		src.Play ();
	}

	public bool isSprite(Sprite s){
		return sr.sprite.Equals (s);
	}

	public void setSprite(Sprite s){
		sr.sprite = s;
	}

	public void punch(){
		playSound (aPUNCH);
		p.punch = 30;
		setSprite (PUNCH);
		p.dashSpeed = p.transform.localScale.x < 0 ? 0.15f : -0.15f;
	}

	public void setPlayer(Player pl){
		this.p = pl;
		this.sr = p.GetComponent<SpriteRenderer> ();
		this.source = p.GetComponent<AudioSource> ();
		bgmusic = (GameObject.Find ("Main Camera")).GetComponent<AudioSource> ();
		bgmusic.clip = mNORMAL;
		bgmusic.loop = true;
	}

	public void showKO(){
		ko.transform.position = new Vector3 (ko.transform.position.x, ko.transform.position.y, -1);
	}
	public void hideKO(){
		ko.transform.position = new Vector3 (ko.transform.position.x, ko.transform.position.y, 2);
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
			
			if(cmd.isCmd("rr") || cmd.isCmd("ll") && p.punch==0)
				setSprite(KICK_SLIDE);

			if(cmd.isCmd("rlr") || cmd.isCmd("lrl") && p.punch==0)
				setSprite(KICK_FLY);

			p.issueCommand (cmd);
			cmd = new Command ();
		}
	}

    private void giveHeadband()
    {
        if (isSprite(STAND_1)    && headband == 1)  setSprite(GREEN_STAND_1);
        if (isSprite(STAND_2)    && headband == 1)  setSprite(GREEN_STAND_2);
        if (isSprite(KICK_FLY)   && headband == 1)  setSprite(GREEN_KICK_FLY);
        if (isSprite(KICK_SLIDE) && headband == 1)  setSprite(GREEN_KICK_SLIDE);
        if (isSprite(PUNCH)      && headband == 1)  setSprite(GREEN_PUNCH);
        if (isSprite(DEAD)       && headband == 1)  setSprite(GREEN_DEAD);
        if (isSprite(HURT)       && headband == 1)  setSprite(GREEN_HURT);
        if (isSprite(WALK1)      && headband == 1)  setSprite(GREEN_WALK1);
        if (isSprite(WALK2)      && headband == 1)  setSprite(GREEN_WALK2);
        if (isSprite(WALK3)      && headband == 1)  setSprite(GREEN_WALK3);
        if (isSprite(WALK4)      && headband == 1)  setSprite(GREEN_WALK4);
        if (isSprite(WALK5)      && headband == 1)  setSprite(GREEN_WALK5);
        if (isSprite(WALK6)      && headband == 1)  setSprite(GREEN_WALK6);
        
        if (isSprite(STAND_1)    && headband == 2)  setSprite(PURPLE_STAND_1);
        if (isSprite(STAND_2)    && headband == 2)  setSprite(PURPLE_STAND_2);
        if (isSprite(KICK_FLY)   && headband == 2)  setSprite(PURPLE_KICK_FLY);
        if (isSprite(KICK_SLIDE) && headband == 2)  setSprite(PURPLE_KICK_SLIDE);
        if (isSprite(PUNCH)      && headband == 2)  setSprite(PURPLE_PUNCH);
        if (isSprite(DEAD)       && headband == 2)  setSprite(PURPLE_DEAD);
        if (isSprite(HURT)       && headband == 2)  setSprite(PURPLE_HURT);
        if (isSprite(WALK1)      && headband == 2)  setSprite(PURPLE_WALK1);
        if (isSprite(WALK2)      && headband == 2)  setSprite(PURPLE_WALK2);
        if (isSprite(WALK3)      && headband == 2)  setSprite(PURPLE_WALK3);
        if (isSprite(WALK4)      && headband == 2)  setSprite(PURPLE_WALK4);
        if (isSprite(WALK5)      && headband == 2)  setSprite(PURPLE_WALK5);
        if (isSprite(WALK6)      && headband == 2)  setSprite(PURPLE_WALK6);
        
        if (isSprite(STAND_1)    && headband == 3)  setSprite(GOLD_STAND_1);
        if (isSprite(STAND_2)    && headband == 3)  setSprite(GOLD_STAND_2);
        if (isSprite(KICK_FLY)   && headband == 3)  setSprite(GOLD_KICK_FLY);
        if (isSprite(KICK_SLIDE) && headband == 3)  setSprite(GOLD_KICK_SLIDE);
        if (isSprite(PUNCH)      && headband == 3)  setSprite(GOLD_PUNCH);
        if (isSprite(DEAD)       && headband == 3)  setSprite(GOLD_DEAD);
        if (isSprite(HURT)       && headband == 3)  setSprite(GOLD_HURT);
        if (isSprite(WALK1)      && headband == 3)  setSprite(GOLD_WALK1);
        if (isSprite(WALK2)      && headband == 3)  setSprite(GOLD_WALK2);
        if (isSprite(WALK3)      && headband == 3)  setSprite(GOLD_WALK3);
        if (isSprite(WALK4)      && headband == 3)  setSprite(GOLD_WALK4);
        if (isSprite(WALK5)      && headband == 3)  setSprite(GOLD_WALK5);
        if (isSprite(WALK6)      && headband == 3)  setSprite(GOLD_WALK6);
    }

	public void idle(){
		setSprite (STAND_2);
		p.dashSpeed = 0;
	}

	public void hurt(int dx){
		playSound(aOW, rattles);
		playSound (aPUNCH_1);
		th = 45;
		setSprite (HURT);
		if (--p.health == 0) {
			LAST_SCORE = kills;
			p.kill ();
			Destroy (health);
		}

        if (p.health == 5)
        {
            health.sprite = HP5;
        }
        else if (p.health == 4)
        {
            health.sprite = HP4;
        }
        else if (p.health == 3)
        {
            health.sprite = HP3;
        }
        else if (p.health == 2)
        {
            health.sprite = HP2;
        }
        else if (p.health == 1)
        {
            bgmusic.clip = mLOW;
            bgmusic.Play();
            health.sprite = HP1;
        } 

	}
	private void setAlpha(SpriteRenderer s, float alpha){
		Color tmp = s.color;
		tmp.a = alpha;
		s.color = tmp;
	}

	void hideMenu(){
		MENU = false;
		sc_menu.erase ();
		Vector3 v = menu.transform.position;
		v.z = 90;
		menu.transform.position = v;
        //--

        v = health.transform.position;
        v.z = 1;
        health.transform.position = v;
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();

        giveHeadband();

        if (MENU && Input.GetKeyUp (KeyCode.LeftArrow)) {
			hideMenu ();
			gc.HARD=false;
            playSound(aSTART);
            p.health = 6;
            health.sprite = HP6;
		}
		if (MENU && Input.GetKeyUp (KeyCode.RightArrow)) {
			hideMenu ();
			gc.HARD=true;
            playSound(aSTART);
            p.health = 3;
            health.sprite = HP3;
        }

		setAlpha (sr_black, blackAlpha);
		setAlpha (sr_menu,  menuAlpha);

		if (LOADING) {
			blackAlpha-=0.01f;
			if(blackAlpha < 0.6f && MENU){
				menuAlpha +=0.02f;
				blackAlpha=0.6f;
				sc_menu.setAlpha(menuAlpha);
                setAlpha(sr_credits,menuAlpha);
                if (sc_menu.empty() && !sr_menu.sprite.Equals(NEW_MENU))
                {
                    sc_menu.setAlpha(0f);
                    if (LAST_SCORE != 0)
                    {
                        sc_menu.drawNumber(LAST_SCORE);
                    } else
                    {
                        sr_menu.sprite = NEW_MENU;
                    }
				}
			}
			if(blackAlpha<=0){
				blackAlpha=0;
				LOADING=false;
			}
		}

		if (p.DEAD) {
			blackAlpha += 0.0045f;
			if(blackAlpha>1.4)
				Application.LoadLevel(0);
		}

	
		if (gc.PAUSED)
			return;

		if (th > 0) {
			p.LEFT = false;
			p.RIGHT = false;
			th--;
			setSprite (HURT);
            giveHeadband();
            return;
		}

		if (p.LEFT && p.transform.localScale.x < 0)
			faceLeft ();
		
		if (p.RIGHT && p.transform.localScale.x > 0)
			faceRight ();

		if (!p.LEFT && !p.RIGHT && ++ts >= tsmax) {
			ts=0;

			if(isSprite(STAND_1) || isSprite (WALK1)  || isSprite (WALK2) || isSprite (WALK3) || isSprite (WALK4) || isSprite (WALK5) || isSprite (WALK6)
                || isSprite(GREEN_STAND_1) || isSprite(GREEN_WALK1) || isSprite(GREEN_WALK2) || isSprite(GREEN_WALK3) || isSprite(GREEN_WALK4) || isSprite(GREEN_WALK5) || isSprite(GREEN_WALK6)
                || isSprite(PURPLE_STAND_1) || isSprite(PURPLE_WALK1) || isSprite(PURPLE_WALK2) || isSprite(PURPLE_WALK3) || isSprite(PURPLE_WALK4) || isSprite(PURPLE_WALK5) || isSprite(PURPLE_WALK6)
                || isSprite(GOLD_STAND_1) || isSprite(GOLD_WALK1) || isSprite(GOLD_WALK2) || isSprite(GOLD_WALK3) || isSprite(GOLD_WALK4) || isSprite(GOLD_WALK5) || isSprite(GOLD_WALK6)) {
				setSprite(STAND_2);
			} else if(isSprite(STAND_2)|| isSprite (HURT) || isSprite (KICK_FLY)
                || isSprite(GREEN_STAND_2) || isSprite(GREEN_HURT) || isSprite(GREEN_KICK_FLY)
                || isSprite(PURPLE_STAND_2) || isSprite(PURPLE_HURT) || isSprite(PURPLE_KICK_FLY)
                || isSprite(GOLD_STAND_2) || isSprite(GOLD_HURT) || isSprite(GOLD_KICK_FLY)) {
				setSprite(STAND_1);}
		}

		if ((p.LEFT || p.RIGHT) && p.dashSpeed==0 && ++tw >= twmax) {
			tw=0;

			if(isSprite (STAND_2) || isSprite (HURT) || isSprite(GREEN_STAND_2) || isSprite(GREEN_HURT) || isSprite(PURPLE_STAND_2) || isSprite(PURPLE_HURT) || isSprite(GOLD_STAND_2) || isSprite(GOLD_HURT))
            {
				setSprite (STAND_1);
			} else if(isSprite(STAND_1) || isSprite(GOLD_STAND_1) || isSprite(PURPLE_STAND_1) || isSprite(GREEN_STAND_1))
            {
				setSprite (WALK1);
			} else if(isSprite(WALK1) || isSprite(GOLD_WALK1) || isSprite(PURPLE_WALK1) || isSprite(GREEN_WALK1))
            {
				setSprite (WALK2);
            }
            else if (isSprite(WALK2) || isSprite(GOLD_WALK2) || isSprite(PURPLE_WALK2) || isSprite(GREEN_WALK2))
            {
                setSprite (WALK3);
            }
            else if (isSprite(WALK3) || isSprite(GOLD_WALK3) || isSprite(PURPLE_WALK3) || isSprite(GREEN_WALK3))
            {
                setSprite (WALK4);
            }
            else if (isSprite(WALK4) || isSprite(GOLD_WALK4) || isSprite(PURPLE_WALK4) || isSprite(GREEN_WALK4))
            {
                setSprite (WALK5);
            }
            else if (isSprite(WALK5) || isSprite(GOLD_WALK5) || isSprite(PURPLE_WALK5) || isSprite(GREEN_WALK5))
            {
                setSprite (WALK6);
            }
            else if (isSprite(WALK6) || isSprite(GOLD_WALK6) || isSprite(PURPLE_WALK6) || isSprite(GREEN_WALK6))
            {
                setSprite (STAND_1);
			}
		}

		if (p.LEFT) {
			btnLeft.sprite = BTN_ON;
			setAlpha (btnLeft, 1);
		} else {
			btnLeft.sprite = BTN_OFF;
			setAlpha (btnLeft, 0.5f);
		}

		
		if (p.RIGHT) {
			btnRight.sprite = BTN_ON;
			setAlpha (btnRight, 1);
		} else {
			btnRight.sprite = BTN_OFF;
			setAlpha (btnRight, 0.5f);
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
