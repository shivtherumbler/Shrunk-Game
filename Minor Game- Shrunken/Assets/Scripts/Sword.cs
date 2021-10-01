using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sword : Interactable
{
    public GameObject rightHand;
    public GameObject sword;
    public ThirdPersonMove pick;
    private PickupText pickup;
    public bool isCollected;
    public GameObject companion;

    public override void Interact(GameObject actor)
    {
        base.Interact(actor);
        Pickup();
    }



    // Start is called before the first frame update
    void Start()
    {
        pickup = GetComponent<PickupText>();
    }

    public void Pickup()
    {
        pick.animator.SetBool("PickupSword", true);
        //transform.parent = rightHand.transform;
        //sword.SetActive(true);
        isCollected = true;
        companion.GetComponent<CompanionAI>().canvas[1].SetActive(false);
        Destroy(this.gameObject, 1.15f);
        //pick.animator.SetBool("Pickup", false);
    }

    public override bool isActive(GameObject actor)
    {
        return !isCollected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
