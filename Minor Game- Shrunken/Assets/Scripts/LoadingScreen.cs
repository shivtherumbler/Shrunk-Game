using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    //public Slider slider;
    public GameObject slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //slider.GetComponent<Slider>().value += 0.3f * Time.deltaTime;

        //if (slider.GetComponent<Slider>().value == 1)

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
}
