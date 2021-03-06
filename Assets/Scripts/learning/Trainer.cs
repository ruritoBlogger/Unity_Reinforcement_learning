﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trainer : MonoBehaviour
{
    // ここからはハイパーパラメータなど
    //------------------------------------------

    // gamma
    public double gamma = 0.9;

    // learning rate
    public double learning_rate = 0.1;

    public double eplison = 0.01;

    // 学習済みのQtableを読み込むかどうか
    private bool isLoadData = false;

    // もし学習済みのQtableを読み込む場合のファイル
    private string Qtable_name = "Qtable.txt";

    //------------------------------------------
    
    // ここからはUnityの設定
    
    // ブロックに設定しているtagの名前
    private string block_name = "enemy";

    // agentに設定しているtagの名前
    private string agent_name = "agent";

    // observerに設定しているtagの名前
    private string observer_name = "observer";

    // ballに設定しているtagの名前
    private string ball_name = "ball";
    
    //------------------------------------------
    
    // ここからは学習に必要な変数群

    // ブロック
    GameObject[] blocks;

    // agent
    public GameObject agent;

    // observer
    public GameObject observer;

    // ball
    public GameObject ball;

    // UI
    public GameObject UI_Object;


    // 1サイクル前の状況
    private int last_state;

    // 学習回数
    private int episode;
    
    //------------------------------------------

    void Start()
    {
        episode = 0;
        blocks = GameObject.FindGameObjectsWithTag(block_name);
        agent.GetComponent<Agent>().Init(isLoadData, gamma, learning_rate, eplison);

        Reset();
    }

    void Update()
    {
        int state = observer.GetComponent<Observer>().Transform(agent, ball);
        int action = agent.GetComponent<Agent>().Policy(state);
        double reward = observer.GetComponent<Observer>().GetReward(agent, ball);

        agent.GetComponent<Agent>().Learn(last_state, state, action, reward);
        UI_Object.GetComponent<UIBehavier>().SetLogText(agent.GetComponent<Agent>().GetQtable(state)); 
        UI_Object.GetComponent<UIBehavier>().SetEpisodeText(episode); 

        last_state = state;
        agent.GetComponent<Agent>().Move(action);
    }

    // 環境のリセット
    public void Reset()
    {
        // ブロックの状態をリセットする
        foreach( GameObject block in blocks)
        {
            block.SetActive(true);
        }

        // last_stateの初期化
        last_state = observer.GetComponent<Observer>().Transform(agent, ball);
        episode += 1;
    }
}
