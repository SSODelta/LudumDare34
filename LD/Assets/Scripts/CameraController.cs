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

        if(p.transform.position.x > transform.position.x)
        {
            dx += 0.01f;
        } else
        {
            dx -= 0.01f;
        }

        dx *= 0.775f;

        float newx = transform.position.x + dx;

        if (newx < -1.88f) newx = -1.88f;
        if (newx >  8.85f) newx = 8.85f;

        transform.position = new Vector3(newx,
                                         transform.position.y,
                                         transform.position.z);
	}
}
