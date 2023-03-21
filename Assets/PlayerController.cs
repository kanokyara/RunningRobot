using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //��Ԃ������ϐ�
    //0:�������Ă��Ȃ��@1:�����Ă���@2�F�W�����v���Ă���@3�F�_���[�W���󂯂Ă���
    public int state = 0;

    //�O�t���[���̏�Ԃ�\���ϐ�
    //��Ԃ̐؂�ւ�����Ɏg��
    int preState = 0;

    //�A�j���[�^�[�R���|�[�l���g�擾�̂��߂̕ϐ�
    private Animator animator;

    //rigidbody2D�R���|�[�l���g�擾�̂��߂̕ϐ�
    private Rigidbody2D rigid2D;

    //�W�����v�̑��x
    private float velocityY = 7f;

    //�W�����v���x����
    private float dump = 0.5f;

    //���ړ��̑��x
    private float velocityX = 5f;

    //�n�ʂƏՓ˂��Ă��邩�̔���
    public bool isGround;

    //�G�ƏՓ˂��Ă��邩�̔���
    bool isEnemy;

    //�I�u�W�F�N�g�̃X�P�[���i���E���]�p�j
    float Scale = 0.8f;

    //�I�[�f�B�I�N���b�v�Đ��p�I�[�f�B�I�\�[�X�R���|�[�l���g�擾�̂��߂̕ϐ�
    AudioSource audioSource;

    //�I�[�f�B�I�N���b�v�w��
    public AudioClip SEJump;
    public AudioClip SEDead;

    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�[�R���|�[�l���g�擾
        this.animator = GetComponent<Animator>();

        //rigidbody2D�R���|�[�l���g�擾
        this.rigid2D = GetComponent<Rigidbody2D>();

        //�I�[�f�B�I�\�[�X�R���|�[�l���g�擾
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ڒn�A�܂��͓G�ƐڐG�̍ۂ̈����̕ϐ���ݒ�
        this.animator.SetBool("isGround", isGround);
        this.animator.SetBool("isEnemy", isEnemy);

        //��Ԑ؂�ւ�蔻��(state�Ɛ��l������Ȃ��ꍇ��StateStart���Ăяo����A���̌�state�Ɛ��l�����킹�A��Ԃ��؂�ւ���state�̐��l���ς�邽�߁A�s�x�ω�����)
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

        //Ground�^�O�������n�ʃI�u�W�F�N�g�ɐڐG������
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        //Enemy�^�O�������G�I�u�W�F�N�g�ɐڐG���Ă��邩�ۂ�
        if (collision.gameObject.tag == "Enemy")
        {
            isEnemy = true;
        }
    }
        
    //�e��Ԑ؂�ւ�莞��1�񂾂����s����鏈��
    public void StateStart()
    {
        //�������Ă��Ȃ���ԁF���x�ƃA�j���[�V���������Z�b�g����
        if (state == 0)
        {
            this.rigid2D.velocity = new Vector2(0, 0);
            this.animator.SetBool("Run", false);
            audioSource.Stop();
        }

        //�����Ă����ԁF����A�j���[�V�������Đ�����
        if (state == 1)
        {
            this.animator.SetBool("Run", true);
            audioSource.Play();
        }

        //�W�����v���Ă����ԁF����A�j���[�V�������~�A������ɑ��x��ǉ�
        if (state == 2)
        {
            isGround = false;
            audioSource.Stop();
            audioSource.PlayOneShot(SEJump, 1.0f);
            this.animator.SetBool("Run", false);
            this.rigid2D.velocity = new Vector2(0, this.velocityY);
        }

        //�Q�[���I�[�o�[�@���x�����Z�b�g����
        if (state == 3)
        {
            this.animator.SetBool("Run", false);
            audioSource.Stop();
            audioSource.PlayOneShot(SEDead, 1.0f);
            this.animator.SetBool("Dead", true);
            if (transform.localScale.x == -Scale)
            {
                transform.localScale = new Vector2(-Scale, Scale);
            }
            else
            {
                transform.localScale = new Vector2(Scale, Scale);
            }
            this.rigid2D.velocity = new Vector2(0, 0);
        }
    }

    //�e��Ԃ�Update����
    void StateUpdate()
    {
        //�������Ă��Ȃ���ԁF��������W�����v���ł���
        if (state == 0)
        {
            //���L�[�������Ɖ��ړ�
            //�̂̌������f�t�H���g�i�������j�ɕς���
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localScale = new Vector2(Scale, Scale);
                this.rigid2D.velocity = new Vector2(velocityX, this.rigid2D.velocity.y);
            }

            //���L�[�������Ɖ��ړ��AScale��x�����}�C�i�X�ɂ��邱�Ƃő̂𔽓]
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector2(-Scale, Scale);
                this.rigid2D.velocity = new Vector2(-velocityX, this.rigid2D.velocity.y);
            }

            //�X�y�[�X�L�[�������ƃW�����v����
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                this.rigid2D.velocity = new Vector2(0, this.velocityY);
            }

            //�X�y�[�X�L�[�𗣂����ꍇ���x��������
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (this.rigid2D.velocity.y > 0)
                {
                    this.rigid2D.velocity *= this.dump;
                }
            }
        }

        //�����Ă����ԁF��������W�����v���ł���   
        if (state == 1)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localScale = new Vector2(Scale, Scale);
                this.rigid2D.velocity = new Vector2(velocityX, this.rigid2D.velocity.y);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector2(-Scale, Scale);
                this.rigid2D.velocity = new Vector2(-velocityX, this.rigid2D.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                this.rigid2D.velocity = new Vector2(0, this.velocityY);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (this.rigid2D.velocity.y > 0)
                {
                    this.rigid2D.velocity *= this.dump;
                }
            }
        }

        //�W�����v���Ă����ԁF����邪�W�����v�͂ł��Ȃ�
        if (state == 2)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localScale = new Vector2(Scale, Scale);
                this.rigid2D.velocity = new Vector2(velocityX, this.rigid2D.velocity.y);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector2(-Scale, Scale);
                this.rigid2D.velocity = new Vector2(-velocityX, this.rigid2D.velocity.y);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (this.rigid2D.velocity.y > 0)
                {
                    this.rigid2D.velocity *= this.dump;
                }
            }
        }

    }

    //�e��Ԃ̐؂�ւ�菈��
    void StateChange()
    {

        //�������Ă��Ȃ���ԁF���͂�����Α���A�W�����v��
        if (state == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                state = 2;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                state = 1;
            }
        }

        //�����Ă����ԁF���͂Ȃ���Ή������Ă��Ȃ���ԂցA�W�����v���͂ŃW�����v��Ԃ�
        else if (state == 1)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                state = 0;
            }

            else if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                state = 2;
            }
        }

        //�W�����v��ԁF���n�����牽�����Ă��Ȃ���Ԃ�
        else if (state == 2)
        {
            if (isGround)
            {
                state = 0;
            }
        }

        //�G�ɓ�����ƃQ�[���I�[�o�[
        if (isEnemy)
        {
            state = 3;
        }
    }
}
