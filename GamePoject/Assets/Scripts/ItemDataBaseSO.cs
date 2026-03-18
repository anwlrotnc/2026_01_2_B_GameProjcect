using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBaseSO", menuName = "Inventory/ItemDataBase")]
public class ItemDataBaseSO : ScriptableObject
{
    public List<ItemSO> items = new List<ItemSO>();

    //캐싱을 위한 Dictionary
    private Dictionary<int, ItemSO> itemsByld;                              //ID로 아이템 찾기 위한 캐싱
    private Dictionary<string, ItemSO> itemsByName;                         //이름으로 아이템 찾기

    public void Initialize()
    {
        itemsByld = new Dictionary<int, ItemSO>();                            //위에 선언만 했기 때문에 Dictionary 할당
        itemsByName = new Dictionary<string, ItemSO>();

        foreach(var item in items)
        {
            itemsByld[item.id] = item;
            itemsByName[item.itemName] = item;
        }
    }

    //ID로 아이템 찾기

    public ItemSO GetItemByID(int id)                                       
    {
        if(itemsByld == null)
        {
            Initialize();                                                   //캐싱이 되어있는지 확인하고 아니면 초기화
        }

        if (itemsByld.TryGetValue(id, out ItemSO item))                     //id 값을 찾아서 ItemSO를 리턴한다
            return item;

        return null;                                                        //없을 경우 NULL
    }

    //타입으로 아이템 필터링
    public List<ItemSO> GetItemByType(ItemType type)
    {
        return items.FindAll(item => item.itemType == type);
    }
}
