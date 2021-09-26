using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupText : MonoBehaviour
{

    public GameObject Player;
    public GameObject canvas;
    public float range;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        range = Vector3.Distance(Player.transform.position, transform.position);

        if (range <= 2)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
            
        }
    }
}
