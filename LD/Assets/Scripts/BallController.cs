using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }

    private const float SPEED = 1;

    public void spawn(Player p)
    {

        Vector3 pos = p.transform.position;

        float angle = Random.Range(0, 180);
        float radius = 5 + Random.Range(0, 20);

        transform.position = new Vector3(pos.x + radius * Mathf.Cos(angle),
                                         pos.y + radius * Mathf.Sin(angle),
                                         0);

        float dx = pos.x - transform.position.x,
              dy = pos.y - transform.position.y;

        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        Vector2 vel = new Vector2(dx, dy);
        vel.Normalize();
        rb.velocity = new Vector2(vel.x * SPEED,
                                  vel.y * SPEED);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
