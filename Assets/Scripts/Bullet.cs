using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 500.0f;
    public float maxLifetime = 5.0f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        rb.AddForce(direction * this.Speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                Destroy(this.gameObject);
                break;
            case "Asteroid":
                Destroy(this.gameObject);
                break;
        }
    }
}
