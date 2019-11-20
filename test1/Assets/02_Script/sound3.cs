using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound3 : MonoBehaviour
{
    private AudioSource musicPlayer;
    public AudioClip backgroundMusic;


    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        playSound(backgroundMusic, musicPlayer);
        Invoke("Sound3", 15);
    }

    public static void playSound(AudioClip clip, AudioSource audioPlayer)

    {
        audioPlayer.Stop();
        audioPlayer.clip = clip;

        audioPlayer.time = 1000;
        audioPlayer.Play();

    }
}
