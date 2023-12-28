using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.2f;
    public float speed = 10.0f;
    public float maxLifetime = 50.0f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        rb.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                if (this.size * 0.5f >= this.minSize)
                {
                    CreateSplit();
                    CreateSplit();
                }
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.AsteroidDestroyed(this);
                }
                else
                {
                    Debug.LogWarning("GameManager not found!");
                }
                Destroy(this.gameObject);
                break;
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid haft = Instantiate(this, position, this.transform.rotation);
        haft.size = this.size * 0.5f;
        haft.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
