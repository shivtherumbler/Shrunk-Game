using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(winPanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        winPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void Continue()
    {
        Cursor.visible = false;
        Destroy(winPanel);   
    }

}
