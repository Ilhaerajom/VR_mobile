using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class first : MonoBehaviour
{
    int count = 3;

    float timer;
    float delay;

    public Text[] life = new Text[3];

    public GameObject[] problem = new GameObject[5];
    
    public GameObject[] Noms = new GameObject[8];

    public GameObject[] ms = new GameObject[6];

    public GameObject[] yn = new GameObject[2];

    public Text[] pb;

    public GameObject[] yee;

    void Start()
    {
        yee[0].gameObject.SetActive(false);
        InvokeRepeating("Update",2,1);
        timer = 0.0f;
        delay = 12f;
        life[0].text = "스톡:" + count.ToString();
        Invoke("Nomsstart", 0.4f);
    }
    void Nomsstart()
    {
        Noms[0].gameObject.SetActive(true);
        Noms[1].gameObject.SetActive(true);
        Noms[2].gameObject.SetActive(true);
        Invoke("Nomsfalse", 2);
    }

    private void Update()
    {
        timer += Time.deltaTime;
           if(timer > delay)
        {
            yee[0].gameObject.SetActive(false);
            yn[0].gameObject.SetActive(false);
            yn[1].gameObject.SetActive(false);
            timer = 10;
        }
    }

    public void OnClickChoiceanswer()
    {       
      yn[0].gameObject.SetActive(true);        
    } 

    public void OnClickchoiceerror()
    {
        yn[1].gameObject.SetActive(true);             

        count = count - 1;
        life[0].text = "스톡:" + count.ToString();
        if (count == 2)
        {
            ms[0].gameObject.SetActive(true);
            Noms[1].gameObject.SetActive(true);
            Noms[2].gameObject.SetActive(true);
            Invoke("firsterror", 2);
        }else  if (count == 1)
        {
            Destroy(ms[0], 0);
            Destroy(Noms[1], 0);
            Destroy(Noms[2], 0);
            ms[1].gameObject.SetActive(true);
            ms[2].gameObject.SetActive(true);
            Noms[3].gameObject.SetActive(true);
            Invoke("scenderror", 2);
        }else if (count == 0)
        {
            Destroy(ms[1], 0);
            Destroy(ms[2], 0);
            Destroy(Noms[3], 0);
            ms[3].gameObject.SetActive(true);
            ms[4].gameObject.SetActive(true);
            ms[5].gameObject.SetActive(true);
           
            life[1].text = "Welcome to hell";

            Invoke("ChangeSecondScene", 3);
        }
    }
    void finsh()
    {
        yee[0].gameObject.SetActive(true);
        Invoke("restart", 0.7f);     
    }
    void restart()
    {
        yee[1].gameObject.transform.Translate(0.180007f, 0, -49.3648f);
    }
    void Nomsfalse()
    {
        Noms[0].gameObject.SetActive(false);
        Noms[1].gameObject.SetActive(false);
        Noms[2].gameObject.SetActive(false);
    }
    void firsterror()
    {
        ms[0].gameObject.SetActive(false);
        Noms[1].gameObject.SetActive(false);
        Noms[2].gameObject.SetActive(false);
    }
    void scenderror()
    {
        ms[1].gameObject.SetActive(false);
        ms[2].gameObject.SetActive(false);
        Noms[3].gameObject.SetActive(false);
    }
    public void ChangeSecondScene()
    {
        SceneManager.LoadScene("05 VRMenuGaze");
    }
    public void OnClickchangeown()
    {
     //   this.gameObject.GetComponent<Image>().color = Color.green;
        int R = Random.Range(1, 10);
        if (R == 1)
        {
            Invoke("Tt", 0);
        }
        else if (R == 2)
        {
            Invoke("Tt", 0);
        }
        else if (R == 2)
        {
            Invoke("Tt", 0);
        }
        else if (R == 3)
        {
            Invoke("Tt", 0);
        }
        else if (R == 4)
        {
            Invoke("Tt", 0);
        }
        else if (R == 5)
        {
            Invoke("Tt", 0);
        }
        else if (R == 6)
        {
            Invoke("Tt", 0);
        }
        else if (R == 7)
        {
            Invoke("Tt", 0);
        }
        else if (R == 8)
        {
            Invoke("Tt", 0);
        }
        else if (R == 9)
        {
            Invoke("Tt", 0);
        }
        else if (R == 10)
        {
            Invoke("Tt", 0);
        }
        problem[0].gameObject.SetActive(false);
        problem[1].gameObject.SetActive(false);
        problem[2].gameObject.SetActive(false);
        problem[3].gameObject.SetActive(false);
        problem[4].gameObject.SetActive(false);
        //  problem[1].GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        Invoke("finsh", 3);
    }
    private void Tt()
    {
        pb[0].GetComponent<Text>().text = "tlqkf";
        //   problem[1].GetComponent<RectTransform>().position = new Vector3(10,0,0);
        pb[1].GetComponent<Text>().text = "usb";
        pb[2].GetComponent<Text>().text = "dhoenrh";
        pb[3].GetComponent<Text>().text = "dhkTwl";
        pb[4].GetComponent<Text>().text = "qt";
    }
}




