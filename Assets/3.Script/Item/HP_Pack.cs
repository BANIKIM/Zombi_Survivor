using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Pack : MonoBehaviour,IItem
{
    public float health = 50; // ü���� ȸ���� ��ġ

    public void Use(GameObject target)
    {
        // ���޹��� ���� ������Ʈ�κ��� LivingEntity ������Ʈ �������� �õ�
        LivingEntity life = target.GetComponent<LivingEntity>();

        // LivingEntity������Ʈ�� �ִٸ�
        if (life != null)
        {
            Debug.Log("ȸ��");
            // ü�� ȸ�� ����
            life.Retore_health(health);
        }

        // ���Ǿ����Ƿ�, �ڽ��� �ı�
        Destroy(gameObject);
    }
}
