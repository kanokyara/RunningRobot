using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRCheckerScript : MonoBehaviour
{
    GameObject Enemy;
    EnemyController EnemyC;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        EnemyC = Enemy.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            EnemyC.isGroundR = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            EnemyC.isGroundR = true;
        }
    }
}
