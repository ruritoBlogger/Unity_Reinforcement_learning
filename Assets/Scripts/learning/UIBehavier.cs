using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavier : MonoBehaviour
{
    // 残っているブロックの数を画面に表示する
    public Text ScoreText;

    // Qtableの情報を画面に表示する
    public Text LogText;

    // 試行回数を画面に表示する
    public Text EpisodeText;

    public void SetScoreText( int block_num )
    {
        ScoreText.text = "Count: " + block_num;
    }

    public void SetEpisodeText( int episode )
    {
        EpisodeText.text = "Episode: " + episode;
    }

    public void SetLogText( List<double> table )
    {
        if (table.Count > 2)
        {
            LogText.text = "Qtable: [" + (int)(table[0]) + "," + (int)(table[1]) + "," + (int)(table[2]) + "]";
        }
    }
}
