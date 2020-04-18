using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10;
    void Start()
    {
        var force = ( transform.forward + transform.right ) * speed;
        GetComponent<Rigidbody>().AddForce( force, ForceMode.VelocityChange );  
    }
}
