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
        base.OnEnable();//-> 부모의 클래스의 메소드를 호출

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = StartHeath;
        healthSlider.value = Heath;


        //죽었을 때 move shoot를 비활성화 할꺼기 때문에
        //여기서 확인차 활성화 
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

        healthSlider.value = Heath; // 체력바 Update;

    }


    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);
        //시각적 효과 청각적 효과
        player_ani.SetTrigger("Die");
        playeraudio.PlayOneShot(deathClip);

        plyer_move.enabled = false; // 컴포넌트 활성화 비활성화를 할 수 있는 메소드
        player_shooter.enabled = false;

    }

}
