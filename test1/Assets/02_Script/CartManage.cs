using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManage : MonoBehaviour
{
    public bool TrainPlayBool = false;

    public float TrainPlayGoSpeedFloat;

    public float TrainSetfloatZ = -17f;

    public float deltaTime = 1f;

    public float StopPositionZ;

    public GameObject GameProblemPanel;


    void Start()
    {


    }


    void Update()
    {
        if (TrainPlayBool == true)
        {
            if (TrainSetfloatZ >= StopPositionZ)
            {
                //게임 문제 패널 실행
                GameProblemPanel.SetActive(true);

                TrainPlayBool = false;
            }
         // else
            
             // TrainSetfloatZ = TrainSetfloatZ + TrainPlayGoSpeedFloat;
                //   this.gameObject.transform.localPosition = new Vector3(185.49f, 1.37f, TrainSetfloatZ);//TrainSetfloatZ * Time.deltaTime
             // gameObject.transform.localPosition = Vector3.Lerp(new Vector3(0.97f, 1.14f, -17), new Vector3(0.97f, 1.14f, 30), 0 - 1);
            //

        }
    }
}
