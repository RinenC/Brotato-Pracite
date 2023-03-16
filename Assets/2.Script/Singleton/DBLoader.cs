using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

[System.Serializable]
public class CharacrStatus
{
    public int idx;
    public string name;
    public float maxhp;
    public float hpreg;
    public float leech;
    public float dmgper;
    public float meeledmg;
    public float rangedmg;
    public float eledmg;
    public float atkspd;
    public float critchance;
    public float engineer;
    public float atkrange;
    public float armor;
    public float avoid;
    public float movespd;
    public float luck;
    public float income;
}

[System.Serializable]
public class EnemyStatus
{
    public int idx;
    public string name;
    public float maxhp;
    public float addhpPwave;
    public float movespd;
    public float dmg;
    public float adddmgPwave;
    public int killgold;
    public float regitemdropC;
    public float boxdropC;
    public int spawnwave;
}

[System.Serializable]
public class ItemList
{
    public int idx;
    public string type;
    public string attacktype;
    public int lv;
    public string name;
    public int price;
    public float maxhp;
    public float hpreg;
    public float leech;
    public float dmgper;
    public float meeledmg;
    public float rangedmg;
    public float eledmg;
    public float atkcool;
    public float critchance;
    public float engineer;
    public float atkrange;
    public float armor;
    public float avoid;
    public float movespd;
    public float luck;
    public float income;
}

public class DBLoader : MonoSingleton<DBLoader>
{
    static List<CharacrStatus> characterStatusList = new List<CharacrStatus>();
    static List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();
    static List<ItemList> itemTableList = new List<ItemList>();

    public Player player;

    [Header("스텟 스프라이트")]
    public Sprite maxHpSprite;
    public Sprite RegHpSprite;
    public Sprite LeechSprite;
    public Sprite DMGPERSprite;
    public Sprite MeeleDMGsprite;
    public Sprite RangeDMGSprite;
    public Sprite EleDMGSprite;
    public Sprite ATKSPDSprite;
    public Sprite CritChanSprite;
    public Sprite EngineerSprite;
    public Sprite ATKRangeSprite;
    public Sprite ArmorSprite;
    public Sprite AvoidSprite;
    public Sprite MoveSPDSprite;
    public Sprite LUCKSprite;
    public Sprite IncomeSprite;

    private const string jsonCharacterStatFilePath = "CharacterStatus";
    private const string jsonEnemyStatFilePath = "EnemyStatus";
    private const string jsonitemFilePath = "ItemTable";
    private const string jsonWeaponUpgradeFilePath = "WeaponUpgrade";


    void Awake()
    {
        var jsonCharacterStatFile = Resources.Load<TextAsset>("CharacterStatus");
        characterStatusList = JsonConvert.DeserializeObject<List<CharacrStatus>>(jsonCharacterStatFile.ToString());

        player.SetStatus(GetCharacterStatusByIdx(0));

        var jsonEnemyStatFile = Resources.Load<TextAsset>("EnemyStatus");
        enemyStatusList = JsonConvert.DeserializeObject<List<EnemyStatus>>(jsonEnemyStatFile.ToString());

        var jsonItemFile = Resources.Load<TextAsset>("ItemTable");
        itemTableList = JsonConvert.DeserializeObject<List<ItemList>>(jsonItemFile.ToString());

        //jsonutility는 list를 못쳐읽어옴

        //var list = new List<Status>();

        //var status = new Status();
        //status.name = "asdf";

        //list.Add(status);

        //Debug.Log(JsonUtility.ToJson(list, true));
    }

    public void CallDB()
    {
        Debug.Log("Call DB");
    }

    //Generic 공부
    static T DeepCopy<T>(T obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var tmp = JsonConvert.DeserializeObject<T>(json);

        return tmp;
    }

    //람다식 + Linq 공부할것
    public static CharacrStatus GetCharacterStatusByIdx(int idx)
    {
        return DeepCopy(characterStatusList.Find(x => x.idx == idx));
    }

    public ItemList GetItemListByIdx(int idx)
    {
        return DeepCopy(itemTableList.Find(x => x.idx == idx));
    }

    public EnemyStatus GetEnemyStatByIdx(int idx)
    {
        return DeepCopy(enemyStatusList.Find(x => x.idx == idx));
    }
    //Singleton 제작. (GameManager)
    //자료구조 공부할것

    public Sprite GetStatSprite(RewStat statType)
    {
        switch (statType)
        {
            case RewStat.Maxhp:
                return maxHpSprite;
            case RewStat.HpReg:
                return RegHpSprite;
            case RewStat.Leech:
                return LeechSprite;
            case RewStat.Dmgper:
                return DMGPERSprite;
            case RewStat.Meeledmg:
                return MeeleDMGsprite;
            case RewStat.Rangedmg:
                return RangeDMGSprite;
            case RewStat.Eledmg:
                return EleDMGSprite;
            case RewStat.Atkspd:
                return ATKSPDSprite;
            case RewStat.Critchance:
                return CritChanSprite;
            case RewStat.Engineer:
                return EngineerSprite;
            case RewStat.Atkrange:
                return ATKRangeSprite;
            case RewStat.Armor:
                return ArmorSprite;
            case RewStat.Avoid:
                return AvoidSprite;
            case RewStat.Movespd:
                return MoveSPDSprite;
            case RewStat.Luck:
                return LUCKSprite;
            case RewStat.Income:
                return IncomeSprite;
            default:
                return null;
        }
    }
}
