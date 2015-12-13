using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.FloorToInt(Random.Range(0,100))==1)spawnEnemy();
	}

	public void spawnEnemy(){
		GameObject enemy = (GameObject)Instantiate (Resources.Load ("Enemy"));
		Enemy e = enemy.GetComponent<Enemy> ();
		enemy.transform.position = transform.position;
	}
}
