using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public AudioClip playBgm;
    public AudioClip deadBgm;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StopPlayBgm()
    {
        audioSource.Stop();
    }
}
