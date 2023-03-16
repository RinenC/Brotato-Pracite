using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public int weapon;
    public List<ItemList> tmplist = new List<ItemList>();
    public List<ItemList> samelevel = new List<ItemList>();
    void Update()
    {
        GUIManager.Instance.GetClickedItemData(ref weapon);
    }

    public void DestroyUI()
    {
        GUIManager.Instance.itemclickedbool = false;
        Destroy(this.gameObject);
    }

    public void UpgradeItem()
    {
        Debug.Log("Clicked");
        int idx = weapon;
        Debug.Log(idx);
        Debug.Log(weapon);
        tmplist = GUIManager.Instance.haveweaponstat.FindAll(x => x.name == GUIManager.Instance.haveweaponstat[idx].name);
        samelevel = tmplist.FindAll(x => x.lv == GUIManager.Instance.haveweaponstat[idx].lv);
        if (samelevel.Count > 1)
        {
            if (samelevel[0].lv < 4)
            {
                switch (GUIManager.Instance.haveweaponstat[idx].name)
                {
                    case "knife":
                        GUIManager.Instance.haveweaponstat[idx].lv += 1;
                        GUIManager.Instance.haveweaponstat[idx].meeledmg += 4;
                        GUIManager.Instance.haveweaponstat[idx].critchance += 0.25f;
                        break;
                    case "gun":
                        GUIManager.Instance.haveweaponstat[idx].lv += 1;
                        GUIManager.Instance.haveweaponstat[idx].rangedmg += 3;
                        GUIManager.Instance.haveweaponstat[idx].critchance += 0.2f;
                        break;
                    case "stick":
                        GUIManager.Instance.haveweaponstat[idx].lv += 1;
                        GUIManager.Instance.haveweaponstat[idx].eledmg += 3;
                        GUIManager.Instance.haveweaponstat[idx].critchance += 0.2f;
                        break;
                    case "branch":
                        GUIManager.Instance.haveweaponstat[idx].lv += 1;
                        GUIManager.Instance.haveweaponstat[idx].meeledmg += 4;
                        GUIManager.Instance.haveweaponstat[idx].critchance += 0.1f;
                        break;
                }
                SetWeaponBackColor(idx);
                int tmp = GUIManager.Instance.haveweaponstat.FindIndex(x => x == samelevel.Find(y => y != GUIManager.Instance.haveweaponstat[idx])); //tmplist[1] 이 haveweaponstat 의 몇번째 원소인지 확인
                GUIManager.Instance.haveweaponcount--;
                GUIManager.Instance.haveweaponstat.Remove(GUIManager.Instance.haveweaponstat[tmp]);
                var back = GUIManager.Instance.haveweapondisplayback[tmp];
                var display = GUIManager.Instance.haveweapondisplay[tmp];
                GUIManager.Instance.haveweapondisplayback.Remove(GUIManager.Instance.haveweapondisplayback[tmp]);
                GUIManager.Instance.haveweapondisplay.Remove(GUIManager.Instance.haveweapondisplay[tmp]);
                Destroy(back);
                Destroy(display);
                tmplist.Clear();
                samelevel.Clear();
                Debug.Log(GUIManager.Instance.haveweapondisplayback.Count);
                Debug.Log(GUIManager.Instance.haveweapondisplay.Count);
            }
        }
    }

    public void SetWeaponBackColor(int idx)
    {
        if (GUIManager.Instance.haveweaponstat[idx].lv == 4)
        {
            GUIManager.Instance.haveweapondisplayback[idx].GetComponent<Image>().color = new Color(255/255f, 60/255f, 50/255f);
        }
        else if (GUIManager.Instance.haveweaponstat[idx].lv == 3)
        {
            GUIManager.Instance.haveweapondisplayback[idx].GetComponent<Image>().color = new Color(185/255f, 60/255f, 255/255f);
        }
        else if (GUIManager.Instance.haveweaponstat[idx].lv == 2)
        {
            GUIManager.Instance.haveweapondisplayback[idx].GetComponent<Image>().color = new Color(70/255f, 175/255f, 255/255f);
        }
    }
}
