using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ThirdPersonMove : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;
    public Transform cam;
    public float jump;
    public float combo;
    public GameObject sword;
    public GameObject fidgetspinner;
    public GameObject enemy;
    public GameObject lockon;
    public GameObject locktarget;
    public AudioClip[] audioSources;
    public Image stamina;
    public Image health;
    public GameObject companion;
    public GameObject blood;
    public bool weapons;
    public GameObject key;

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
            StartCoroutine(Sword());
            if (Input.GetButtonDown("Fire1") && controller.isGrounded)
            {
                animator.SetBool("Attack", true);
                StartCoroutine(SwordWait());
                animator.SetFloat("Swordpose", combo);
            }
            else if (Input.GetButton("Fire2") && controller.isGrounded)
            {
                animator.SetBool("block", true);
                
            }
            else
            {
                animator.SetBool("Attack", false);
                animator.SetBool("block", false);
            }
        }


        if(fidgetspinner.activeInHierarchy == true)
        {

            StartCoroutine(Spinner());
            if (stamina.fillAmount > 0.1)
            {
                if (Input.GetButton("Fire2") && controller.isGrounded)
                {
                    animator.SetBool("fly", true);

                    
                    StartCoroutine(Fly());

                }

                else if (Input.GetButton("Fire2"))
                {
                    controller.Move(Physics.gravity * -0.01f);
                    //fidgetspinner.GetComponent<Animator>().SetBool("Spin", true);
                    fidgetspinner.GetComponent<Invector.vRotateObject>().enabled = true;
                    fidgetspinner.GetComponent<MeshRenderer>().enabled = true;
                    stamina.fillAmount -= 0.5f * Time.deltaTime;
                }
                else if (Input.GetButton("Fire1"))
                {
                    if (controller.isGrounded)
                    {
                        animator.SetBool("fly", false);
                        //fidgetspinner.GetComponent<Animator>().SetBool("Spin", false);
                        fidgetspinner.GetComponent<Invector.vRotateObject>().enabled = false;

                        StartCoroutine(Spinneroff());


                    }
                    controller.Move(Physics.gravity * 0.01f);
                    stamina.fillAmount += 0.3f * Time.deltaTime;
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
                    stamina.fillAmount += 0.3f * Time.deltaTime;
                }
            }
            else if(stamina.fillAmount <= 0.1f && stamina.fillAmount > 0f)
            {
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * 0 * Time.deltaTime);

                }
                stamina.fillAmount -= 0.1f * Time.deltaTime;
            }
            
            else if(stamina.fillAmount == 0)
            {
                stamina.fillAmount += 0.11f;
            }

            
        }

        if(health.fillAmount == 0)
        {
            animator.SetBool("death", true);
            animator.SetBool("Run", false);
            h = 0;
            v = 0;
            speed = 0;
            controller.Move(Vector3.zero);
            
        }

        if(weapons == true)
        {
            if(Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
            {
                sword.SetActive(true);
                fidgetspinner.SetActive(false);
            }
            else if(Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            {
                fidgetspinner.SetActive(true);
                sword.SetActive(false);
            }
        }
        
        if(enemy.GetComponent<Animator>().GetBool("death") == true)
        {
            companion.GetComponent<CompanionAI>().canvas[1].SetActive(true);
            companion.GetComponent<CompanionAI>().canvas[1].GetComponentInChildren<Text>().text = "Pull the lever!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Health")
        {
            health.fillAmount = 1;
            Destroy(other.gameObject, 0.1f);
        }
    }

    IEnumerator Sword()
    {
        yield return new WaitForSeconds(4f);
        animator.SetBool("PickupSword", false);
        //locktarget.GetComponent<CinemachineFreeLook>().m_LookAt = lockon.transform;
        //lockon.GetComponent<CinemachineTargetGroup>().m_Targets[0] = (new CinemachineTargetGroup.Target { target = gameObject.transform, radius = 1f, weight = 1f });
        //lockon.GetComponent<CinemachineTargetGroup>().m_Targets[1] = (new CinemachineTargetGroup.Target {target = enemy.transform, radius = 1f, weight = 1f });

    }

    IEnumerator Spinner()
    {
        yield return new WaitForSeconds(4f);
        animator.SetBool("PickupSpinner", false);
        //locktarget.GetComponent<CinemachineFreeLook>().m_LookAt = lockon.transform;
        //lockon.GetComponent<CinemachineTargetGroup>().m_Targets[0] = (new CinemachineTargetGroup.Target { target = gameObject.transform, radius = 1f, weight = 1f });
        //lockon.GetComponent<CinemachineTargetGroup>().m_Targets[1] = (new CinemachineTargetGroup.Target {target = enemy.transform, radius = 1f, weight = 1f });

    }

    IEnumerator Key()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("PickupKey", false);
        key.SetActive(false);
        //locktarget.GetComponent<CinemachineFreeLook>().m_LookAt = lockon.transform;
        //lockon.GetComponent<CinemachineTargetGroup>().m_Targets[0] = (new CinemachineTargetGroup.Target { target = gameObject.transform, radius = 1f, weight = 1f });
        //lockon.GetComponent<CinemachineTargetGroup>().m_Targets[1] = (new CinemachineTargetGroup.Target {target = enemy.transform, radius = 1f, weight = 1f });

    }

    IEnumerator SwordWait()
    {
        yield return new WaitForSeconds(2);
        combo = (combo + 1) % 3;
    }

    public void PickupKey()
    {
        StartCoroutine(Key());
    }

    public void PickupSword()
    {
        sword.SetActive(true);
    }

    public void PickupSpinner()
    {
        fidgetspinner.SetActive(true);
        sword.SetActive(false);
        weapons = true;
    }

    IEnumerator Fly()
    {
        yield return new WaitForSeconds(1.5f);
        controller.Move(Physics.gravity * -0.01f);
        fidgetspinner.GetComponent<MeshRenderer>().enabled = true;
        //fidgetspinner.GetComponent<Animator>().SetBool("Spin", true);
        fidgetspinner.GetComponent<Invector.vRotateObject>().enabled = true;

    }

    IEnumerator Spinneroff()
    {
        yield return new WaitForSeconds(1);
        fidgetspinner.GetComponent<MeshRenderer>().enabled = false;

    }

}
