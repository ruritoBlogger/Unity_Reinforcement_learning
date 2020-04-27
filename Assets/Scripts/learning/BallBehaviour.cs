using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour
{
    // 底の壁に設定しているtagの名前
    private string bottom_wall_name = "bottom";

    // ブロックに設定しているtagの名前
    private string block_name = "enemy";
    
    // 残っているブロックの数を画面に表示する
    public Text scoreText;

    // 残っているブロックの数
    private int blocks;
    
    // ボールの初速度
    public float speed = 10;

    // ボールの初期位置


    // trainerを保持しているオブジェクト
    GameObject trainer;

    // trainerを保持しているオブジェクト名
    private string trainer_tag = "trainer";

    // trainerのscript
    Trainer script;

    void Start()
    {
        trainer = GameObject.Find(trainer_tag);
        script = trainer.GetComponent<Trainer>();
        Reset();
    }

    void Update()
    {
        FixedBallVector();
    }

    private void Reset()
    {
        // ブロックの総数を数える
        GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(block_name);
        blocks = tagObjects.Length;

        // ボールの位置と速度を初期化
        this.gameObject.transform.position = new Vector3(0f, 0f, 15f);   
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        // ボールの初動を設定
        var force = (transform.forward + transform.right) * speed;
        GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }

    private void FixedBallVector()
    { 
        // 移動する方向のベクトルを正規化します。
        Vector2 velocityNormalized = GetComponent<Rigidbody>().velocity.normalized;

        // 何もしないと角にぶつかったときに水平に移動を始める場合があります。
        // それではゲームに支障をきたすので、移動する方向が一定範囲の角度の場合は、許可される範囲に丸めます。
        float limitVerticalDeg = 10f;   // 垂直方向は 90 ± 10 度、270 ± 10 度の範囲の角度は近いほうに寄せる。
        float limitHorizontalDeg = 45f; // 水平方向は 0 ± 45 度、 180 ± 45 度の範囲の角度は近いほうに寄せる。
        if (velocityNormalized.x >= 0f)
        {
            velocityNormalized.x = Mathf.Clamp(velocityNormalized.x, Mathf.Cos(Mathf.Deg2Rad * (90 - limitVerticalDeg)), Mathf.Cos(Mathf.Deg2Rad * (0 + limitHorizontalDeg)));
        }
        else
        {
            velocityNormalized.x = Mathf.Clamp(velocityNormalized.x, Mathf.Cos(Mathf.Deg2Rad * (180 - limitHorizontalDeg)), Mathf.Cos(Mathf.Deg2Rad * (90 + limitVerticalDeg)));
        }
        if (velocityNormalized.y >= 0f)
        {
            velocityNormalized.y = Mathf.Clamp(velocityNormalized.y, Mathf.Sin(Mathf.Deg2Rad * (180 - limitHorizontalDeg)), Mathf.Sin(Mathf.Deg2Rad * (90 + limitVerticalDeg)));
        }
        else
        {
            velocityNormalized.y = Mathf.Clamp(velocityNormalized.y, Mathf.Sin(Mathf.Deg2Rad * (270 - limitVerticalDeg)), Mathf.Sin(Mathf.Deg2Rad * (180 + limitHorizontalDeg)));
        }
    }

    void OnCollisionEnter( Collision other )
    {
        // ブロックと玉が衝突した場合、表示している数値を減らす
        if( other.gameObject.CompareTag(block_name))
        {
            blocks--;
            other.gameObject.SetActive(false);
            SetUI();

            // 全てのブロックを削除できた場合環境のリセットを実行
            if( blocks == 0 )
            {
                script.Reset();
                Reset();
            }
        }
        else
        {
            if ( other.gameObject.CompareTag(bottom_wall_name) )
            {
                script.Reset();
                Reset();
            }
        }
        /*
        // 底の壁と玉が接触した場合環境のリセットを実行
        else if ( other.gameObject.CompareTag(bottom_wall_name) )
        {
            script.Reset();
            Reset();
        }
        */
    }
    
    // UIの設定
    private void SetUI()
    {
        scoreText.text = "Count: " + blocks.ToString();
    }

}
