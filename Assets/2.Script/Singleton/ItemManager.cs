using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public enum ItemTypeE { EXP, HP, MAX }
    public ItemTypeE curstate;

    public float drainDist = 0.2f;

    public Player player;

    public List<Sprite> expitemSprite;

    void Awake()
    {

    }

    void Update()
    {

    }

    public void SetItemType(ItemTypeE itemtype)
    {
        switch (itemtype)
        {
            case ItemTypeE.EXP:
                break;
            case ItemTypeE.HP:
                break;
            default:
                break;
        }
        curstate = itemtype;
    }

    public void ExpItemSprite(GameObject expitem)
    {
        var random = Random.Range(0, 3);
        expitem.GetComponent<SpriteRenderer>().sprite = expitemSprite[random];
    }

    public void GiveExp(float exp)
    {
        GUIManager.Instance.curExp += exp;
    }

    public void GiveHp(float hp)
    {
        GUIManager.Instance.curhp += hp;
    }

    public void GiveGold(int gold)
    {
        GUIManager.Instance.gold += gold;
    }
}
