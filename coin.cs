using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {
    private Collider2D mCollider2D;

    void start () {
        mCollider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(other.tag == "Player") {
            mCollider2D.enabled = false;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
