using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public int Transform( GameObject agent, GameObject ball )
    {
        if( Mathf.Abs(agent.transform.position.x - ball.transform.position.x) < 1 )
        {
            return 2;
        }
        else if( agent.transform.position.x > ball.transform.position.x )
        {
            return 1;
        }
        else
        {
            return 0;
        }
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
            return 0;
        }
    }
}
