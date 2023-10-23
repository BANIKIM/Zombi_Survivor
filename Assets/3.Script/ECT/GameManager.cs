using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    //�̱��� _ ���� 2

    //���������ڰ� private�̱� ������ ������ �Ұ��� �ϴ�. static�̿��� ������ �� ������...
    private static GameManager instace = null;
    //�ν��Ͻ� ĸ��ȭ
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

    //�̱��� _ ���� 2 End

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
