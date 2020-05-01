using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trainer : MonoBehaviour
{
    // ブロックに設定しているtagの名前
    private string block_name = "enemy";

    // agentに設定しているtagの名前
    private string agent_name = "agent";

    // observerに設定しているtagの名前
    private string observer_name = "observer";

    // ballに設定しているtagの名前
    private string ball_name = "ball";

    // ブロック
    GameObject[] blocks;

    // agent
    GameObject agent;

    // observer
    GameObject observer;

    // ball
    GameObject ball;

    // agentのscript
    Agent agent_script;

    // observerのscript
    Observer observer_script;

    // gamma
    private double gamma = 0.9;

    // learning rate
    private double learning_rate = 0.1;

    // 学習済みのQtableを読み込むかどうか
    private bool isLoadData = false;

    // もし学習済みのQtableを読み込む場合のファイル
    private string Qtable_name = "Qtable.txt";

    void Start()
    {
        blocks = GameObject.FindGameObjectsWithTag(block_name);
        agent = GameObject.Find(agent_name);
        agent_script = agent.GetComponent<Agent>();
        agent_script.Init(isLoadData, gamma, learning_rate);

        observer = GameObject.Find(observer_name);
        observer_script = observer.GetComponent<Observer>();

        ball = GameObject.Find(ball_name);

        Reset();
    }

    void Update()
    {
        int state = observer_script.Transform(agent, ball);
        int action = agent_script.Policy(state);

        // next_stateはどうやって持ってこようかな
        int next_state = 0;
        int reward = 0;
        agent_script.Learn(state, next_state, action, reward);
    }

    // 環境のリセット
    public void Reset()
    {
        // ブロックの状態をリセットする
        foreach( GameObject block in blocks)
        {
            block.SetActive(true);
        }
        Debug.Log("呼び出されました");
    }
}
