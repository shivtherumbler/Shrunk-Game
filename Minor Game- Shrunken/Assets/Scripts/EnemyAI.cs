using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 300f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public float range;
    public Image healthbar;
    public GameObject healthcanvas;
    public GameObject blood;
    public GameObject nextTrigger;
    public GameObject companion;

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
        if(animator.GetBool("death") == false)
        {
            Quaternion lookOnLook =
 Quaternion.LookRotation(target.transform.position - transform.position);

            transform.rotation =
            Quaternion.Slerp(transform.rotation, lookOnLook, 2f);
        }
        
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

    private void Update()
    {
        if (range <= 2.5)
        {
            if (reached == true && target.GetComponent<ThirdPersonMove>().animator.GetBool("block") == false && animator.GetBool("death") == false)
            {
                target.GetComponent<ThirdPersonMove>().health.fillAmount -= 0.1f * Time.deltaTime;
                target.GetComponent<ThirdPersonMove>().blood.SetActive(true);
            }
            else
            {
                target.GetComponent<ThirdPersonMove>().blood.SetActive(false);
                blood.SetActive(false);
            }

        }
        else if (range > 2.5)
        {

            target.GetComponent<ThirdPersonMove>().blood.SetActive(false);
            blood.SetActive(false);

        }

        if (target.GetComponent<ThirdPersonMove>().controller.isGrounded == false && target.GetComponent<ThirdPersonMove>().animator.GetBool("Run") == false)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            
        }
        else if(target.GetComponent<ThirdPersonMove>().controller.isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        if(healthbar.fillAmount == 0)
        {
            animator.SetBool("death", true);
            speed = 0;
            target.GetComponent<ThirdPersonMove>().blood.SetActive(false);
            Destroy(blood);
            healthcanvas.SetActive(false);
            nextTrigger.SetActive(true);
            gameObject.GetComponent<EnemyAI>().enabled = false;
           
        }
    }

    public void TextChange()
    {
        companion.GetComponent<CompanionAI>().canvas[1].SetActive(true);
        companion.GetComponent<CompanionAI>().canvas[1].GetComponentInChildren<Text>().text = "Pull the lever!";
    }

}
