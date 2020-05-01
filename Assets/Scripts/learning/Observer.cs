using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public int Transform( GameObject agent, GameObject ball )
    {
        if( agent.transform.position.x < ball.transform.position.x )
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
