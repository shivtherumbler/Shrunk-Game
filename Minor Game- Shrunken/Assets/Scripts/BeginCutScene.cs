using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class BeginCutScene : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] cams;
    public GameObject player;
    public ThirdPersonMove move;
    public AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        move = player.GetComponent<ThirdPersonMove>();
        audioSource = player.GetComponent<AudioSource>();
        Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Play()
    {
        cams[1].SetActive(true);
        cams[0].SetActive(false);

        playableDirector.Play();
    }

    void OnEnable()
    {
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (playableDirector == aDirector)
        {
            cams[0].SetActive(true);
            cams[1].SetActive(false);
            move.speed = 5;
            audioSource.clip = move.audioSources[0];
            audioSource.Play();
            Destroy(gameObject, 0.2f);
        }

    }

    void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
}
