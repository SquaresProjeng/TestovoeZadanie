using UnityEngine;

public class Enemy_Anim_Control : MonoBehaviour
{
    private Animator animator;
    public void Change_Animation(int animstate)
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
                animator.Play("Atack");
                break;
            case 3:
                animator.Play("Dead");
                break;
            default:
                animator.Play("Idle");
                break;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
