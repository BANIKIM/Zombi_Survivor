using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour,IItem
{
    public int ammo = 30;

    public void Use(GameObject target)
    {
        // ���� ���� ���� ������Ʈ�κ��� PlayerShooter ������Ʈ�� �������� �õ�
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        // PlayerShooter ������Ʈ�� ������, �� ������Ʈ�� �����ϸ�
        if (playerShooter != null && playerShooter.gun != null)
        {
            // ���� ���� źȯ ���� ammo ��ŭ ���Ѵ�
            playerShooter.gun.ammoRemain += ammo;
        }
        Debug.Log("�ı�");
        // ���Ǿ����Ƿ�, �ڽ��� �ı�
        Destroy(gameObject);
    }


}
