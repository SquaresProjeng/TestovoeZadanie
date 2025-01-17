using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int slotCount = 4; // Количество ячеек в инвентаре
    private InventorySlot[] slots;
    [SerializeField] Image[] spritebutton;
    [SerializeField] Text[] textbutton;
    [SerializeField] Sprite defSprite;
    [SerializeField] GameObject delButton;
    public int slotSelected;

    void Start()
    {
        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            slots[i] = new InventorySlot();
        }
        UpdateUI();
        slotSelected = 0;
        delButton.SetActive(false);
    }

    public void SelectSlot(int id)
    {
        slotSelected = id;
        delButton.SetActive(true);
    }
    public bool CheckId(int id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.id == id)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null || (slots[i].item.isStackable && slots[i].item.id == itemToAdd.id))
            {
                slots[i].AddItem(itemToAdd);
                UpdateUI(); // Обновляем интерфейс после добавления
                return true;
            }
        }
        Debug.Log("Нет свободных ячеек для добавления предмета!");
        delButton.SetActive(false);
        return false;
    }
    public void FullRemoveItemSelected()
    {
        if (slotSelected >= 0 && slotSelected < slots.Length)
        {
            slots[slotSelected].RemoveItem(1000);
            UpdateUI(); // Обновляем интерфейс после удаления
        }
        delButton.SetActive(false);
    }
    public void RemoveAmmo(int quantity)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.id == 3)
                {
                    slots[i].RemoveItem(quantity);
                }
            }
        }
        UpdateUI(); // Обновляем интерфейс после удаления
        delButton.SetActive(false);
    }
    public void RemoveItem(int slotIndex, int quantity)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            slots[slotIndex].RemoveItem(quantity);
            UpdateUI(); // Обновляем интерфейс после удаления
        }
        delButton.SetActive(false);
    }

    private void UpdateUI()
    {
        // Здесь вы можете обновить отображение иконок предметов в UI.
        if (slots.Length == spritebutton.Length & slots.Length == textbutton.Length)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    spritebutton[i].sprite = slots[i].item.itemSprite;
                    if (slots[i].item.quantity > 1) 
                        textbutton[i].text = slots[i].item.quantity.ToString(); 
                    else textbutton[i].text = "";

                    Debug.Log($"Ячейка {i}: {slots[i].item.name}, Количество: {slots[i].item.quantity}");
                }
                else
                {
                    spritebutton[i].sprite = defSprite;
                    textbutton[i].text = "";
                    Debug.Log($"Ячейка {i}: Пусто");
                }
            }
        }

    }
}

[System.Serializable]
public class Item
{
    public int id; // ID предмета
    public string name; // Название предмета
    public int quantity; // Количество предметов
    public bool isStackable; // Можно ли группировать предметы
    public Sprite itemSprite;

    public Item(int id, string name, int quantity, bool isStackable, Sprite itemSprite)
    {
        this.id = id;
        this.name = name;
        this.quantity = quantity;
        this.isStackable = isStackable;
        this.itemSprite = itemSprite;
    }
}

public class InventorySlot
{
    public Item item; // Предмет в ячейке
    [SerializeField] Sprite image;

    public void AddItem(Item newItem)
    {
        if (item == null) 
        {
            item = newItem; // Если ячейка пустая, добавляем предмет
        }
        else if (item.isStackable && item.id == newItem.id)
        {
            item.quantity += newItem.quantity; // Увеличиваем количество, если предметы одного типа
        }
        else
        {
            Debug.Log("Ячейка уже занята другим предметом!");
        }
    }

    public void RemoveItem(int quantity)
    {
        if (item != null)
        {
            item.quantity -= quantity;

            if (item.quantity <= 0)
            {
                item = null; // Удаляем предмет, если количество стало 0 или меньше
            }
        }
    }

    public void ClearSlot()
    {
        item = null; // Очищаем ячейку
    }
}
