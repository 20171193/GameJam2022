using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBgm : MonoBehaviour
{
    public AudioSource DeadBGM;

    // Start is called before the first frame update
    void Start()
    {
        DeadBGM = GetComponent<AudioSource>();
    }

    public void PlayDeadBgm()
    {
        DeadBGM.Play();
    }
}
