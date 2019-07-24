using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    private Text kText;

	// Use this for initialization
	void Start () {
        kText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
        int score = Score.instance.score;
        string scoreZero = score.ToString("000");
        kText.text = "Score" + scoreZero;
	}
}
