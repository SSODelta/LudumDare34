using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


	// Use this for initialization
	void Start () {
		spawnEnemy ();
	}
	
	// Update is called once per frame
	void Update () {
		//if(Mathf.FloorToInt(Random.Range(0,500))==1)spawnEnemy();
	}

	public void spawnEnemy(){
		GameObject enemy = (GameObject)Instantiate (Resources.Load ("Enemy"));
		enemy.transform.position = transform.position;
	}
}
