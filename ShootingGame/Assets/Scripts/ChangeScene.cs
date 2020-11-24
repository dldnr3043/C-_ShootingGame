using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public void ChangeStartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeHelpScene()
    {
        SceneManager.LoadScene("HelpScene");
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
