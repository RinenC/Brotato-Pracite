using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public Image rewardImage;
    public Text rewardText;
    public Button rewardButton;

    public enum RewNum
    {
        Rew1,
        Rew2,
        Rew3,
        Rew4,
    }

    public RewNum curRewNum;



    public RewStat curStat;

    void Start()
    {
        curStat = (RewStat)(0);
        RewardStatus();
    }

    void Update()
    {

    }

    public void RewardStatus()
    {
        switch (curRewNum)
        {
            case RewNum.Rew1:
                SetResStat((RewStat)(RewardManager.Instance.randomList[0]));
                break;
            case RewNum.Rew2:
                SetResStat((RewStat)(RewardManager.Instance.randomList[1]));
                break;
            case RewNum.Rew3:
                SetResStat((RewStat)(RewardManager.Instance.randomList[2]));
                break;
            case RewNum.Rew4:
                SetResStat((RewStat)(RewardManager.Instance.randomList[3]));
                break;
        }
    }
    public void SetResStat(RewStat stat)
    {
        switch (stat)
        {
            case RewStat.Maxhp:
                break;
            case RewStat.HpReg:
                break;
            case RewStat.Leech:
                break;
            case RewStat.Dmgper:
                break;
            case RewStat.Meeledmg:
                break;
            case RewStat.Rangedmg:
                break;
            case RewStat.Eledmg:
                break;
            case RewStat.Atkspd:
                break;
            case RewStat.Critchance:
                break;
            case RewStat.Engineer:
                break;
            case RewStat.Atkrange:
                break;
            case RewStat.Armor:
                break;
            case RewStat.Avoid:
                break;
            case RewStat.Movespd:
                break;
            case RewStat.Luck:
                break;
            case RewStat.Income:
                break;
        }
        curStat = stat;
    }

    
}