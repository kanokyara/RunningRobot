using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("OP", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)||Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("StageSelect");
        }
    }
}
