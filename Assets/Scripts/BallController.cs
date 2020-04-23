using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10;
    
    // 残っているブロックの数
    public int blocks;

    // ブロックに設定しているtagの名前
    private string tag_name = "enemy";

    // 残っているブロックの数を画面に表示する
    public Text scoreText;

    void Start()
    {
        // 初動の設定
        var force = ( transform.forward + transform.right ) * speed;
        GetComponent<Rigidbody>().AddForce( force, ForceMode.VelocityChange );

        // ブロックの総数を数える
        GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(tag_name);
        blocks = tagObjects.Length; 
            
        SetUI();
    }

    private void Update()
    {
        FixedBallVector();
    }

    // ブロックと玉が衝突した場合、表示している数値を減らす
    void OnCollisionEnter( Collision other )
    {
        if( other.gameObject.CompareTag(tag_name))
        {
            blocks--;
            other.gameObject.SetActive(false);
            SetUI();

            // 全てのブロックを削除できた場合初期画面に遷移
            if( blocks == 0 )
            {
                SceneManager.LoadScene("StartWindow");
            }
        }
    }

    // UIの設定
    private void SetUI()
    {
        scoreText.text = "Count: " + blocks.ToString();
    }

    // ボールが水平にならないようにする
    private void FixedBallVector()
    {
        // 移動する方向のベクトルを正規化します。
        Vector2 velocityNormalized = GetComponent<Rigidbody>().velocity.normalized;

        // 何もしないと角にぶつかったときに水平に移動を始める場合があります。
        // それではゲームに支障をきたすので、移動する方向が一定範囲の角度の場合は、許可される範囲に丸めます。
        float limitVerticalDeg = 20f;   // 垂直方向は 90 ± 10 度、270 ± 10 度の範囲の角度は近いほうに寄せる。
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
}
