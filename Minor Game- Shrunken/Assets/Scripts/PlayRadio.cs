using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRadio : Interactable
{
    private AudioSource audioSource;
    public bool isCollected;
    public GameObject Player;
    public GameObject canvas;
    public float range;

    public override void Interact(GameObject actor)
    {
        base.Interact(actor);
        PlayMusic();
    }



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayMusic()
    {
        audioSource.Play();
        isCollected = true;
        canvas.SetActive(false);
    }

    public override bool isActive(GameObject actor)
    {
        return !isCollected;
    }
    

    // Update is called once per frame
    void Update()
    {
        range = Vector3.Distance(Player.transform.position, transform.position);

        if (range <= 3)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);

        }
    }

}
