using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("gc").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(!gc.PAUSED && Mathf.FloorToInt(Random.Range(0,500))==1)spawnEnemy();
	}

	public void spawnEnemy(){
		GameObject enemy = (GameObject)Instantiate (Resources.Load ("Enemy"));
		enemy.transform.position = transform.position;
	}
}
