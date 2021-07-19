using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public enum GAME_STATUS
    {
        Ready,//0
        InGame,//1
        GameOver//2
    }

    GAME_STATUS status;//字段

    GAME_STATUS Status
    {
        get { return status; }
        set 
        { 
            status = value;
            UpdataUI();
        }
    }

    public GameObject PanelReady;
    public GameObject PanelIngame;
    public GameObject PanelGameOver;

    public PipelineManager pipelineManager;//管道

    public Player player;//角色

    public int score;//得分

    public Text UI_Score;//当前分数
    public Text UI_ScoreMax;//最高分

    public int Score//得分自动赋值
    {
        get { return score; }
        set
        {
            score = value;
            UI_Score.text = score.ToString();
            if(int.Parse(UI_Score.text) > int.Parse(UI_ScoreMax.text))
                UI_ScoreMax.text = score.ToString();
        }
    }

    void Start()
    {
        PanelReady.SetActive(true);
        Status = GAME_STATUS.Ready;
        player.OnDeath += Player_OnDeath;
        player.OnScore = OnPlayerScore;
    }
    void OnPlayerScore(int score)
    {
        Score += score;
    }

    public void StartGame()
    {
        //游戏开始
        Status = GAME_STATUS.InGame;

        //管道开始生成
        pipelineManager.StartRun();

        //角色进入Fly模式
        player.Fly();

        //积分重置
        Score = 0;
    }
    private void Player_OnDeath()
    {
        Status = GAME_STATUS.GameOver;
        pipelineManager.Stop();
    }
    public void UpdataUI()//UI更新
    {
        PanelReady.SetActive(Status == GAME_STATUS.Ready);
        PanelIngame.SetActive(Status == GAME_STATUS.InGame);
        PanelGameOver.SetActive(Status == GAME_STATUS.GameOver);
    }

    public void Restart()//GameOver按钮点击后返回初始界面
    {
        Status = GAME_STATUS.Ready;
        pipelineManager.Init();
        player.Init();
    }
}
