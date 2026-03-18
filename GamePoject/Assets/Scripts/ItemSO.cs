using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public string nameEng;
    
    public ItemType itemType;
    public int price;
    public int power;
    public int level;
    public bool isStackable;
    public Sprite icon;                 //실제 사용할 스프라이트 선언

    public override string ToString()
    {
        return $"[{id}] { itemName} ({ itemType}) - 가격 : {price} 골드, 속성 : {power}"; 
    }

    public string DisplayName
    {
        get {return string.IsNullOrEmpty(nameEng) ? itemName : nameEng;}
    }
}
