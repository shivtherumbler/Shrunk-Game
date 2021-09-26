using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual bool isActive(GameObject actor)
    {
        return true;
    }

    public virtual void Interact(GameObject actor)
    {

    }


}
