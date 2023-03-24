using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void SwitchScene()
    {
        if (PlayerPrefs.HasKey("OP"))
        {
            SceneManager.LoadScene("StageSelect");
        }
        else
        {
            SceneManager.LoadScene("OP");
        }
    }
}
