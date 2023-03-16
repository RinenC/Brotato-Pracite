using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    [SerializeField] public static ItemList weapon;

    public static GameObject prefabitemname;

    public void ClickEvent(int idx)
    {
        GUIManager.Instance.HaveItemInfo();
    }

    public static void CheckEqualItem(GameObject prefab)
    {
        if(prefab == GUIManager.Instance.haveweapondisplay[0])
        {
            weapon = GUIManager.Instance.haveweaponstat[0];
        }
        else if (prefab == GUIManager.Instance.haveweapondisplay[1])
        {
            weapon = GUIManager.Instance.haveweaponstat[1];
        }
        else if (prefab == GUIManager.Instance.haveweapondisplay[2])
        {
            weapon = GUIManager.Instance.haveweaponstat[2];
        }
        else if (prefab == GUIManager.Instance.haveweapondisplay[3])
        {
            weapon = GUIManager.Instance.haveweaponstat[3];
        }
        else if (prefab == GUIManager.Instance.haveweapondisplay[4])
        {
            weapon = GUIManager.Instance.haveweaponstat[4];
        }
        else if (prefab == GUIManager.Instance.haveweapondisplay[5])
        {
            weapon = GUIManager.Instance.haveweaponstat[5];
        }
    }
}
