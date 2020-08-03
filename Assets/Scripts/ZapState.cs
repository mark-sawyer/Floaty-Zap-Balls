using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapState : BallState {
    private Vector2 priorVelocity;

    public ZapState(GameObject ball) : base(ball) { }

    override public void update() {
        rb.velocity = Vector2.zero;
    }

    public void storePriorVelocity(Vector2 v) {
        priorVelocity = v;
    }

    public Vector2 getPriorVelocity() {
        return priorVelocity;
    }
}
