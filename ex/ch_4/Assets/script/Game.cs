using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    private static Game kInstance;
    public static Game instance {
        get {
            if(kInstance == null) {
                kInstance = FindObjectOfType<Game>();
            }
            return kInstance;
        }
    }
	public enum STATE {
        NONE,
        START,
        MOVE,
        GAMEOVER
    };
    public STATE state {
        get;
        set;
    }
    private Text kText;

    private void Start() {
        kText = GetComponent<Text>();
        state = STATE.START;
        StartCoroutine("StartCountDown");
    }
    void Update() {
        switch (state) {
        case STATE.START:
                break;
        case STATE.MOVE:
                break;
        case STATE.GAMEOVER:

                kText.text = "Game Over";
                if(Input.GetButtonDown("Jump")) {
                    int currentScene = Application.loadedLevel;
                }
                break;
        }
    }
    IEnumerator StartCountDown() {
        kText.text = "3";
        yield return new WaitForSeconds(1.0f);
        kText.text = "2";
        yield return new WaitForSeconds(1.0f);
        kText.text = "1";
        yield return new WaitForSeconds(1.0f);
        kText.text = "";
        state = STATE.MOVE;

    }
}
