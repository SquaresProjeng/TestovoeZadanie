using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Script : MonoBehaviour
{
    [SerializeField] Item_Data[] spriteVariant;
    [SerializeField] private Player_Control PlCon;
    [SerializeField] public bool _playerOnItem = false;
    public Item_Data _item_data;
    private SpriteRenderer sr;
    public bool isRandom = true;
    public int Index = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (Index >= spriteVariant.Length | Index < 0) Index = 0;
        int randomindex;
        if (isRandom)
            randomindex = Random.Range(0, spriteVariant.Length);
        else
            randomindex = Index;
        sr.sprite = spriteVariant[randomindex].itemImage;
        _item_data = spriteVariant[randomindex];
        PlCon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bool stackable = true;
            if (_item_data.index!=3) 
                stackable = false;
            Item _item = new Item(_item_data.index, "NewObject", _item_data.count, stackable, _item_data.itemImage);
            PlCon.GetComponent<Inventory>().AddItem(_item);
            Destroy(gameObject);
        }
    }
}

[System.Serializable]

public class Item_Data
{
    public int index;
    public int count;
    public Sprite itemImage;
}
