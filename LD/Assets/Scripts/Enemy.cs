using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	
	private Sprite RUN1, RUN2, RUN3, RUN4, KICK_UP, KICK_DOWN, PUNCH, DEAD, HURT_UP, HURT_DOWN;
	private SpriteRenderer sr;

	private float dashSpeed = 0;

	private int ta = 0, tamax = 10;
	private int tk = 0, tp = 0;

    public int attack = 0;

	private bool GROUNDED = false, ALIVE =true;

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
		HURT_UP    = Resources.Load <Sprite> ("Sprites/Enemies/HurtUp");
		HURT_DOWN  = Resources.Load <Sprite> ("Sprites/Enemies/HurtDown");
		DEAD  = Resources.Load <Sprite> ("Sprites/Enemies/Dead");

		setSprite (KICK_DOWN);
		ta = Mathf.FloorToInt (Random.Range (0, tamax));


	}

	public void kill(int dx){
		if (!ALIVE)
			return;
		setSprite (HURT_UP);
		rb.AddForce (new Vector2 (dx * 150, 575));
		ALIVE = false;
        Destroy(GetComponent<BoxCollider2D>());
    }

	private bool isSprite(Sprite s){
		return sr.sprite.Equals (s);
	}

	private void setSprite(Sprite s){
		sr.sprite = s;
	}

	private void punch(){
		p.ci.playSound(p.ci.aWOOSH);
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
		if (GROUNDED || coll.collider.name.Contains("layer")) return;
		GROUNDED = true;	

		if (ALIVE) {
			setSprite (RUN1);
		} else {
            Debug.Log("deaded him");
			setSprite(DEAD);

        }
	}

	private const float MAX_SPEED = 5;
    // Update is called once per frame
    void Update() {

        if (isSprite(PUNCH) || isSprite(KICK_DOWN) || isSprite(KICK_UP))
        {
            attack = Mathf.FloorToInt(Mathf.Sign(rb.velocity.x));
        }
        else attack = 0;

        if (transform.position.y < -4.5f) {
            setSprite(DEAD);
            rb.velocity = new Vector2(0,0);
            transform.position = new Vector2(transform.position.x, -4.5f);
            return;
        }

		if (isSprite (HURT_UP) && rb.velocity.y < 0)
			setSprite (HURT_DOWN);

		if (!ALIVE)
			return;

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

		if (++ta >= tamax && GROUNDED ) {
			ta = 0;

			if(isSprite(KICK_DOWN) || isSprite(KICK_UP) || isSprite(PUNCH))
				setSprite (RUN2);

			if(isSprite(RUN1)){
				setSprite(RUN2);
				tamax=10;
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

        if (rb == null) return;
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
