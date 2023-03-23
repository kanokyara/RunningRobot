using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //プレイヤーのオブジェクト
    private GameObject Player;

    float offset;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        offset = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x >= offset)
        {
            this.transform.position = new Vector3(Player.transform.position.x - offset, 0, transform.position.z);
        }
    }
}
