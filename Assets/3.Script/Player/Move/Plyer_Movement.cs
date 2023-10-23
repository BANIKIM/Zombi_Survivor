using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer_Movement : MonoBehaviour
{
    [SerializeField] private float Movespeed = 5f;
    [SerializeField] private float RotateSpeed = 180f;

    [SerializeField] private Player_Input player_input;
    private Rigidbody player_r;
    private Animator player_ani;

    private void Start()
    {
        TryGetComponent(out player_input);
        TryGetComponent(out player_r);
        TryGetComponent(out player_ani);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        player_ani.SetFloat("Move",player_input.Move_Value);
    }
    private void Move()
    {
        Vector3 moveDirection = player_input.Move_Value * transform.forward * Movespeed * Time.deltaTime;

        player_r.MovePosition(player_r.position + moveDirection);//플레이어의 포지션을 moveDirection만큼 움직여 주세요
    }

    private void Rotate()
    {
        float turn = player_input.Rotate_Value * RotateSpeed * Time.deltaTime;

        player_r.rotation = player_r.rotation * Quaternion.Euler(0, turn, 0);

    
    }
}
