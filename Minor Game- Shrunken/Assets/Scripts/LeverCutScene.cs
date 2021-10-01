using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class LeverCutScene : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] cams;
    public GameObject player;
    public ThirdPersonMove move;
    public GameObject companion;
    public AudioSource audioSource;
    public GameObject nextTrigger;

    // Start is called before the first frame update
    void Start()
    {
        move = player.GetComponent<ThirdPersonMove>();
        audioSource = player.GetComponent<AudioSource>();
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
            companion.GetComponent<CompanionAI>().canvas[1].SetActive(true);
            companion.GetComponent<CompanionAI>().canvas[1].GetComponentInChildren<Text>().text = "Pick up the Spinner!";
            audioSource.clip = move.audioSources[5];
            audioSource.Play();
            nextTrigger.SetActive(true);
            Destroy(gameObject, 0.2f);
            
        }

    }

    void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Play();
            move.speed = 0;

        }
    }
}
