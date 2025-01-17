using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Anim_Proc : MonoBehaviour
{
    [SerializeField] private EnemyBrain PlCon;

    public void Attack_Player()
    {
        PlCon.Attack_Player();
    }
    public void Set_Idle_Anim()
    {
        PlCon.Set_Idle_Anim();
    }
    public void Clear_Space()
    {
        PlCon.Clear_Space();
    }
}
