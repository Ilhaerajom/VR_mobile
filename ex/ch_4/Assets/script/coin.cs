using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {
    public CircleCollider2D mCollider2D;
    public  SpriteRenderer kRenderer;
 

 

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player") {
            Score.instance.Add();
            //kRenderer.enabled = false;
            this.gameObject.SetActive(false);
            mCollider2D.enabled = false;
        }
    }
    void Start () {
        print("ww");
       kRenderer = GetComponent<SpriteRenderer>();
        mCollider2D = this.GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
