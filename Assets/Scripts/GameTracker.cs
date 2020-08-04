using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour {
    public GameObject zap;
    private GameObject onScreenZap;
    public GameObject ball1;
    public GameObject ball2;
    public static float ZAPPING_TIME = 1;
    public static float zapTimer = ZAPPING_TIME;
    public static float ZAP_COOLDOWN = 3;
    public zappingState zapState;

    void Start() {
        GameEvents.startZapping.AddListener(startZapping);
        GameEvents.endZapping.AddListener(endZapping);
        zapState = zappingState.CAN_ZAP;
    }

    void Update() {
        if (zapState == zappingState.COOLDOWN) {
            zapTimer -= Time.deltaTime;
            if (zapTimer <= 0) {
                GameEvents.endCooldown.Invoke();
                zapState = zappingState.CAN_ZAP;
            }
        }

        if (zapState == zappingState.ZAPPING) {
            zapTimer -= Time.deltaTime;
            if (zapTimer <= 0) {
                GameEvents.endZapping.Invoke();
            }
        }

        if (Input.GetMouseButtonDown(0) & zapState == zappingState.CAN_ZAP) {
            GameEvents.startZapping.Invoke();
        }
    }

    public void startZapping() {
        zapState = zappingState.ZAPPING;
        zapTimer = ZAPPING_TIME;

        Vector2 ball1Pos = ball1.transform.position;
        Vector2 ball2Pos = ball2.transform.position;
        Vector2 betweenBalls = (ball1Pos + ball2Pos) / 2;
        Vector2 difference = ball1Pos - ball2Pos;
        float angle = (float)Math.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        zap.transform.localScale = new Vector3(difference.magnitude, 1, 1);
        onScreenZap = Instantiate(zap, (Vector3)betweenBalls, Quaternion.Euler(0, 0, angle));
    }

    public void endZapping() {
        zapState = zappingState.COOLDOWN;
        zapTimer = ZAP_COOLDOWN;
        Destroy(onScreenZap);
    }
}

public enum zappingState {
    ZAPPING,
    COOLDOWN,
    CAN_ZAP
};
