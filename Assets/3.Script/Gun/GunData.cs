using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GunData",fileName ="Gun_Data")]//ã�ƺ���

public class GunData : ScriptableObject//ScriptableObject������ ����
{
    /*
     * ���ݷ� => float
     * ����� => float => �ڷ�ƾ
     * ������ �ð�  => float
     * ó�� źâ�뷮 => int
     * �ѼҸ� => audio clip
     * ������ �Ҹ�  => audio clip
     * źâ�뷮 => int
     */

    public float Damage = 25f;
    public float TimebetFire = 0.12f;//�����
    public float ReloadTime = 1.8f;
    public int MagCapacity = 30; // źâ �뷮
    public int StartAmmoRemaion = 100;//���� ź ��

    public AudioClip Shot_clip;
    public AudioClip Reload_clip;



}
