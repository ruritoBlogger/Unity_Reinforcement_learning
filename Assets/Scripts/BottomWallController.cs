﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomWallController : MonoBehaviour
{
    private void OnCollisionEnter( Collision collision )
    {
        Destroy( collision.gameObject );
        SceneManager.LoadScene("StartWindow");
    }
}
