using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 300f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public float range;

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
        if (!p.error)
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

        if (currentWaypoint >= path.vectorPath.Count)
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

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        Quaternion lookOnLook =
 Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation =
        Quaternion.Slerp(transform.rotation, lookOnLook, 2f);
        //transform.LookAt(target.position);

        range = Vector3.Distance(target.position, transform.position);

        if (range <= 2.5)
        {
            animator.SetBool("reached", true);
            speed = 0;
            //animator.SetBool("attack", true);
        }
        else if (range > 2.5)
        {
            animator.SetBool("reached", false);
            speed = 300f;
            //animator.SetBool("attack", false);
        }

    }

}
