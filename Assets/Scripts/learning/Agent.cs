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
        for( int i = 0; i < 18; i++ )
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
            if (Q.Count > state)
            {
                int action = 0;
                if( Q[state][0] < Q[state][1]
                    && Q[state][2] < Q[state][1])
                {
                    action = 1;
                }
                else if( Q[state][0] < Q[state][2]
                         && Q[state][1] < Q[state][2] )
                {
                    action = 2;
                }
                return action;
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
            if( r1 < 0.333 )
            {
                return 0;
            }
            else if( r1 < 0.666 )
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }

    public void Learn( int state, int next_state, int action, double reward )
    {
        double max_action = Q[next_state][Policy(next_state)];

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
        else if( action == 1 )
        {
            var force = transform.right * -5000;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
        else
        {
            var force = transform.right * 0;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

    public List<double> GetQtable( int state )
    {
        return Q[state];
    }
}
