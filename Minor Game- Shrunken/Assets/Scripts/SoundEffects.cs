using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slash1()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    public void Slash2()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
    public void Slash3()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }

}
