using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPopup : MonoBehaviour
{
    public List<RewardSlot> rewardSlotList = new List<RewardSlot>();

    public void Init()
    {
        var randList = RandomInts(rewardSlotList.Count, EnumHelper.GetEnumMemberCnt(typeof(RewStat)));

        for (int i = 0; i < rewardSlotList.Count; i++)
        {
            var data = new RewardSlotData();
            data.rewStat = (RewStat)randList[i];
            data.value = 6;

            rewardSlotList[i].InitSlot(data);
        }
    }

    List<int> RandomInts(int listCnt, int rndMaxValue)
    {
        List<int> returnList = new List<int>();

        for (int i = 0; i < listCnt; i++)
        {
            var addInt = Random.Range(0, rndMaxValue);

            if (!returnList.Contains(addInt))
            {
                returnList.Add(addInt);
            }
            else
                --i;
        }

        //foreach (var a in returnList)
        //{
        //    Debug.Log(a);
        //}

        return returnList;
    }
}
