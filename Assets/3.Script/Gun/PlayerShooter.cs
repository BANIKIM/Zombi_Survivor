using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    /*
     * �ѽ�� ������ -> Gun 
     * Player input
     * Gun ������Ʈ �տ� ���߱� -> animator
     * 
     */

    public Gun gun;

    //�ѱ� ��ġ ���߱� ���� Transform��
    public Transform gunpivot;
    public Transform LeftHand_Mount;
    public Transform RightHand_Mount;

    [SerializeField] private Animator animator;
    [SerializeField] private Player_Input input;

    private void Start()
    {
        input = GetComponent<Player_Input>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //input���õ� �̺�Ʈ ȣ��
        if(input.isFire)
        {
            gun.Fire();
        }
        else if(input.isReload)
        {
            if(gun.Reload())
            {
                animator.SetTrigger("Reload");
            }
        }    
    }


    private void OnAnimatorIK(int layerIndex)
    {
        //���� �������� ������ �Ȳ�ġ�� �̵� 
        gunpivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);


        //ik�� ����Ͽ� �޼��� ��ġ�� ȸ���� �� ���� �����̿� ����
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);//SetIKPositionWeight ����ġ�� �ο� �� �� �ִ�.
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHand_Mount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHand_Mount.rotation);

        //ik�� ����Ͽ� �������� ��ġ�� ȸ���� �� �����̿� ����
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, RightHand_Mount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, RightHand_Mount.rotation);
    }

}
