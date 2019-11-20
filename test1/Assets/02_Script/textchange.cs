using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textchange : MonoBehaviour
{
    public first f;
    int R = Random.Range(1, 10);

    private void Tt()
    {
        f.problem[1].GetComponent<Text>().text = "tlqkf";
        f.problem[2].GetComponent<Text>().text = "usb";
        f.problem[3].GetComponent<Text>().text = "dhoenrh";
        f.problem[4].GetComponent<Text>().text = "dhkTwl";
        f.problem[5].GetComponent<Text>().text = "qt";
    }

    public void tf()
    {
        if (R == 1)
        {
            Invoke("Tt",0);
        }else if(R == 2)
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
    }
}
