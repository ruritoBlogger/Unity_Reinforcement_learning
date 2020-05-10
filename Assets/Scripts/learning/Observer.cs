using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public int Transform( GameObject agent, GameObject ball )
    {
        int state = 0;
        if( Mathf.Abs(agent.transform.position.x - ball.transform.position.x) < 1 )
        {
            state += 2;
        }
        else if( agent.transform.position.x > ball.transform.position.x )
        {
            state += 1;
        }
        if( ball.transform.position.z < 15 )
        {
            state += 3;
        }
        if( agent.transform.position.x < -4 )
        {
            state += 6;
        }
        else if( agent.transform.position.x > 4 )
        {
            state += 12;
        }
        return state;
    }

    public double GetReward( GameObject agent, GameObject ball )
    {
        // ボールを跳ね返せなかった時
        if( agent.transform.position.z > ball.transform.position.z )
        {
            return -1;
        }
        else
        {
            return 0.1;
        }
    }
}
