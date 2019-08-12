using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManage : MonoBehaviour
{
    public bool TrainPlayBool = false;

    public float TrainPlayGoSpeedFloat;

    public float TrainSetfloatZ = -530f;
    void Start()
    {

  
    }

   
    void Update()
    {
        if(TrainPlayBool==true)
        {
            TrainSetfloatZ = TrainSetfloatZ + TrainPlayGoSpeedFloat;
            this.gameObject.transform.localPosition = new Vector3(185.49f, 1.37f, TrainSetfloatZ);
        }
    }
}
