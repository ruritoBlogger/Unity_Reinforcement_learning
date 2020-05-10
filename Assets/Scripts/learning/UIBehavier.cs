using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavier : MonoBehaviour
{
    // 残っているブロックの数を画面に表示する
    public Text scoreText;

    // Qtableの情報を画面に表示する
    public Text LogText;

    public void SetScoreText( int block_num )
    {
        scoreText.text = "Count: " + block_num;
    }

    public void SetLogText( List<double> table )
    {
        if (table.Count > 2)
        {
            LogText.text = "Qtable: [" + (int)(table[0]) + "," + (int)(table[1]) + "," + (int)(table[2]) + "]";
        }
    }
}
