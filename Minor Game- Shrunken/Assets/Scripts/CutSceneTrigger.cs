using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using Cinemachine;
public class CutSceneTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] cams;
    public GameObject player;
    public ThirdPersonMove move;
    public GameObject companion;
    public GameObject sword;
    public GameObject lockon;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        move = player.GetComponent<ThirdPersonMove>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            Play();
            move.speed = 0;
            companion.GetComponent<CompanionAI>().target = companion.GetComponent<CompanionAI>().objective[0];
            companion.GetComponent<CompanionAI>().canvas[1].GetComponentInChildren<Text>().text = "Pick up the Sword!";
            lockon.GetComponent<CinemachineFreeLook>().m_LookAt = sword.transform;
        }
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
            enemy.GetComponent<EnemyAI>().enabled = true;
            enemy.GetComponent<Animator>().SetBool("idle", false); 
            Destroy(gameObject, 0.2f);
        }
            
    }

    void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }

}
