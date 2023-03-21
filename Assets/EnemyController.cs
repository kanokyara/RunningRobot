using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //��Ԃ������@0:�������@1:�������@2:��~
    public int state = 0;

    //�O�t���[���̏�Ԃ�\���ϐ��@��Ԃ̐؂�ւ�����Ɏg��
    int preState = 0;

    //�A�j���[�^�[�R���|�[�l���g�擾�̂��߂̕ϐ�
    private Animator animator;

    //rigidbody2D�R���|�[�l���g�擾�̂��߂̕ϐ�
    private Rigidbody2D rigid2D;

    //���ړ��̑��x
    private float speed = 2f;

    //�n�ʂ̒[�ɂ��邩�ǂ����̔���
    public bool isGroundL;
    public bool isGroundR;

    //�v���C���[�ƏՓ˂��Ă��邩�̔���
    bool isPlayer;

    //�I�u�W�F�N�g�̃X�P�[���i���E���]�p�j
    float Scale = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�[�R���|�[�l���g�擾
        this.animator = GetComponent<Animator>();

        //rigidbody2D�R���|�[�l���g�擾
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != preState)
        {
            StateStart();
            preState = state;
        }

        //�e��Ԃ�Update����
        StateUpdate();

        //�e��Ԃ̐؂�ւ�蔻��
        StateChange();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy�^�O�������G�I�u�W�F�N�g�ɐڐG���Ă��邩�ۂ�
        if (collision.gameObject.tag == "PlayerTag")
        {
            isPlayer = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            isGroundL = true;
            isGroundR = true;
        }
    }

    //�e��Ԑ؂�ւ�莞��1�񂾂����s����鏈��
    public void StateStart()
    {
        if(state == 0)
        {
            this.animator.SetBool("Run", true);
        }
        if (state == 1)
        {
            this.animator.SetBool("Run", true);
        }
        if (state == 2)
        {
            this.rigid2D.velocity = new Vector2(0, 0);
            this.animator.SetBool("Run", false);
        }
    }
    //�e��Ԃ�Update����
    void StateUpdate()
    {
        if (state == 0)
        {
            this.animator.SetBool("Run", true);
            transform.localScale = new Vector2(Scale, Scale);
            transform.Translate(-this.speed * Time.deltaTime, 0, 0);
        }
        else if (state == 1)
        {
            transform.localScale = new Vector2(-Scale, Scale);
            transform.Translate(this.speed * Time.deltaTime, 0, 0);
        }
    }
    void StateChange()
    {
        if (state == 0)
        {
            if (isGroundL == false)
            {
                state = 1;
            }
        }
        else if (state == 1)
        {
            if (isGroundR == false)
            {
                state = 0;
            }
        }

        if (isPlayer)
        {
            state = 2;
        }
    }
}