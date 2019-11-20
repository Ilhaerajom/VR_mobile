using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeFirstScene()
    {
        SceneManager.LoadScene("Room");
    }
    public void ChangeSecondScene()
    {
        SceneManager.LoadScene("stone1");
    }
}
