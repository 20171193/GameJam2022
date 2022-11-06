using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyGoSound : MonoBehaviour
{
    public AudioSource RG;

    // Start is called before the first frame update
    void Start()
    {
        RG = GetComponent<AudioSource>();
    }

    public void PlayRG()
    {
        RG.Play();
    }
}
