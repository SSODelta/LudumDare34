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

        transform.position = new Vector3(transform.position.x + dx,
                                         transform.position.y,
                                         transform.position.z);
	}
}
