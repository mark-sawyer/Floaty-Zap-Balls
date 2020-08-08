using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static float speed = 1;
    public Rigidbody2D rb;
    public EnemyState state;
    public Animator anim;
    private float direction;
    private float timer;

    void Start() {
        state = EnemyState.CREATING;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            switch(state) {
                case EnemyState.MOVING:
                    timer = Random.Range(3, 4);
                    rb.velocity = Vector2.zero;
                    state = EnemyState.WAITING;
                    break;
                case EnemyState.WAITING:
                    direction = Random.Range(0, 2 * Mathf.PI);
                    timer = Random.Range(1.5f, 3f);
                    rb.velocity = new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * speed;
                    state = EnemyState.MOVING;
                    break;
                case EnemyState.DYING:
                    Destroy(gameObject);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameTracker.enemyCount--;
        GameTracker.score++;
        GameTracker.getNewEnemyTimerMultiplier();
        if (GameTracker.enemyCount == 0) {
            GameTracker.createNewEnemy();
        }

        Destroy(transform.GetChild(0).gameObject);
        anim.SetTrigger("changeAnimation");
        rb.velocity = Vector2.zero;
        timer = 1;
        state = EnemyState.DYING;
    }

    private void endCreatingAnimation() {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        direction = Random.Range(0, 2 * Mathf.PI);
        timer = Random.Range(3, 4);
        state = EnemyState.WAITING;
    }
}

public enum EnemyState {
    CREATING,
    MOVING,
    WAITING,
    DYING
};

