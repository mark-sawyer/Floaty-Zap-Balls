using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatState : BallState {
    private static float maxVelocity = 5;
    private Vector2 acceleration;
    private Vector2 velocity;

    public FloatState(GameObject ball) : base(ball) { }

    override public void update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ballPos = ball.transform.position;
        Vector2 difference = ballPos - mousePos;
        float distance = difference.magnitude;
        if (distance < 1) {
            distance = 1;
        }
        acceleration = 5 * (difference / (float)Math.Pow(distance, 2));
        rb.velocity += acceleration * Time.deltaTime;
        if (rb.velocity.magnitude > maxVelocity) {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}
