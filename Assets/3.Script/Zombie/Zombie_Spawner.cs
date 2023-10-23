using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Spawner : MonoBehaviour
{
    public Zombie_Data[] zomdie_Datas;
    public Zombie_Controller zombie;

    [SerializeField] private Transform[] spawnPoint;

    private List<Zombie_Controller> zombie_List = new List<Zombie_Controller>();

    private int Wave;

    private void Awake()
    {
        //SpawnPoint 설정
        Setup_SpawnPoint();
    }

    private void Setup_SpawnPoint()
    {
        spawnPoint = new Transform[transform.childCount];

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            //GetChild(index) 자식 객체를 순서대로 가지고 올때 사용한다.
            spawnPoint[i] = transform.GetChild(i).transform;
        }
    }

    private void Update()
    {
        //게임 오버일 경우
        if (GameManager.Instance != null && GameManager.Instance.isGameover)
        {
            return;
        }
        //Length 길이가 정해져 있을 때 / Count 길이가 정해져 있지 않을 때
        if (zombie_List.Count<=0)
        {
            //웨이브 늘리는 메소드 넣기
            Spawn_Wave();
        }
        //UI update
        Update_UI();
    }
    private void Update_UI()
    {
        UIController.instance.Update_WaveText(Wave, zombie_List.Count);
    }
    private void Spawn_Wave()
    {
        //웨이브 증가
        Wave++;
        //좀비 생성 및 좀비 몇마리인지 결정함.
        int count = Mathf.RoundToInt(Wave * 2f);
        for (int i = 0; i < count; i++)
        {
            Create_Zombie();
        }

    }
    private void Create_Zombie()
    {
        /*
         * zomdie data 랜덤하게 정해줌
         * zombie Spawnpoint 랜덤하게 정해줌
         * 
         * 좀다이가 다이되었을 때
         * 
         * -> Event 추가
         * 1.List 에서 삭제
         * 2.좀다이 오브젝트 삭제
         * 3.점수 계산
         */

        Zombie_Data data = zomdie_Datas[Random.Range(0, zomdie_Datas.Length)];
        Transform point = spawnPoint[Random.Range(0, spawnPoint.Length)];

        Zombie_Controller zombie = Instantiate(this.zombie,point.position, point.rotation);
        zombie.Setup(data);
        
        zombie_List.Add(zombie);
        //좀비가 생성되었을 때


        //익명 함수
        zombie.OnDead += () => 
        {
            zombie_List.Remove(zombie);
        };
        zombie.OnDead += () =>
        {
            Destroy(zombie.gameObject, 10f);
        };
        zombie.OnDead += () => { GameManager.Instance.AddScore(10); };
    }
}
