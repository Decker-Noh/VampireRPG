using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "Lv." + level;
    }
    public void OnClick()
    {
        if (level == data.damages.Length)
            return;
            switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
                break;
            case ItemData.ItemType.Range:
                if (level ==0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                break;
            case ItemData.ItemType.Heal:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Glove:
                break;
        }
        level++;
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
