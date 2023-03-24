using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{
    public bool Stage1Clear;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Stage1Clear"))
        {
            Stage1Clear = true;
            this.GetComponent<Image>().color = new Color(255f,255f, 255f, 100f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
        public void SwitchScene()
    {
        if (PlayerPrefs.HasKey("Stage1Clear") == false)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
