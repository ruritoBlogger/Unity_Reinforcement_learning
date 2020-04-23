using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    // PlayGameがクリックされたときゲームを実行する
    public void OnClick()
    {
        SceneManager.LoadScene("PlayGame");
    }
}
