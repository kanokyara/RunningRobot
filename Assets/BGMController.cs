using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    //�I�[�f�B�I�\�[�X���p��
    public AudioSource source1;
    public AudioSource source2;

    //�G�ƏՓ˂��Ă��邩�̔���
    bool isEnemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy�^�O�������G�I�u�W�F�N�g�ɐڐG���Ă��邩�ۂ�
        if (collision.gameObject.tag == "Enemy")
        {
            source1.Stop();
            yield return new WaitForSeconds(1f);
            source2.Play();
        }

        if(collision.gameObject.name == "DeadLine")
        {
            source1.Stop();
            yield return new WaitForSeconds(0.5f);
            source2.Play();
        }
    }
}
