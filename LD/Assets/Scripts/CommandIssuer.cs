using UnityEngine;
using System.Collections;

public class CommandIssuer : MonoBehaviour {

	private int t = 0, tmax = 12;
	private Player p;
	private Command cmd;

	private GameController gc;

	private int ts = 0, tsmax = 50;
	private int tw = 0, twmax = 13;
	public  int th = 0;

	private ScoreController sc;

	private GameObject ko;

	private SpriteRenderer btnLeft, btnRight;

	private SpriteRenderer sr;
	public Sprite STAND_1, STAND_2, KICK_FLY, KICK_SLIDE, PUNCH, DEAD, HURT, WALK1, WALK2, WALK3, WALK4, WALK5, WALK6, BTN_OFF, BTN_ON;
	public Sprite HP3, HP2, HP1;

	public AudioClip aPUNCH, aPUNCH_1, aPUNCH_2, aKICK, aKICK_1, aWALK_1, aWOOSH, aGONG, aREADY, aSPLAT, aOW;
	public AudioClip mLOW, mNORMAL;
	public AudioClip aD1, aD2, aD3, aD4, aD5, aD6, aD7;  

	private string[] cmds;
	private AudioSource source;
	public AudioSource bgmusic, rattles;
	private int kills = 0;

	private SpriteRenderer health;

	public void newKill(){
		sc.drawNumber (++kills);
	}

	// Use this for initialization
	void Start () {
		
		gc = GameObject.Find ("gc").GetComponent<GameController> ();
		sc = GameObject.Find ("kills").GetComponent<ScoreController> ();
		btnLeft  = GameObject.Find ("btnLeft").GetComponent<SpriteRenderer> ();
		health = GameObject.Find ("health").GetComponent<SpriteRenderer> ();
		btnRight = GameObject.Find ("btnRight").GetComponent<SpriteRenderer> ();
		rattles = GetComponent<AudioSource> ();

		ko = (GameObject)GameObject.Find ("KO_0");
		
		cmd = new Command ();
		cmds = new string[]{"l","r","ll","rr","lrl","rlr"};

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

		HP1 = Resources.Load<Sprite> ("Sprites/HP/1_HP");
		HP2 = Resources.Load<Sprite> ("Sprites/HP/2_HP");
		HP3 = Resources.Load<Sprite> ("Sprites/HP/3_HP");

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

	public void idle(){
		setSprite (STAND_2);
		p.dashSpeed = 0;
	}

	public void hurt(int dx){
		playSound(aOW, rattles);
		playSound (aPUNCH_1);
		th = 45;
		setSprite (HURT);
		if (--p.health == 0)
			p.kill ();
		if (p.health == 2) {
			health.sprite = HP2;
		} else if (p.health == 1) {
			bgmusic.clip = mLOW;
			bgmusic.Play();
			health.sprite = HP1;
		} else if (p.health == 0) {
			Destroy (health);
		}

	}
	private void setAlpha(SpriteRenderer s, float alpha){
		Color tmp = s.color;
		tmp.a = alpha;
		s.color = tmp;
	}

	// Update is called once per frame
	void Update () {

		if (gc.PAUSED)
			return;

		if (th > 0) {
			p.LEFT = false;
			p.RIGHT = false;
			th--;
			setSprite (HURT);
			return;
		}

		if (p.LEFT && p.transform.localScale.x < 0)
			faceLeft ();
		
		if (p.RIGHT && p.transform.localScale.x > 0)
			faceRight ();

		if (!p.LEFT && !p.RIGHT && ++ts >= tsmax) {
			ts=0;

			if(isSprite(STAND_1) || isSprite (WALK1)  || isSprite (WALK2) || isSprite (WALK3) || isSprite (WALK4) || isSprite (WALK5) || isSprite (WALK6)){
				setSprite(STAND_2);
			} else if(isSprite(STAND_2)|| isSprite (HURT) || isSprite (KICK_FLY)) {
				setSprite(STAND_1);}
		}

		if ((p.LEFT || p.RIGHT) && p.dashSpeed==0 && ++tw >= twmax) {
			tw=0;

			if(isSprite (STAND_2) || isSprite (HURT)){
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
