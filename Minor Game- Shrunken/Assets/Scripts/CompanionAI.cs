using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class CompanionAI : MonoBehaviour
{
    public Transform target;
    public Transform[] objective;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public float range;
    public GameObject[] canvas;

    Path path;
    int currentWaypoint = 0;
    bool reached = false;

    public Seeker seeker;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reached = true;
            return;
        }
        else
        {
            reached = false;
        }

        Vector3 direction = ((Vector3)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector3 force = direction * speed * Time.deltaTime;

        rb.velocity = (force);

        float distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        Quaternion lookOnLook =
 Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation =
        Quaternion.Slerp(transform.rotation, lookOnLook, 2f);
        //transform.LookAt(target.position);

        range = Vector3.Distance(target.position, transform.position);

        if (range <= 3)
        {
            animator.SetBool("reached", true);
            speed = 0;
        }
        else if(range > 3)
        {
            animator.SetBool("reached", false);
            speed = 200f;
        }

        if(range <=2)
        {
            Snif();
            canvas[0].SetActive(true);
        }
        else
        {
            canvas[0].SetActive(false);
        }
        //if(target == objective[1])
        {
            //canvas[1].GetComponentInChildren<Text>().text = "Follow Tammy";
            //canvas[1].SetActive(true);
        }
        

    }

    public void Snif()
    {
        if(Input.GetKey(KeyCode.I))
        {
            target = objective[1];
            canvas[0].SetActive(false);
            canvas[1].GetComponentInChildren<Text>().text = "Follow Tammy";
            canvas[1].SetActive(true);
        }
    }

}
