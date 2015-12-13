using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	
	private Sprite RUN1, RUN2, RUN3, RUN4;
	private SpriteRenderer sr;
	
	private int ta = 0, tamax = 10;

	private Rigidbody2D rb;
	private Player p;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		p = GameObject.Find ("player").GetComponent<Player> ();
		sr = this.GetComponent<SpriteRenderer> ();

		RUN1    = Resources.Load <Sprite> ("Sprites/Enemies/Run0000");
		RUN2    = Resources.Load <Sprite> ("Sprites/Enemies/Run0001");
		RUN3    = Resources.Load <Sprite> ("Sprites/Enemies/Run0002");
		RUN4    = Resources.Load <Sprite> ("Sprites/Enemies/Run0003");

		int rand = Mathf.FloorToInt (Random.Range(0,4)*0.99f);
		switch (rand) {
			case 1:
				setSprite(RUN2);
				break;
			case 2:
				setSprite(RUN3);
				tamax++;
				break;
			case 3:
				setSprite(RUN4);
				tamax--;
				break;
		}

		ta = Mathf.FloorToInt (Random.Range (0, tamax));
	}

	private bool isSprite(Sprite s){
		return sr.sprite.Equals (s);
	}

	private void setSprite(Sprite s){
		sr.sprite = s;
	}


	private const float MAX_SPEED = 5;
	// Update is called once per frame
	void Update () {

		
		if (++ta >= tamax) {
			ta = 0;

			if(isSprite(RUN1)){
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

		if (p.transform.position.x > rb.position.x) {
			rb.AddForce (new Vector2 (10, 0));
			transform.localScale = new Vector3(10,10,1);
		} else {
			rb.AddForce (new Vector2(-10,0));
			transform.localScale = new Vector3(-10,10,1);
		}

		if(rb.velocity.magnitude > MAX_SPEED)
			rb.velocity *= MAX_SPEED / rb.velocity.magnitude;
	}
}
