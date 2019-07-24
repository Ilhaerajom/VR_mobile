using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaControll : MonoBehaviour {

    private Animator anim;
    Rigidbody2D rigid2D;
    float jumpPower = 600.0f;
    float walkPower = 40.0f;
    float maxSpeed = 8.0f;


	// Use this for initialization
	void Start () {
        this.rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown (KeyCode.Space)) {
            this.anim.SetTrigger("Jump");
            this.rigid2D.AddForce(transform.up * this.jumpPower);
        }
        float key = 0;
        int h = 1;
        if (Input.GetKey(KeyCode.RightArrow)) {
            key = 0.5f;
            anim.SetFloat("Speed", h);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            key = -0.5f;
            anim.SetFloat("Speed", h);
        }
        float speedA = Mathf.Abs(this.rigid2D.velocity.x);
        if (speedA < this.maxSpeed) {
            this.rigid2D.AddForce(transform.right * key * this.walkPower);

        }

        if (key != 0) {
            transform.localScale = new Vector3(key, 0.5f, 1);
        }
	}
}
