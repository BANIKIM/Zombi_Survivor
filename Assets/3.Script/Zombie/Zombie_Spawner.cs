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
        //SpawnPoint ����
        Setup_SpawnPoint();
    }

    private void Setup_SpawnPoint()
    {
        spawnPoint = new Transform[transform.childCount];

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            //GetChild(index) �ڽ� ��ü�� ������� ������ �ö� ����Ѵ�.
            spawnPoint[i] = transform.GetChild(i).transform;
        }
    }

    private void Update()
    {
        //���� ������ ���
        if (GameManager.Instance != null && GameManager.Instance.isGameover)
        {
            return;
        }
        //Length ���̰� ������ ���� �� / Count ���̰� ������ ���� ���� ��
        if (zombie_List.Count<=0)
        {
            //���̺� �ø��� �޼ҵ� �ֱ�
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
        //���̺� ����
        Wave++;
        //���� ���� �� ���� ������� ������.
        int count = Mathf.RoundToInt(Wave * 2f);
        for (int i = 0; i < count; i++)
        {
            Create_Zombie();
        }

    }
    private void Create_Zombie()
    {
        /*
         * zomdie data �����ϰ� ������
         * zombie Spawnpoint �����ϰ� ������
         * 
         * �����̰� ���̵Ǿ��� ��
         * 
         * -> Event �߰�
         * 1.List ���� ����
         * 2.������ ������Ʈ ����
         * 3.���� ���
         */

        Zombie_Data data = zomdie_Datas[Random.Range(0, zomdie_Datas.Length)];
        Transform point = spawnPoint[Random.Range(0, spawnPoint.Length)];

        Zombie_Controller zombie = Instantiate(this.zombie,point.position, point.rotation);
        zombie.Setup(data);
        
        zombie_List.Add(zombie);
        //���� �����Ǿ��� ��


        //�͸� �Լ�
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
