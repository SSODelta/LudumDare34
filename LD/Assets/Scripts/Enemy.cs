using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	
	private Sprite RUN1, RUN2, RUN3, RUN4, KICK_UP, KICK_DOWN, PUNCH;
	private SpriteRenderer sr;

	private float dashSpeed = 0;

	private int ta = 0, tamax = 10;
	private int tk = 0, tp = 0;

	private bool GROUNDED = false;

	private Rigidbody2D rb;
	private Player p;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		p = GameObject.Find ("player").GetComponent<Player> ();
		sr = this.GetComponent<SpriteRenderer> ();

		RUN1       = Resources.Load <Sprite> ("Sprites/Enemies/Run0000");
		RUN2       = Resources.Load <Sprite> ("Sprites/Enemies/Run0001");
		RUN3       = Resources.Load <Sprite> ("Sprites/Enemies/Run0002");
		RUN4       = Resources.Load <Sprite> ("Sprites/Enemies/Run0003");
		PUNCH      = Resources.Load <Sprite> ("Sprites/Enemies/Punch");
		KICK_UP    = Resources.Load <Sprite> ("Sprites/Enemies/KickUp");
		KICK_DOWN  = Resources.Load <Sprite> ("Sprites/Enemies/KickDown");

		setSprite (KICK_DOWN);
		ta = Mathf.FloorToInt (Random.Range (0, tamax));


	}

	private bool isSprite(Sprite s){
		return sr.sprite.Equals (s);
	}

	private void setSprite(Sprite s){
		sr.sprite = s;
	}

	private void punch(){

		int dx = 1;
		if (transform.localScale.x < 0)
			dx = -1;
	
		dashSpeed = dx * 0.15f;

		setSprite (PUNCH);
	}

	private void jump(){
		if (!GROUNDED)
			return;

		GROUNDED = false;
		setSprite (KICK_UP);

		int dx = 1;
		if (transform.localScale.x < 0)
			dx = -1;

		rb.AddForce (new Vector2 (dx * 150, 575));
	}

	private float dist(){
		float dx = p.transform.position.x - transform.position.x,
	   	      dy = p.transform.position.y - transform.position.y;

		return Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (GROUNDED) return;
		GROUNDED = true;	
		setSprite (RUN1);
	}

	private const float MAX_SPEED = 5;
	// Update is called once per frame
	void Update () {

		if (isSprite (KICK_UP) && rb.velocity.y < 0)
			setSprite (KICK_DOWN);

		if (tk > 0)
			tk--;
		
		if (tp > 0) {
			tp--;
			if(tp == 0)
				setSprite(RUN1);
		}

		if (dist () < 5 && tk == 0 && tp == 0 && GROUNDED) {
			tk = 300;
			jump();
		}

		if (dist () < 3 && tk < 170 && tp == 0 && GROUNDED) {
			tp = 160;
			punch ();
		}

		if (GROUNDED && ++ta >= tamax) {
			ta = 0;

			if(isSprite(RUN1) || isSprite(KICK_DOWN) || isSprite(KICK_UP) || isSprite(PUNCH)){
				setSprite(RUN2);
				tamax+=5;
			} else if(isSprite(RUN2)){
				setSprite(RUN3);
				tamax-=5;
			} else if(isSprite(RUN3)){
				tamax+=5;
				setSprite(RUN4);
			} else if(isSprite(RUN4)){
				setSprite(RUN1);
				tamax-=5;
			}

		}



		if(rb.velocity.magnitude > MAX_SPEED)
			rb.velocity *= MAX_SPEED / rb.velocity.magnitude;

		if (dashSpeed != 0) {

			setSprite (PUNCH);

			dashSpeed *= 0.95f;
			rb.AddForce (100 * new Vector2 (dashSpeed, 0));
			if (Mathf.Abs (dashSpeed) < 0.01)
				dashSpeed = 0;

		} else {

			if (p.transform.position.x > rb.position.x) {
				rb.AddForce (new Vector2 (10, 0));
				transform.localScale = new Vector3(10,10,1);
			} else {
				rb.AddForce (new Vector2(-10,0));
				transform.localScale = new Vector3(-10,10,1);
			}
		
		}
	}
}
