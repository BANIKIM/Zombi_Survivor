using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    //싱글톤 _ 형태 2

    //접근제한자가 private이기 때문에 접근이 불가능 하다. static이여서 전역에 떠 있지만...
    private static GameManager instace = null;
    //인스턴스 캡슐화
    public static GameManager Instance
    {
        get
        {
            if(instace==null)
            {
                instace = FindObjectOfType<GameManager>();
            }
            return instace;
        }
    }

    private void Awake()
    {
        if(Instance==null)
        {
            Destroy(gameObject);
        }
    }

    //싱글톤 _ 형태 2 End

    public int Score = 0;
    public bool isGameover { get; private set; }


    private void Start()
    {
        FindObjectOfType<PlayerHealth>().OnDead += EndGame;
    }

    public void EndGame()
    {
        isGameover = true;
        UIController.instance.SetActive_Gameover(true);
    }
    public void AddScore(int newScore)
    {
        if(!isGameover)
        {
            Score += newScore;
            UIController.instance.Update_ScoreText(Score);
        }
    }
}
