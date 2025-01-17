using UnityEngine;

public class Player_Anim_Proc : MonoBehaviour
{
    [SerializeField] private Player_Control Pl;
    [SerializeField] private CharProperty pstats;
    [SerializeField] private EnemyBrain EnBr;
    [SerializeField] private Inventory inv;
    private FindNearestEnemy Enem;


    public void Atack_Zombde()
    {
        Enem.Update_Enemy_List();
        GameObject nearestEnemy = Enem.FindClosestEnemy();
        if (nearestEnemy != null)
        {
            if (Pl.GetComponent<CharProperty>().GunId>0 & inv.CheckId(3) & Enem.LastDist< 4 )
            {
                nearestEnemy.GetComponent<EnemyBrain>().Set_Damage(pstats.Damage);
            } else if (Enem.LastDist < 0.5) nearestEnemy.GetComponent<EnemyBrain>().Set_Damage(10);
        }
        if (Pl.GetComponent<CharProperty>().GunId > 0 & inv.CheckId(3))     //Если не нужно тратить патроны в холостую, то можно убрать
            inv.RemoveAmmo(1);
    }

    public void Player_Idle()
    {
        Pl.Player_Idle();
    }
    public void Player_Dead()
    {
        //  _enbrain._playerIsDead = true;  Сделать Event (Action)
        Pl.Player_Dead();
    }
    void Start()
    {
        Enem = Pl.GetComponent<FindNearestEnemy>();
        EnBr = GameObject.FindGameObjectWithTag("Enemie").GetComponent<EnemyBrain>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
}
