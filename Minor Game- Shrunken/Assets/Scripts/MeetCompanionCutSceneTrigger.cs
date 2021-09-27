using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class MeetCompanionCutSceneTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] cams;
    public GameObject player;
    public ThirdPersonMove move;
    public GameObject nextTrigger;
    public GameObject companion;
    public GameObject lizard;
    public AudioSource audioSource;
    


    // Start is called before the first frame update
    void Start()
    {
        move = player.GetComponent<ThirdPersonMove>();
        audioSource = player.GetComponent<AudioSource>();

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
        companion.SetActive(true);
        
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
            nextTrigger.SetActive(true);
            companion.GetComponent<CompanionAI>().enabled = true;
            lizard.SetActive(true);
            audioSource.clip = move.audioSources[4];
            audioSource.Play();
            companion.GetComponent<CompanionAI>().animator.SetBool("thanks", false);
            Destroy(gameObject, 0.2f);
        }

    }

    void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
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
