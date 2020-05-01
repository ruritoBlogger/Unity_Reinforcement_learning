using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    private double[,] Q = new double[2, 2];
    private double gamma;
    private double learning_rate;

    public void Init( bool isLoadData, double tmp_gamma, double tmp_learning_rate )
    {
        if( isLoadData )
        {
            // Text形式で吐き出したファイルを読み込む
        }

        gamma = tmp_gamma;
        learning_rate = tmp_learning_rate;
    }

    public int Policy( int state )
    {
        try
        {
            return (Q[state][0] > Q[state][1]) ? 0 : 1;
        }
        catch( IndexOutOfRangeException e )
        {
            Debug.Log("Q値のアクセス部分において配列外参照しています。引数のstateを確認してください");
            return 0;
        }
    }

    public void Learn( int state, int next_state, int action, double reward )
    {
        double max_action = Q[next_state][0];
        if( Q[next_state][0] < Q[next_state][1] )
        {
            max_action = Q[next_state][1];
        }

        double G = reward + gamma * max_action;
        Q[state][action] += learning_rate * (G - Q[state][action]);
    }
}
