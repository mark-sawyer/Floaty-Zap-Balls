using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public GameObject otherBall;
    public Animator anim;
    public Rigidbody2D rb;
    public BallState state;
    public FloatState floating;
    public ZapState zapping;

    private void Start() {
        GameEvents.startZapping.AddListener(transitionToZapping);
        GameEvents.endZapping.AddListener(transitionToFloating);
        GameEvents.endCooldown.AddListener(endCooldownAnimation);
        GameEvents.hitEnemy.AddListener(enemyHit);

        floating = new FloatState(gameObject);
        zapping = new ZapState(gameObject);
        state = floating;
    }

    private void Update() {
        state.update();
    }

    private void transitionToZapping() {
        anim.SetTrigger("changeAnimation");
        zapping.storePriorVelocity(rb.velocity);
        state = zapping;
    }

    private void transitionToFloating() {
        anim.SetTrigger("changeAnimation");
        rb.velocity = zapping.getPriorVelocity();
        state = floating;
    }

    public void endCooldownAnimation() {
        anim.SetTrigger("changeAnimation");
    }

    public void enemyHit() {
        anim.SetTrigger("hitEnemy");
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "enemy" &
            (GameTracker.zapState == zappingState.CAN_ZAP | GameTracker.zapState == zappingState.COOLDOWN)) {
            GameEvents.hitEnemy.Invoke();
        }
    }
}
