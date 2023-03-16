using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSlotData
{
    public RewStat rewStat;
    public int value;
}

public class RewardSlot : MonoBehaviour
{
    [SerializeField] Image itemImg;
    [SerializeField] Text itemNameText;
    [SerializeField] Button positiveBtn;

    public RewardSlotData data { get; private set; }

    private void Awake()
    {
        positiveBtn.onClick.AddListener(GetReward);
        positiveBtn.onClick.AddListener(GUIManager.Instance.RewardButtonClick);
    }

    public void InitSlot(RewardSlotData _data)
    {
        data = _data;

        if (data == null)
            return;
        var sprite = DBLoader.Instance;
        //< string.format 공부할것
        switch (data.rewStat)
        {
            case RewStat.Maxhp:
                itemImg.sprite = sprite.maxHpSprite;
                itemNameText.text = $"최대 체력 +{data.value:N0}";
                break;
            case RewStat.HpReg:
                itemImg.sprite = sprite.RegHpSprite;
                itemNameText.text = $"체력 재생 +{data.value:N0}";
                break;
            case RewStat.Leech:
                itemImg.sprite = sprite.LeechSprite;
                itemNameText.text = $"흡혈 +{data.value:N0}%";
                break;
            case RewStat.Dmgper:
                itemImg.sprite = sprite.DMGPERSprite;
                itemNameText.text = $"데미지 +{data.value:N0}%";
                break;
            case RewStat.Meeledmg:
                itemImg.sprite = sprite.MeeleDMGsprite;
                itemNameText.text = $"근거리데미지 +{data.value:N0}%";
                break;
            case RewStat.Rangedmg:
                itemImg.sprite = sprite.RangeDMGSprite;
                itemNameText.text = $"원거리데미지 +{data.value:N0}%";
                break;
            case RewStat.Eledmg:
                itemImg.sprite = sprite.EleDMGSprite;
                itemNameText.text = $"원소데미지 +{data.value:N0}%";
                break;
            case RewStat.Atkspd:
                itemImg.sprite = sprite.ATKSPDSprite;
                itemNameText.text = $"공격속도 +{data.value:N0}%";
                break;
            case RewStat.Critchance:
                itemImg.sprite = sprite.CritChanSprite;
                itemNameText.text = $"치명타 확률 +{data.value:N0}%";
                break;
            case RewStat.Engineer:
                itemImg.sprite = sprite.EngineerSprite;
                itemNameText.text = $"엔지니어링 +{data.value:N0}%";
                break;
            case RewStat.Atkrange:
                itemImg.sprite = sprite.ATKRangeSprite;
                itemNameText.text = $"공격범위 +{data.value:N0}%";
                break;
            case RewStat.Armor:
                itemImg.sprite = sprite.ArmorSprite;
                itemNameText.text = $"방어력 +{data.value:N0}%";
                break;
            case RewStat.Avoid:
                itemImg.sprite = sprite.AvoidSprite;
                itemNameText.text = $"회피 +{data.value:N0}%";
                break;
            case RewStat.Movespd:
                itemImg.sprite = sprite.MoveSPDSprite;
                itemNameText.text = $"이동속도 +{data.value:N0}%";
                break;
            case RewStat.Luck:
                itemImg.sprite = sprite.LUCKSprite;
                itemNameText.text = $"행운 +{data.value:N0}%";
                break;
            case RewStat.Income:
                itemImg.sprite = sprite.IncomeSprite;
                itemNameText.text = $"수확 +{data.value:N0}%";
                break;
        }
        itemImg.sprite = DBLoader.Instance.GetStatSprite(data.rewStat);
    }

    public void GetReward()
    {
        var player = GameManager.Instance.player;

        switch (data.rewStat)
        {
            case RewStat.Maxhp:
                player.status.maxhp += data.value;
                GUIManager.Instance.curhp += data.value;
                break;
            case RewStat.HpReg:
                player.status.hpreg += data.value;
                break;
            case RewStat.Leech:
                player.status.leech += data.value;
                break;
            case RewStat.Dmgper:
                player.status.dmgper += data.value;
                break;
            case RewStat.Meeledmg:
                player.status.meeledmg += data.value;
                break;
            case RewStat.Rangedmg:
                player.status.rangedmg += data.value;
                break;
            case RewStat.Eledmg:
                player.status.eledmg += data.value;
                break;
            case RewStat.Atkspd:
                player.status.atkspd += data.value;
                break;
            case RewStat.Critchance:
                player.status.critchance += data.value;
                break;
            case RewStat.Engineer:
                player.status.engineer += data.value;
                break;
            case RewStat.Atkrange:
                player.status.atkrange += data.value;
                break;
            case RewStat.Armor:
                player.status.armor += data.value;
                break;
            case RewStat.Avoid:
                player.status.avoid += data.value;
                break;
            case RewStat.Movespd:
                player.status.movespd += data.value;
                break;
            case RewStat.Luck:
                player.status.luck += data.value;
                break;
            case RewStat.Income:
                player.status.income += data.value;
                break;
        }

        GUIManager.Instance.rewardcount--;
        if (GUIManager.Instance.rewardcount <= 0)
        {
            GUIManager.Instance.rewardPopup.gameObject.SetActive(false);
            GUIManager.Instance.SetGUIState(sceneE.SHOP);
        }
        else
            GUIManager.Instance.rewardPopup.Init();
    }
}
