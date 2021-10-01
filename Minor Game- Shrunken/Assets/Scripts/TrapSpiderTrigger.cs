using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TrapSpiderTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] cams;
    public GameObject player;
    public ThirdPersonMove move;
    public GameObject nextTrigger;
    public GameObject[] canvas;
    public GameObject spider;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        move = player.GetComponent<ThirdPersonMove>();
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
            nextTrigger.SetActive(true);
            nextTrigger.GetComponent<SpiderTrapCutScene>().Play();
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            //Destroy(spider);
            Destroy(playableDirector, 0.2f);
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
            canvas[0].GetComponentInChildren<Text>().text = "Press E to trap spider in bucket";
            canvas[0].SetActive(true);
            rb = spider.GetComponent<Rigidbody>();

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Play();
            move.speed = 0;
            spider.GetComponent<SpiderAI>().enabled = false;
            spider.GetComponentInChildren<BoxCollider>().enabled = false;
            Destroy(rb);
            canvas[0].SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas[0].SetActive(false);
    }
}
