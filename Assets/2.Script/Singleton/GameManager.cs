using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Player player;
    public bool attacked;

    public void GameoverCheck()
    {
        if (GUIManager.Instance.curhp > 0)
            GUIManager.Instance.playerdeath = false;
        else if (GUIManager.Instance.curhp <= 0)
            GUIManager.Instance.playerdeath = true;

        if (GUIManager.Instance.playerdeath == true)
            RandomSpawnManager.Instance.Delete();
    }

    public void EventExit()
    {
        Application.Quit();
    }

    public void EventReset()
    {
        GUIManager.Instance.wave = 1;
        GUIManager.Instance.gold = 0;
        GUIManager.Instance.maxExp = 100;
        GUIManager.Instance.curExp = 0;
        GUIManager.Instance.lv = 1;
        GUIManager.Instance.wavetime = 20f;
        GUIManager.Instance.scenechangetime = 3f;
        GUIManager.Instance.rewardcount = 0;
        GUIManager.Instance.curhp = player.status.maxhp;
        GUIManager.Instance.scenechangetime = 3f;
        GUIManager.Instance.curwavetime = GUIManager.Instance.wavetime;
        RandomSpawnManager.Instance.Delete();
    }

    public List<int> RandomInts(int returnMax, int max)
    {
        List<int> returnList = new List<int>();
        int addInt = 0;
        while (returnList.Count <= returnMax)
        {
            addInt = Random.Range(0, max);
            if (!returnList.Exists(x => x == addInt))
            {
                returnList.Add(addInt);
            }
        }
        foreach (var a in returnList)
        {
            Debug.Log(a);
        }
        return returnList;
    }

    public void DamageCalProcess(Opossum Enemy, Player player, ItemList weapon)
    {
        switch (weapon.attacktype)
        {
            case "meele":
                Enemy.curhp -= (player.status.meeledmg + weapon.meeledmg) * (1 + (player.status.dmgper / 100));
                break;
            case "range":
                Enemy.curhp -= (player.status.rangedmg + weapon.rangedmg) * (1 + (player.status.dmgper / 100));
                break;
            case "item":
                break;
        }
    }
}