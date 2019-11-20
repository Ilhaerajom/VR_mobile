using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QDestory : MonoBehaviour
{
    public GameObject anim;

    void Start()
    {
        Invoke("setfalse", 1);            
    }

    void setfalse()
    {        
            anim.gameObject.SetActive(false);
    }

}


        
    

