using System;
using UnityEngine;

public class sleep : MonoBehaviour
{
    
    Animator Sleep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "sleep")
        {
           Play(Sleep);
        }
    }

    private void Play(Animator Sleep)
    {
        throw new NotImplementedException();
    }
}

