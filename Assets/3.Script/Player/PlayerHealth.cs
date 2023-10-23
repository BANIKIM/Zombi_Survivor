using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : LivingEntity
{
    /*
     * 
     */
    public Slider healthSlider;

    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemDropClip;

    private AudioSource playeraudio;
    private Animator player_ani;

    private PlayerShooter player_shooter;
    private Plyer_Movement plyer_move;


    private void Awake()
    {
        TryGetComponent(out playeraudio);
        TryGetComponent(out player_ani);
        TryGetComponent(out plyer_move);
        TryGetComponent(out player_shooter);
    }

    protected override void OnEnable()
    {
        base.OnEnable();//-> �θ��� Ŭ������ �޼ҵ带 ȣ��

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = StartHeath;
        healthSlider.value = Heath;


        //�׾��� �� move shoot�� ��Ȱ��ȭ �Ҳ��� ������
        //���⼭ Ȯ���� Ȱ��ȭ 
        plyer_move.enabled = true;
        player_shooter.enabled = true;
    }
    public override void OnDamage(float Damage, Vector3 hitposition, Vector3 hitNomal)
    {
        if(!isDead)
        {
            playeraudio.PlayOneShot(hitClip);
        }

        base.OnDamage(Damage, hitposition, hitNomal);

        healthSlider.value = Heath; // ü�¹� Update;

    }


    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);
        //�ð��� ȿ�� û���� ȿ��
        player_ani.SetTrigger("Die");
        playeraudio.PlayOneShot(deathClip);

        plyer_move.enabled = false; // ������Ʈ Ȱ��ȭ ��Ȱ��ȭ�� �� �� �ִ� �޼ҵ�
        player_shooter.enabled = false;

    }

}
