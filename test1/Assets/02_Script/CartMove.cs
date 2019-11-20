using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWS;

public class CartMove : MonoBehaviour
{
    public GameObject gameProblemPanel;

    private Rigidbody rb;
    public Image UIImage;
    int C;
    public GameObject[] problem;
    
    // public Transform startMarker;
    // public Transform endMarker;

    // private float startTime;
    // private float journeyLength;

    // public bool TrainPlayBool = false;
    // public float TrainSetfloatZ = -17f;
    // public float TrainPlayGoSpeedFloat;


    //public float StopPositionZ;

    //int True = 1;
    //int False = 0;
    

    private void Start()
    {
     /*   UIImage = GameObject.Find("08_Images").GetComponent<Image>();
        if(UIImage != null)
        {
           UIImage.sprite = Resources.Load("tree napa", typeof(Sprite)) as Sprite;
        }*/
        //startTime = Time.deltaTime;
        //journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        //GameProblemPanel.SetActive(false);
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //float distCoverd = (Time.time - startTime) * speed;
        //float fracJourney = distCoverd / journeyLength;
        //transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);   

        //NewMethod();
        
    }

 /* private void NewMethod()
    {
        Debug.Log(rb.velocity.magnitude);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        // 컬라이더 닿으면 문제 나옴
        if (other.transform.tag == "problem")
        {
            gameProblemPanel.SetActive(true);
            Debug.Log(111);
            for(C = 0;C < 10; C++)
            {
                G();
            }
        }

    }

    IEnumerator CheckVelocity()
    {
        Debug.Log(rb.velocity.magnitude);

        while (rb.velocity.magnitude > 0)
        {
            yield return null;
        }

        gameProblemPanel.SetActive(true);
    }
    private void G()
    {
        problem[0].gameObject.SetActive(true);
        problem[1].gameObject.SetActive(true);
        problem[2].gameObject.SetActive(true);
        problem[3].gameObject.SetActive(true);
        problem[4].gameObject.SetActive(true);
    }
}