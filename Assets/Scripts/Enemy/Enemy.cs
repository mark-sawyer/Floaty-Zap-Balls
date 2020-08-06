using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static float speed = 1;
    public Rigidbody2D rb;
    public EnemyState state;
    private float direction;
    private float timer;

    void Start() {
        timer = Random.Range(3, 4);
        direction = Random.Range(0, 2 * Mathf.PI);
        state = EnemyState.WAITING;
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
            }
        }
    }
}

public enum EnemyState {
    MOVING,
    WAITING
};

