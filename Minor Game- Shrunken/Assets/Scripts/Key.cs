using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public ThirdPersonMove pick;
    private PickupText pickup;
    public bool isCollected;
    public GameObject companion;
    public GameObject Door;

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
        pick.animator.SetBool("PickupKey", true);
        //transform.parent = rightHand.transform;
        //sword.SetActive(true);
        isCollected = true;
        companion.GetComponent<CompanionAI>().canvas[1].SetActive(false);
        Door.GetComponent<Animation>().Play();
        //pick.animator.SetBool("Pickup", false);
    }

    public override bool isActive(GameObject actor)
    {
        return !isCollected;
    }
}
