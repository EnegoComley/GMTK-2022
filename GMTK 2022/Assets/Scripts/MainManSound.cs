using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManSound : MonoBehaviour
{
    public static MainManSound manager;
    public AudioSource source;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySoundNumber(int number)
    {
        source.PlayOneShot(clips[number]);
    }
}
