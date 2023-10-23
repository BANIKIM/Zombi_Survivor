using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIController : MonoBehaviour
{
    public static UIController instance = null;//싱글톤 선언
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }//싱글톤 기본 형태


    /*
     * 탄약 표시용 텍스트
     * 점수 표시 텍스트 -> Gamemanager -> r관리
     * 적 웨이브
     * 게임 오버 오브젝트
     */
    [SerializeField] private Text AmmoText;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text Wave_Text;

    [SerializeField] private GameObject Gameover_ob;


    //탄약 업데이트
    public void Update_AmmoText(int magAmmo, int Remain)
    {
        // 25 / 100
        AmmoText.text = string.Format("{0} / {1}",magAmmo, Remain);
    }

    public void Update_ScoreText(int newScore)
    {
        //Score : 00
        ScoreText.text = string.Format("Score : {0}", newScore);
    }

    public void Update_WaveText(int Wave, int Count)
    {
        //Wave : 0
        //Zom
        Wave_Text.text = string.Format("Wave : {0}\nZomdie Left : {1}", Wave, Count);
    }

    public void SetActive_Gameover(bool isAct)
    {
        Gameover_ob.SetActive(isAct);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//현재 씬을 다시 불러온다
        Gameover_ob.SetActive(false);//게임오버 화면 비활성화
    }
}
