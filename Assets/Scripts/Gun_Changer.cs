using UnityEngine;
using UnityEngine.UI;

public class Gun_Changer : MonoBehaviour
{
    [SerializeField] Image GunButton;
    [SerializeField] Sprite[] Sates;
    public Player_Control pC;
    public Inventory inv;
    private int index = 0;

    public void Start()
    {
        pC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        pC.Change_Gun(0);
    }

    public void ChangeButton()
    {
        index += 1;
        if (inv.CheckId(index))
        {
            GunButton.sprite = Sates[1];
            pC.Change_Gun(index);
        } else
        {
            GunButton.sprite = Sates[0];
            pC.Change_Gun(0);
        }
        if (index >= 2) index = 0;
    }
}
