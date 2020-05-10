using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    private List<List<double>> Q = new List<List<double>>();
    private double gamma;
    private double learning_rate;
    private double eplison;

    public void Init( bool isLoadData, double tmp_gamma, double tmp_learning_rate, double tmp_eplison )
    {
        if( isLoadData )
        {
            // Text形式で吐き出したファイルを読み込む
        }

        gamma = tmp_gamma;
        learning_rate = tmp_learning_rate;
        eplison = tmp_eplison;

        // Qtableの初期化
        for( int i = 0; i < 3; i++ )
        {
            List<double> tmp = new List<double>();
            tmp.Add(0.0);
            tmp.Add(0.0);
            tmp.Add(0.0);
            Q.Add(tmp);
        }
    }

    public int Policy( int state )
    {
        double key = Random.value;

        // eplison以上ならmax(Q[state])の行動を選択する
        if (eplison <= key)
        {
            if (Q[state].Count > state)
            {
                Debug.Log(Q[state][0] + " " + Q[state][1]);
                return (Q[state][0] > Q[state][1]) ? 0 : 1;
            }
            else
            {
                Debug.Log("Q値のアクセス部分において配列外参照しています。引数のstateを確認してください");
                return 0;
            }
        }
        else
        {
            double r1 = Random.value;
            if( r1 > 0.5 )
            {
                return 0;
            }
            else
            {
                return 1;
            }
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
    
    public void Move( int action )
    {
        // 0なら左
        if( action == 0 )
        {
            var force = transform.right * 5000;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
        else
        {
            var force = transform.right * -5000;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

    public List<double> GetQtable( int state )
    {
        return Q[state];
    }
}
