using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 5;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
