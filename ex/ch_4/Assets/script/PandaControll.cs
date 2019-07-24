using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaControll : MonoBehaviour {

    private Animator anim;
    Rigidbody2D rigid2D;
    private State m_state = State.Normal;
    public  float jumpPower = 600.0f;
    float walkPower = 40.0f;
    float maxSpeed = 8.0f;


    private float key = 0;
    private int h = 1;

    // Use this for initialization
    void Start () {
        this.rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
   
	}
    private void OnTriggerStay2D(Collider2D other){
        if (other.tag == "Enemy" && m_state == State.Normal)
        {
            m_state = State.Damaged;
            SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        }

    }
    enum State
    {
        Normal,
        Damaged,
    }

    // Update is called once per frame
    void Update () {
		
      //  if(Input. (0)) {
           
      //  }
        
      //  if (Input.()) {
       //     key = 0.5f;
        //    anim.SetFloat("Speed", h);
       // }
        //if (Input.()) {
        //    key = -0.5f;
        //    anim.SetFloat("Speed", h);
       // }
        float speedA = Mathf.Abs(this.rigid2D.velocity.x);
        if (speedA < this.maxSpeed) {
            this.rigid2D.AddForce(transform.right * key * this.walkPower);

        }

        if (key != 0) {
            transform.localScale = new Vector3(key, 0.5f, 1);
        }
	}

    public void LeftButtonClick()
    {
        key = -0.5f;
        anim.SetFloat("Speed", h);
    }

    public void RightButtonClick()
    {
        key = 0.5f;
        anim.SetFloat("Speed", h);
    }

    public void StopButtonClick()
    {
        key = 0f;
        anim.SetFloat("Speed", 0);
    }


    public void JumpButtonClick()
    {
        this.anim.SetTrigger("Jump");
        this.rigid2D.AddForce(transform.up * this.jumpPower);
    }
}
