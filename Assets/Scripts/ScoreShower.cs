using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreShower : MonoBehaviour {
    public static int score;

    public void Start() {
        GetComponent<Text>().text = score.ToString();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetKeyDown("return")) {
            GameTracker.score = 0;
            GameTracker.enemyCount = 0;
            SceneManager.LoadScene("Game Scene");
        }
    }
}
