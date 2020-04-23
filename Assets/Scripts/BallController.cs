using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("FUCK UNCHI!!!l");
    }

    // ブロックと玉が衝突した場合、表示している数値を減らす
    //void OnTriggerEnter(Collider other)
    void OnCollisionEnter( Collision other )
    {
        if( other.gameObject.CompareTag(tag_name))
        {
            blocks--;
            other.gameObject.SetActive(false);
            SetUI();
            Debug.Log("test");
        }
        else
        {
            Debug.Log("FUCK UNCHI!!!l");
        }
    }

    // UIの設定
    void SetUI()
    {
        scoreText.text = "Count: " + blocks.ToString();
    }
}
