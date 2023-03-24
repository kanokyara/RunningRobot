using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Stage2Clear") && PlayerPrefs.HasKey("Stage1Clear"))
        {
            this.GetComponent<Image>().color = new Color(255f, 255f, 255f, 100f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SwitchScene()
    {
        if (PlayerPrefs.HasKey("Stage2Clear") == false && PlayerPrefs.HasKey("Stage1Clear") == true)
        {
            SceneManager.LoadScene("Stage2");
        }
    }
}
