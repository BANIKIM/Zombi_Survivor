using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Zombie_Data", fileName = "Zombie_Data")]
public class Zombie_Data : ScriptableObject
{


    public float Health = 100f;
    public float Damage = 20f;
    public float Speed = 2f;

    public Color Skincolor = Color.white;
}
