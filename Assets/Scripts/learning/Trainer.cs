using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trainer : MonoBehaviour
{
    // ブロックに設定しているtagの名前
    private string block_name = "enemy";

    // ボールに設定しているtagの名前
    private string ball_name = "agent";

        
    // Qtable
    //private double[,] Q = new double[, 3];

    void Start()
    {
        Reset();
    }

    void Update()
    {
        
    }

    // 環境のリセット
    public void Reset()
    {
        // ブロックの状態をリセットする
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(block_name);

        foreach( GameObject block in blocks)
        {
            block.SetActive(true);
        }
        Debug.Log("呼び出されました");
    }
}
