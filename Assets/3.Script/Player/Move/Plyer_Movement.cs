using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer_Movement : MonoBehaviour
{
    [SerializeField] private float Movespeed = 5f;
    [SerializeField] private float RotateSpeed = 180f;
    [SerializeField] private Camera followCamera;

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

        player_r.MovePosition(player_r.position + moveDirection);//�÷��̾��� �������� moveDirection��ŭ ������ �ּ���
    }

    private void Rotate()
    {
        //Ű���� ȸ��
        /* float turn = player_input.Rotate_Value * RotateSpeed * Time.deltaTime;

         player_r.rotation = player_r.rotation * Quaternion.Euler(0, turn, 0);*/

        //���콺 ȸ��
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayhit;
        if (Physics.Raycast(ray, out rayhit, 100))
        {
            Vector3 nextVec = rayhit.point - transform.position;
            // nextVec.x = 0;
            nextVec.y = 0;
            // nextVec.z = 0;
            transform.LookAt(transform.position + nextVec);
        }


    }
}
