using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamage
{
    //모든 생명체에게 붙이는 컴포넌트
    /*
     * 전체체력
     * 현재체력
     * 죽었는지 살았는지 -> 이벤트로 처리를 할것인데 유니티에서 지원하는 액션이라고 있음
     * 
     */

    public float StartHeath = 100f;
    public float Heath { get; protected set; }
    public bool isDead { get; protected set; }
    public event Action OnDead;//엑션 using System를 선언해야 쓸 수 있다.

    protected virtual void OnEnable()
    {
        //변수 초기화
        isDead = false;
        Heath = StartHeath;
    }


    public virtual void OnDamage(float Damage, Vector3 hitposition, Vector3 hitNomal)
    {
        Heath -= Damage;
        //죽었는지 안죽었는지
        if(Heath<=0 && !isDead)
        {
            //죽는 메소드를 호출
            Die();
        }
    }
    public virtual void Die()
    {
        if (OnDead != null)
        {
            OnDead();
        }
        isDead = true;
    }

    public virtual void Retore_health(float newHealth)
    {
        if(isDead)
        {
            return;
        }
        Heath += newHealth;
    }


}
