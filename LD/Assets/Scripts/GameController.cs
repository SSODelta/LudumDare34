using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool PAUSED = true;
	private int tr = 60;
	private GameObject ready;

	// Use this for initialization
	void Start () {
		ready = (GameObject)GameObject.Find ("ready");
		setZ(2);
	}

	void setZ(float z){
		ready.transform.position = new Vector3 (ready.transform.position.x, ready.transform.position.y, z);
	}

	// Update is called once per frame
	void Update () {
		if (--tr == 0) {
			if(ready.transform.position.z>0){
				setZ(-1);
				tr = 100;
			} else {
				PAUSED = false;
				Destroy (ready);
			}
		}
	}
}
