using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMove : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;
    public Transform cam;
    public float jump;
    public float combo;
    public GameObject sword;
    public GameObject fidgerspinner;
    public GameObject enemy;
    public GameObject lockon;
    public GameObject locktarget;
    public AudioClip[] audioSources;


    public float speed = 5f;

    public float smoothTurn = 0.1f;
    float turnSmoothVelocity;

    private Interactable currentObject;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;


        if (controller.isGrounded)
        {

            bool move = (v != 0 || h != 0);

            animator.SetBool("Run", move);


        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            animator.SetBool("Jump", true);
            jump = (jump + 1) % 2;
            animator.SetFloat("jumppose", jump);
        }
        else
        {
            animator.SetBool("Jump", false);

        }
        

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }

        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.red, 10f);

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            Interactable i = hit.collider.GetComponent<Interactable>();
            //if(hit.transform.gameObject.tag == "Enemy")
            if (i != currentObject)
            {
                currentObject = i;
            }
            if (currentObject != null && currentObject.isActive(transform.gameObject))
            {

                {
                    //hit.transform.gameObject.GetComponent<Animator>().Play("Z_FallingForward");
                    //hit.transform.gameObject.GetComponent<EnemyMove>().enabled = false;
                    //crosshair.fillAmount = 0;
                    if (Input.GetKey(KeyCode.E))
                    {
                        currentObject.Interact(transform.gameObject);
                        currentObject = null;
                    }

                }
            }
        }

        if(sword.activeInHierarchy == true)
        {
            StartCoroutine(Wait());
            if (Input.GetButtonDown("Fire1") && controller.isGrounded)
            {
                animator.SetBool("Attack", true);
                StartCoroutine(SwordWait());
                animator.SetFloat("Swordpose", combo);
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }


        if(fidgerspinner.activeInHierarchy == true)
        {
            if (Input.GetButton("Fire2") && controller.isGrounded)
            {
                animator.SetBool("fly", true);
                
                
                StartCoroutine(Fly());

            }

            else if (Input.GetButton("Fire2"))
            {
                controller.Move(Physics.gravity * -0.01f);
                fidgerspinner.GetComponent<Animator>().SetBool("Spin", true);
                fidgerspinner.GetComponent<MeshRenderer>().enabled = true;

            }

            else if (Input.GetButton("Fire1"))
            {
                if (controller.isGrounded)
                {
                    animator.SetBool("fly", false);
                    fidgerspinner.GetComponent<Animator>().SetBool("Spin", false);
                    StartCoroutine(Spinneroff());


                }
                controller.Move(Physics.gravity * 0.01f);
            }

            else
            {
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);

                }
            }
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        animator.SetBool("Pickup", false);
        locktarget.GetComponent<CinemachineFreeLook>().m_LookAt = lockon.transform;
        lockon.GetComponent<CinemachineTargetGroup>().m_Targets[0] = (new CinemachineTargetGroup.Target { target = gameObject.transform, radius = 1f, weight = 1f });
        lockon.GetComponent<CinemachineTargetGroup>().m_Targets[1] = (new CinemachineTargetGroup.Target {target = enemy.transform, radius = 1f, weight = 1f });

    }

    IEnumerator SwordWait()
    {
        yield return new WaitForSeconds(2);
        combo = (combo + 1) % 3;
    }

    public void PickupSword()
    {
        sword.SetActive(true);
    }

    IEnumerator Fly()
    {
        yield return new WaitForSeconds(1.5f);
        controller.Move(Physics.gravity * -0.01f);
        fidgerspinner.GetComponent<MeshRenderer>().enabled = true;
        fidgerspinner.GetComponent<Animator>().SetBool("Spin", true);
    }

    IEnumerator Spinneroff()
    {
        yield return new WaitForSeconds(1);
        fidgerspinner.GetComponent<MeshRenderer>().enabled = false;

    }
}
