using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManage : MonoBehaviour
{
    public bool TrainPlayBool = false;

    public float TrainPlayGoSpeedFloat;

    public float TrainSetfloatZ = -530f;


    public float StopPositionZ;

    public GameObject GameProblemPanel;


    void Start()
    {

  
    }

   
    void Update()
    {
        if(TrainPlayBool==true)
        {
            if(TrainSetfloatZ >= StopPositionZ)
            {
                //게임 문제 패널 실행
                GameProblemPanel.SetActive(true);

                TrainPlayBool = false;
            }
            else
            {
                TrainSetfloatZ = TrainSetfloatZ + TrainPlayGoSpeedFloat;
                this.gameObject.transform.localPosition = new Vector3(185.49f, 1.37f, TrainSetfloatZ);

            }

        }
    }


    public void ReStartGame()
    {
        //게임 문제 패널 숨기기
        GameProblemPanel.SetActive(false);
        TrainSetfloatZ = -530f;
        this.gameObject.transform.localPosition = new Vector3(185.49f, 1.37f, TrainSetfloatZ);
        TrainPlayBool = true;
    }
}
