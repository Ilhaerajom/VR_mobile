using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour {
    private PandaControll mPandaControll;
    private Collider2D mCollider2D;

	// Use this for initialization
	void Start () {
        mPandaControll = GetComponent<PandaControll>();
        mCollider2D = GetComponent<Collider2D>();
		
	}
	
    void OnDamge() {
        mCollider2D.enabled = false;
        mPandaControll.enabled = false;
        Game.instance.state = Game.STATE.GAMEOVER;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
