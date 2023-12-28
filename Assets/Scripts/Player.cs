using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletprefab;
    private Rigidbody2D rb;
    private bool thrust;
    private float turnDirection;
    public float turnSpeed = 1.0f;
    public float force = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        thrust = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (thrust)
        {
            rb.AddForce(this.transform.up * this.force);
        }

        if (turnDirection != 0.0f)
        {
            rb.AddTorque(this.turnDirection * this.turnSpeed);
        }
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletprefab,
            this.transform.position,
            this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Asteroid":
            case "Wall":
                rb.velocity = Vector3.zero;
                rb.angularVelocity = 0.0f;
                this.gameObject.SetActive(false);
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.PlayerDied();
                }
                else
                {
                    Debug.LogWarning("GameManager not found!");
                }
                break;
        }
    }
}
