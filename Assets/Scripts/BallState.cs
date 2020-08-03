using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallState {
    public GameObject ball;
    public Rigidbody2D rb;

    public BallState(GameObject ball) {
        this.ball = ball;
        rb = ball.GetComponent<Rigidbody2D>();
    }

    public abstract void update();
}
