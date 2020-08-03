using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public GameObject otherBall;
    public Rigidbody2D rb;
    public BallState state;
    public FloatState floating;
    public ZapState zapping;

    private void Start() {
        GameEvents.startZapping.AddListener(transitionToZapping);
        GameEvents.endZapping.AddListener(transitionToFloating);

        floating = new FloatState(gameObject);
        zapping = new ZapState(gameObject);
        state = floating;
    }

    private void Update() {
        state.update();
    }

    private void transitionToZapping() {
        zapping.storePriorVelocity(rb.velocity);
        state = zapping;
    }
    private void transitionToFloating() {
        rb.velocity = zapping.getPriorVelocity();
        state = floating;
    }
}
