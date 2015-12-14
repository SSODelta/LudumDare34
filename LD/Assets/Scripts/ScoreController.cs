using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScoreController : MonoBehaviour {

	private HashSet<GameObject> list = null;

	private Sprite NUM_0, NUM_1, NUM_2, NUM_3, NUM_4, NUM_5, NUM_6, NUM_7, NUM_8, NUM_9;

	// Use this for initialization
	void Start () {
		list = new HashSet<GameObject>();

		Sprite[] sprites = Resources.LoadAll<Sprite> ("Sprites/Points");
		foreach(Sprite s in sprites){
			if(s.name.Equals("Num0"))
				NUM_0 = s;
			if(s.name.Equals("Num1"))
				NUM_1 = s;
			if(s.name.Equals("Num2"))
				NUM_2 = s;
			if(s.name.Equals("Num3"))
				NUM_3 = s;
			if(s.name.Equals("Num4"))
				NUM_4 = s;
			if(s.name.Equals("Num5"))
				NUM_5 = s;
			if(s.name.Equals("Num6"))
				NUM_6 = s;
			if(s.name.Equals("Num7"))
				NUM_7 = s;
			if(s.name.Equals("Num8"))
				NUM_8 = s;
			if(s.name.Equals("Num9"))
				NUM_9 = s;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private const float offset = 2.45f;
	public void drawNumber(int num){
		erase ();
		string s = "" + num;
		printLetters (s, transform.position.x+offset, transform.position.y);
	}

	private void erase(){
		foreach(GameObject go in list){
			Destroy(go);
		}
	}

	private void addDigit(Sprite s, float x, float y){
		GameObject go = (GameObject)(GameObject)Instantiate (Resources.Load ("Num"));
		SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
		sr.sprite = s;
		go.transform.position = new Vector2 (x,y);
		list.Add (go);
		go.transform.localScale = new Vector3 (5f, 5f, 0);
		go.transform.parent = transform;
	}

	private const float margin = 0.06f;
	private const float baseUnit = 0.06f;
	private void printLetters(string letters, float x, float y){
		if (letters.StartsWith ("0")) {
			addDigit(NUM_0, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*9, y);
		} else if (letters.StartsWith ("1")) {
			addDigit(NUM_1, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*2, y);
		} else if (letters.StartsWith ("2")) {
			addDigit(NUM_2, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*10, y);
		} else if (letters.StartsWith ("3")) {
			addDigit(NUM_3, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*11, y);
		} else if (letters.StartsWith ("4")) {
			addDigit(NUM_4, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*10, y);
		} else if (letters.StartsWith ("5")) {
			addDigit(NUM_5, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*10, y);
		} else if (letters.StartsWith ("6")) {
			addDigit(NUM_6, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*10, y);
		} else if (letters.StartsWith ("7")) {
			addDigit(NUM_7, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*10, y);
		} else if (letters.StartsWith ("8")) {
			addDigit(NUM_8, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*10, y);
		} else if (letters.StartsWith ("9")) {
			addDigit(NUM_9, x, y);
			printLetters(letters.Substring(1), x+margin+baseUnit*10, y);
		} 
	}
}
