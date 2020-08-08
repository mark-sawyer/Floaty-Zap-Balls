using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public static int health;

    void Start() {
        health = 5;
        GameEvents.enemyHitBall.AddListener(loseHealth);
    }

    public void loseHealth() {
        health--;
        transform.GetChild(health).GetComponent<SpriteRenderer>().enabled = false;
        if (health == 0) {
            ScoreShower.score = GameTracker.score;
            SceneManager.LoadScene("Game Over");
        }
    }
}
