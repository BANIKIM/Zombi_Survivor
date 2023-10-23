using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamage
{
    //��� ����ü���� ���̴� ������Ʈ
    /*
     * ��üü��
     * ����ü��
     * �׾����� ��Ҵ��� -> �̺�Ʈ�� ó���� �Ұ��ε� ����Ƽ���� �����ϴ� �׼��̶�� ����
     * 
     */

    public float StartHeath = 100f;
    public float Heath { get; protected set; }
    public bool isDead { get; protected set; }
    public event Action OnDead;//���� using System�� �����ؾ� �� �� �ִ�.

    protected virtual void OnEnable()
    {
        //���� �ʱ�ȭ
        isDead = false;
        Heath = StartHeath;
    }


    public virtual void OnDamage(float Damage, Vector3 hitposition, Vector3 hitNomal)
    {
        Heath -= Damage;
        //�׾����� ���׾�����
        if(Heath<=0 && !isDead)
        {
            //�״� �޼ҵ带 ȣ��
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
