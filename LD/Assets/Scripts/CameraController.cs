using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Player p;

	// Use this for initialization
	void Start ()
    {
        p = GameObject.Find("player").GetComponent<Player>();
    }

    float dx = 0;

	// Update is called once per frame
	void Update () {

		if (Mathf.Abs (p.transform.position.x - transform.position.x) > 1f) {
			dx += (p.transform.position.x - transform.position.x) / 20f;
		}
		
		dx *= 0.7f;

        float newx = transform.position.x + dx;

        if (newx < -1.25f) newx = -1.25f;
        if (newx >  8.30f) newx = 8.30f;

        transform.position = new Vector3(newx,
                                         transform.position.y,
                                         transform.position.z);
	}
}
