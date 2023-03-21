using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //状態を示す変数
    //0:何もしていない　1:走っている　2：ジャンプしている　3：ダメージを受けている
    public int state = 0;

    //前フレームの状態を表す変数
    //状態の切り替え判定に使う
    int preState = 0;

    //アニメーターコンポーネント取得のための変数
    private Animator animator;

    //rigidbody2Dコンポーネント取得のための変数
    private Rigidbody2D rigid2D;

    //ジャンプの速度
    private float velocityY = 7f;

    //ジャンプ速度減衰
    private float dump = 0.5f;

    //横移動の速度
    private float velocityX = 5f;

    //地面と衝突しているかの判定
    public bool isGround;

    //敵と衝突しているかの判定
    bool isEnemy;

    //オブジェクトのスケール（左右反転用）
    float Scale = 0.8f;

    //オーディオクリップ再生用オーディオソースコンポーネント取得のための変数
    AudioSource audioSource;

    //オーディオクリップ指定
    public AudioClip SEJump;
    public AudioClip SEDead;

    // Start is called before the first frame update
    void Start()
    {
        //アニメーターコンポーネント取得
        this.animator = GetComponent<Animator>();

        //rigidbody2Dコンポーネント取得
        this.rigid2D = GetComponent<Rigidbody2D>();

        //オーディオソースコンポーネント取得
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //接地、または敵と接触の際の引数の変数を設定
        this.animator.SetBool("isGround", isGround);
        this.animator.SetBool("isEnemy", isEnemy);

        //状態切り替わり判定(stateと数値が合わない場合はStateStartが呼び出され、その後stateと数値を合わせ、状態が切り替わるとstateの数値が変わるため、都度変化する)
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

        //Groundタグをつけた地面オブジェクトに接触したか
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        //Enemyタグをつけた敵オブジェクトに接触しているか否か
        if (collision.gameObject.tag == "Enemy")
        {
            isEnemy = true;
        }
    }
        
    //各状態切り替わり時に1回だけ実行される処理
    public void StateStart()
    {
        //何もしていない状態：速度とアニメーションをリセットする
        if (state == 0)
        {
            this.rigid2D.velocity = new Vector2(0, 0);
            this.animator.SetBool("Run", false);
            audioSource.Stop();
        }

        //走っている状態：走りアニメーションを再生する
        if (state == 1)
        {
            this.animator.SetBool("Run", true);
            audioSource.Play();
        }

        //ジャンプしている状態：走りアニメーションを停止、上方向に速度を追加
        if (state == 2)
        {
            isGround = false;
            audioSource.Stop();
            audioSource.PlayOneShot(SEJump, 1.0f);
            this.animator.SetBool("Run", false);
            this.rigid2D.velocity = new Vector2(0, this.velocityY);
        }

        //ゲームオーバー　速度をリセットする
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

    //各状態のUpdate処理
    void StateUpdate()
    {
        //何もしていない状態：走ったりジャンプができる
        if (state == 0)
        {
            //→キーを押すと横移動
            //体の向きもデフォルト（→方向）に変える
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localScale = new Vector2(Scale, Scale);
                this.rigid2D.velocity = new Vector2(velocityX, this.rigid2D.velocity.y);
            }

            //←キーを押すと横移動、Scaleのx軸をマイナスにすることで体を反転
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector2(-Scale, Scale);
                this.rigid2D.velocity = new Vector2(-velocityX, this.rigid2D.velocity.y);
            }

            //スペースキーを押すとジャンプする
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                this.rigid2D.velocity = new Vector2(0, this.velocityY);
            }

            //スペースキーを離した場合高度が下がる
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (this.rigid2D.velocity.y > 0)
                {
                    this.rigid2D.velocity *= this.dump;
                }
            }
        }

        //走っている状態：走ったりジャンプができる   
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

        //ジャンプしている状態：走れるがジャンプはできない
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

    //各状態の切り替わり処理
    void StateChange()
    {

        //何もしていない状態：入力があれば走り、ジャンプへ
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

        //走っている状態：入力なければ何もしていない状態へ、ジャンプ入力でジャンプ状態へ
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

        //ジャンプ状態：着地したら何もしていない状態へ
        else if (state == 2)
        {
            if (isGround)
            {
                state = 0;
            }
        }

        //敵に当たるとゲームオーバー
        if (isEnemy)
        {
            state = 3;
        }
    }
}
