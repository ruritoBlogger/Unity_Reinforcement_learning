using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavier : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StartWindow");
    }
}
