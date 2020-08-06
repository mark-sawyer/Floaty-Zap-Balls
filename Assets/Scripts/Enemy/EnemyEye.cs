using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour {
    private GameObject ball1;
    private GameObject ball2;
    private Vector2 differenceVector;
    private Vector2 difference1;
    private Vector2 difference2;
    private float distance1;
    private float distance2;
    private float MIN_DISTANCE_TO_LOOK = 25;
    private float MAX_DISPLACEMENT = 0.06882826454f;

    void Start() {
        ball1 = GameObject.Find("ball 1");
        ball2 = GameObject.Find("ball 2");
    }

    void Update() {
        difference1 = ball1.transform.position - transform.parent.position ;
        difference2 = ball2.transform.position - transform.parent.position;
        distance1 = difference1.sqrMagnitude;
        distance2 = difference2.sqrMagnitude;

        print(distance1);
        if (distance1 <= MIN_DISTANCE_TO_LOOK | distance2 <= MIN_DISTANCE_TO_LOOK) {
            if (distance1 < distance2) {
                differenceVector = difference1.normalized;
            }
            else {
                differenceVector = difference2.normalized;
            }

            transform.position = transform.parent.position + new Vector3(differenceVector.x, differenceVector.y, 0) * MAX_DISPLACEMENT;
        }
        else {
            transform.position = transform.parent.position;
        }
    }
}
