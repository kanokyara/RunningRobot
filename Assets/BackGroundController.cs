using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{

    //�J�����̃I�u�W�F�N�g
    private GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        this.Camera= GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector2(Camera.transform.position.x, 0);
    }
}
