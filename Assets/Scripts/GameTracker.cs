using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour {
    public GameObject zap;
    private GameObject onScreenZap;
    public GameObject ball1;
    public GameObject ball2;
    public static GameObject enemy;
    public static float ZAPPING_TIME = 1;
    public static float zapTimer = ZAPPING_TIME;
    public static float ZAP_COOLDOWN = 2;
    public static float HURT_COOLDOWN = 2;
    public static float[] X_MIN_MAX_Y_MIN_MAX = { -10, 10, -7, 7 };
    public static zappingState zapState;
    public static int enemyCount;
    public static int score;
    private static float newEnemyTimer = 10;
    private static float newEnemyTimerMultiplier = 1;

    void Start() {
        enemy = Resources.Load<GameObject>("Prefabs/enemy");
        createNewEnemy();
        GameEvents.startZapping.AddListener(startZapping);
        GameEvents.endZapping.AddListener(endZapping);
        GameEvents.enemyHitBall.AddListener(enemyHitBall);
        zapState = zappingState.CAN_ZAP;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        newEnemyTimer -= Time.deltaTime;
        if (newEnemyTimer <= 0) {
            createNewEnemy();
            newEnemyTimer = 10 * newEnemyTimerMultiplier;
        }

        if (zapState == zappingState.COOLDOWN | zapState == zappingState.HURT) {
            zapTimer -= Time.deltaTime;
            if (zapTimer <= 0) {
                zapState = zappingState.CAN_ZAP;
                GameEvents.endCooldown.Invoke();
            }
        }

        else if (zapState == zappingState.ZAPPING) {
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

    public void enemyHitBall() {
        zapState = zappingState.HURT;
        zapTimer = HURT_COOLDOWN;
    }

    public static void createNewEnemy() {
        enemyCount++;
        float xCoord = UnityEngine.Random.Range(X_MIN_MAX_Y_MIN_MAX[0], X_MIN_MAX_Y_MIN_MAX[1]);
        float yCoord = UnityEngine.Random.Range(X_MIN_MAX_Y_MIN_MAX[2], X_MIN_MAX_Y_MIN_MAX[3]);

        Instantiate(enemy, new Vector3(xCoord, yCoord, 0), Quaternion.identity);
    }

    public static void getNewEnemyTimerMultiplier() {
        newEnemyTimerMultiplier = Mathf.Exp(-0.03f * score);
    }
}

public enum zappingState {
    ZAPPING,
    COOLDOWN,
    CAN_ZAP,
    HURT
};
