using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadingScreen : MonoBehaviour
{
    //public Slider slider;
    public GameObject slider;
    public GameObject howToPlayPanel;
    public GameObject backButton;
    public GameObject mainPanel;
    public GameObject creditsPanel;
    public GameObject howtoplay;
    public GameObject title;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    GameObject currentSelected;
    public GameObject menuButtonController;
    [SerializeField] int thisIndex;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //slider.GetComponent<Slider>().value += 0.3f * Time.deltaTime;

        //if (slider.GetComponent<Slider>().value == 1)

        currentSelected = EventSystem.current.currentSelectedGameObject;
        

        if (menuButtonController.GetComponent<MenuButtonController>().index == thisIndex)
        {
            animator.SetBool("selected", true);

            if (menuButtonController.GetComponent<MenuButtonController>().index == 0)
            {

                if (Input.GetButtonDown("Submit"))
                {
                    animator.SetBool("pressed", true);
                    StartGame();
                }
                else if (animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", false);
                    animatorFunctions.disableOnce = true;
                }
            }

            else if (menuButtonController.GetComponent<MenuButtonController>().index == 1)
            {

                if (Input.GetButtonDown("Submit"))
                {
                    animator.SetBool("pressed", true);
                    HowToPlay();
                }
                else if (animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", false);
                    animatorFunctions.disableOnce = true;
                }
            }

            else if (menuButtonController.GetComponent<MenuButtonController>().index == 2)
            {

                if (Input.GetButtonDown("Submit"))
                {
                    animator.SetBool("pressed", true);
                    Credits();
                }
                else if (animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", false);
                    animatorFunctions.disableOnce = true;
                }
            }

            else if (menuButtonController.GetComponent<MenuButtonController>().index == 3)
            {

                if (Input.GetButton("Submit"))
                {
                    animator.SetBool("pressed", true);
                    QuitGame();
                }
                else if (animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", false);
                    animatorFunctions.disableOnce = true;
                }
            }

            else if(menuButtonController.GetComponent<MenuButtonController>().index == 4)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    animator.SetBool("pressed", true);
                    Back();
                }
                else if (animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", false);
                    animatorFunctions.disableOnce = true;
                }
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    IEnumerator LoadYourAsyncScene(string SceneName)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
        while (asyncLoad.progress < 1)
        {
            slider.GetComponent<Slider>().value = asyncLoad.progress;
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartGame()
    {
        slider.SetActive(true);
        StartCoroutine(LoadYourAsyncScene("KidRoom"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        backButton.SetActive(true);
        title.SetActive(false);

        menuButtonController.GetComponent<MenuButtonController>().index = 4;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(backButton);
    }

    public void Back()
    {
        backButton.SetActive(false);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
        title.SetActive(true);

        menuButtonController.GetComponent<MenuButtonController>().index = 0;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(howtoplay);
    }

    public void Credits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
        backButton.SetActive(true);
        title.SetActive(false);

        menuButtonController.GetComponent<MenuButtonController>().index = 4;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(backButton);
    }
}
