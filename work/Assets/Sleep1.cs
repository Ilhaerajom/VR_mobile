using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep1 : MonoBehaviour
{
    public GameObject readText;
    public static Sleep1 instance;

    void Awake()
    {
        if 
            (Sleep1.instance == null)
            Sleep1.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        readText.SetActive(false);
        StartCoroutine(ShowReady());
    }
    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < 3)
        {
            readText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
           readText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
