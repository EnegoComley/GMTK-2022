using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManSound : MonoBehaviour
{
    public static MainManSound manager;
    public AudioSource source;

    public AudioClip[] clips;

    private void Awake()
    {
        if(manager != null)
        {
            Destroy(gameObject);
        }
        manager = this;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundNumber(int number)
    {
        source.PlayOneShot(clips[number]);
    }
}
