using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scroiptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Melee,
        Range,
        Glove,
        Shoe,
        Heal
    }
    [Header("# Main Info")]
    public int itemID;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    public ItemType itemType;
    [Header("# Level Info")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;
    [Header("# Weapon Info")]
    public GameObject projectile;

}
