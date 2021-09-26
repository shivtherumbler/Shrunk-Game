using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSnap : MonoBehaviour
{
    public bool useIK;

    public bool lefthandIK;
    public bool righthandIK;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        RaycastHit LHit;
        RaycastHit RHit;

        if (Physics.Raycast(transform.position + new Vector3(0.0f, 2.0f, 0.0f), -transform.up + new Vector3(-0.5f, 0.0f, 0.0f), out LHit, 1f))
        {
            lefthandIK = true;
        }
        else
        {
            lefthandIK = false;
        }
    }
}
