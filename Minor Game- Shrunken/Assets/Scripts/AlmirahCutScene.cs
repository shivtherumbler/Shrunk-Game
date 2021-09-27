using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class AlmirahCutScene : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] cams;
    public GameObject player;
    public ThirdPersonMove move;
    public GameObject spider;
    public GameObject prevTrigger;
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
            spider.SetActive(true);
            audioSource.clip = move.audioSources[2];
            audioSource.Play();
            spider.GetComponent<SpiderAI>().enabled = true;
            spider.GetComponentInChildren<BoxCollider>().enabled = true;
            spider.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            prevTrigger.SetActive(false);
            Destroy(gameObject, 1f);
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
