using UnityEngine;
using UnityEngine.UI;

public class HPBarPlayer : MonoBehaviour
{
    [SerializeField] public Slider HealthBar;

    public void Change_Max_Health(float NewHealth)
    {
        HealthBar.maxValue = NewHealth;
    }

    public void Change_Health_Bar(float NewHealth)
    {
        HealthBar.value = NewHealth;
        if (NewHealth <= 0)
            HealthBar.value = 0;
    }

}
