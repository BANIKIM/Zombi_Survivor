using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie_Controller : LivingEntity
{
    [Header("추적할 대상 레이어")]
    public LayerMask TargetLayer;
    private LivingEntity Targetentity;

    // 경로를 계산할 AI Agent
    private NavMeshAgent agent;

    [Header("효과")]
    [SerializeField] private AudioClip DeathClip;
    [SerializeField] private AudioClip HitClip;
    [SerializeField] private ParticleSystem HitEffect;

    private Animator zomdie_ani;
    private AudioSource zomdie_audio;

    //모든 랜더링을 담을 수 있는 마법의 단어
    private Renderer zomdie_renderer;



    /*
     * 나중에 컴포넌트 추가
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
         * 특정 컴포넌트의 하위 객체(자식) 중에 가장 선두에 있는 컴포넌트를 반환
         * 
         * GetComponentsInChildren ->
         * 특정 컴포넌트의 하위 객체(자식) 들의 모든 컴포넌트를 반환합니다.
         * 이때 반환형태는 배열로 반환합니다.
         */

        zomdie_renderer = GetComponentInChildren<Renderer>();
    }


    public void Setup(Zombie_Data data)
    {
        StartHeath = data.Health;//체력변경
        Damage = data.Damage;//데미지 변경

        agent.speed = data.Speed;//속도 변경
        zomdie_renderer.material.color = data.Skincolor;//색상변경
    }


    public override void OnDamage(float Damage, Vector3 hitposition, Vector3 hitNomal)
    {
        /*
         * 좀비의 입장...
         * 플레이어 한테 총알을 맞았을 때
         * 총알을 맞으면 오디오를 출력해야 하고, HitEffect -> 방향 총알이 날라온 방향
         */
        if (!isDead)
        {
            HitEffect.transform.position = hitposition;
            //hit 회전값을 바라보는 회전의 상태로 변환
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
        //닿고 있을 때 -> 지속적으로 호출이 된다.
        /*
         * enter -> 닿기 시작
         * stay -> 닿고 있을 때
         * exit -> 닿는 것이 끝날때
         */
        if(!isDead&&Time.time>=LastAttackTimebet+TimebetAttack)
        {
            if(other.TryGetComponent(out LivingEntity e))
            {
                if(Targetentity.Equals(e))
                {
                    LastAttackTimebet = Time.time;
                    //ClosestPoint -> 닿는 위치
                    //즉 상대방 피격 위치와 피격 방향을 근사값으로 계산
                    Vector3 hitpoint = other.ClosestPoint(transform.position);
                    Vector3 hitnormal = transform.position - other.transform.position;

                    //Player가 맞는것
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
            yield return null; //한 프레임씩
        }
    }

}
