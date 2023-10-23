using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie_Controller : LivingEntity
{
    [Header("������ ��� ���̾�")]
    public LayerMask TargetLayer;
    private LivingEntity Targetentity;

    // ��θ� ����� AI Agent
    private NavMeshAgent agent;

    [Header("ȿ��")]
    [SerializeField] private AudioClip DeathClip;
    [SerializeField] private AudioClip HitClip;
    [SerializeField] private ParticleSystem HitEffect;

    private Animator zomdie_ani;
    private AudioSource zomdie_audio;

    //��� �������� ���� �� �ִ� ������ �ܾ�
    private Renderer zomdie_renderer;



    /*
     * ���߿� ������Ʈ �߰�
     */

    [Header("Info")]
    [SerializeField] private float Damage = 20f;
    [SerializeField] private float TimebetAttack = 0.5f;
    private float LastAttackTimebet;

    private bool isTarget
    {
        get
        {
            if (Targetentity != null && !Targetentity.isDead)
            {
                return true;
            }
            return false;
        }

    }

    private void Awake()
    {
        TryGetComponent(out agent);
        TryGetComponent(out zomdie_ani);
        TryGetComponent(out zomdie_audio);

        /*
         * GetComponentInChildren ->
         * Ư�� ������Ʈ�� ���� ��ü(�ڽ�) �߿� ���� ���ο� �ִ� ������Ʈ�� ��ȯ
         * 
         * GetComponentsInChildren ->
         * Ư�� ������Ʈ�� ���� ��ü(�ڽ�) ���� ��� ������Ʈ�� ��ȯ�մϴ�.
         * �̶� ��ȯ���´� �迭�� ��ȯ�մϴ�.
         */

        zomdie_renderer = GetComponentInChildren<Renderer>();
    }


    public void Setup(Zombie_Data data)
    {
        StartHeath = data.Health;//ü�º���
        Damage = data.Damage;//������ ����

        agent.speed = data.Speed;//�ӵ� ����
        zomdie_renderer.material.color = data.Skincolor;//���󺯰�
    }


    public override void OnDamage(float Damage, Vector3 hitposition, Vector3 hitNomal)
    {
        /*
         * ������ ����...
         * �÷��̾� ���� �Ѿ��� �¾��� ��
         * �Ѿ��� ������ ������� ����ؾ� �ϰ�, HitEffect -> ���� �Ѿ��� ����� ����
         */
        if (!isDead)
        {
            HitEffect.transform.position = hitposition;
            //hit ȸ������ �ٶ󺸴� ȸ���� ���·� ��ȯ
            HitEffect.transform.rotation = Quaternion.LookRotation(hitNomal);

            HitEffect.Play();
            zomdie_audio.PlayOneShot(HitClip);
        }
        base.OnDamage(Damage, hitposition, hitNomal);
    }

    public override void Die()
    {
        base.Die();
        Collider[] colls = GetComponents<Collider>();
        foreach (Collider c in colls)
        {
            c.enabled = false;
        }
        agent.isStopped = true;
        agent.enabled = false;
        zomdie_ani.SetTrigger("Die");
    }

    private void OnTriggerStay(Collider other)
    {
        //��� ���� �� -> ���������� ȣ���� �ȴ�.
        /*
         * enter -> ��� ����
         * stay -> ��� ���� ��
         * exit -> ��� ���� ������
         */
        if(!isDead&&Time.time>=LastAttackTimebet+TimebetAttack)
        {
            if(other.TryGetComponent(out LivingEntity e))
            {
                if(Targetentity.Equals(e))
                {
                    LastAttackTimebet = Time.time;
                    //ClosestPoint -> ��� ��ġ
                    //�� ���� �ǰ� ��ġ�� �ǰ� ������ �ٻ簪���� ���
                    Vector3 hitpoint = other.ClosestPoint(transform.position);
                    Vector3 hitnormal = transform.position - other.transform.position;

                    //Player�� �´°�
                    e.OnDamage(Damage, hitpoint, hitnormal);
                }
            }
        }
    }

    private void Update()
    {
        zomdie_ani.SetBool("HasTarget",isTarget);
    }
    private void Start()
    {
        StartCoroutine(Upsete_Tarposition());
    }

    private IEnumerator Upsete_Tarposition()
    {
        while (!isDead)
        {
            if(isTarget)
            {
                agent.isStopped = false;
                agent.SetDestination(Targetentity.transform.position);
            }
            else
            {
                agent.isStopped = true;
                Collider[] coll = Physics.OverlapSphere(transform.position, 20f, TargetLayer);
                for (int i = 0; i < coll.Length; i++)
                {
                    if(coll[i].TryGetComponent(out LivingEntity e))
                    {
                        if(!e.isDead)
                        {
                            Targetentity = e;
                            break;
                        }
                    }
                }
            }
            yield return null; //�� �����Ӿ�
        }
    }

}
