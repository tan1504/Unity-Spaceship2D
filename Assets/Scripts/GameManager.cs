using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem exposion;
    public int lives = 3;
    private int score = 0;
    public float respawnRate = 3.0f;
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.exposion.transform.position = asteroid.transform.position;
        this.exposion.Play();

        // TODO: Increase Your score
        if (asteroid.size <= 0.7)
        {
            this.score += 3;
            scoreText.text = "Score" + " " + this.score;
        }
        else if (asteroid.size <= 1.0)
        {
            this.score += 5;
            scoreText.text = "Score" + " " + this.score;
        }
        else
        {
            this.score += 7;
            scoreText.text = "Score" + " " + this.score;
        }
    }

    public void PlayerDied()
    {
        this.exposion.transform.position = this.player.transform.position;
        this.exposion.Play();

        this.lives--;
        livesText.text = "x" + this.lives;
        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnRate);
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(DelayResetLayer), 3.0f);
    }

    private void DelayResetLayer()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
