using UnityEngine;

public class Character_Anim_Control : MonoBehaviour
{
    private Animator animator;

    public void Change_Animation(int animstate)
    {
        if (animator != null)
        {
            switch (animstate)
            {
                case 0:
                    animator.Play("Idle");
                    break;
                case 1:
                    animator.Play("Walk");
                    break;
                case 2:
                    animator.Play("Dead");
                    break;
                case 3:
                    animator.Play("Punch_Atack");
                    break;
                case 4:
                    animator.Play("Gun_Attack");
                    break;
            }
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
