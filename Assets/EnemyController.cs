using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //状態を示す　0:←方向　1:→方向　2:停止
    public int state = 0;

    //前フレームの状態を表す変数　状態の切り替え判定に使う
    int preState = 0;

    //アニメーターコンポーネント取得のための変数
    private Animator animator;

    //rigidbody2Dコンポーネント取得のための変数
    private Rigidbody2D rigid2D;

    //横移動の速度
    private float speed = 2f;

    //地面の端にいるかどうかの判定
    public bool isGroundL;
    public bool isGroundR;

    //プレイヤーと衝突しているかの判定
    bool isPlayer;

    //オブジェクトのスケール（左右反転用）
    float Scale = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        //アニメーターコンポーネント取得
        this.animator = GetComponent<Animator>();

        //rigidbody2Dコンポーネント取得
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

        //各状態のUpdate処理
        StateUpdate();

        //各状態の切り替わり判定
        StateChange();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemyタグをつけた敵オブジェクトに接触しているか否か
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

    //各状態切り替わり時に1回だけ実行される処理
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
    //各状態のUpdate処理
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