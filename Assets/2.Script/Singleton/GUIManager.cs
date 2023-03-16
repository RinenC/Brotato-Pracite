using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum sceneE
{
    TITLE,
    PLAY,
    SHOP,
    GAMEOVER,
    CLEAR,
}

[DisallowMultipleComponent]
public class GUIManager : MonoSingleton<GUIManager>
{
    public GraphicRaycaster GR;
    PointerEventData ped;
    public GameObject UIObj;

    private void Awake()
    {
        Initialize();
        ped = new PointerEventData(null);
    }

    void Update()
    {
        GameManager.Instance.GameoverCheck();
        HPBarControll();
        ExpBarControll();
        GoldControll();
        WaveControll();
        GameOver();
        UITest();
        ped.position = Input.mousePosition;//마우스 위치의 이벤트 실행
        List<RaycastResult> rayResults = new List<RaycastResult>();
        GR.Raycast(ped, rayResults);
        if (Input.GetMouseButtonDown(0))
        {
            if (rayResults.Count > 0)
            {
                UIObj = rayResults[0].gameObject;
                weapondisplayslotprefab = UIObj;
            }
            else
            {
                UIObj = null;
            }
        }
    }
    public Player player;// { get; protected set; }

    public ItemList itemlist1;
    public ItemList itemlist2;
    public ItemList itemlist3;
    public ItemList itemlist4;

    public GameObject weaponPrefab;

    public List<GameObject> weaponPrefabList = new List<GameObject>();

    public GameObject waveendT;

    public List<GameObject> listGUIScenes;

    public RewardPopup rewardPopup;

    public sceneE curScene;

    #region GUIManagement
    public void Initialize()
    {
        SetGUIState(curScene);
        rewardPopup.gameObject.SetActive(false);
        //scenechangetime = GameManager.GetInstance().scenechangetime;
        //playerdeath = GameManager.GetInstance().playerdeath;
        Reroll();
        haveweaponcount++;
        if (haveweapondisplay.Count < haveweaponcount)
        {
            for (int i = haveweapondisplay.Count; i < haveweaponcount; i++)
            {
                weapondisplayslot.GetComponent<Image>().sprite = handgun;
                GameObject weapondisplayprefab = Instantiate(weapondisplay, haveweaponlayout.transform);
                weapondisplayslotprefab = Instantiate(weapondisplayslot, weapondisplayprefab.transform);
                haveweapondisplay.Add(weapondisplayslotprefab);
                haveweapondisplayback.Add(weapondisplayprefab);
                haveweaponstat.Add(DBLoader.Instance.GetItemListByIdx(2));
            }
        }
    }

    public void ShowGUIState(sceneE scene)
    {
        for (int i = (int)sceneE.TITLE; i < (EnumHelper.GetEnumMemberCnt(typeof(sceneE))); i++)
        {
            if (i == (int)scene)
                listGUIScenes[(int)i].SetActive(true);
            else
                listGUIScenes[(int)i].SetActive(false);
        }
    }

    public void SetGUIState(sceneE scene)
    {
        switch (scene)
        {
            case sceneE.TITLE:
                Time.timeScale = 0;
                break;
            case sceneE.PLAY:
                curwavetime = wavetime + 1;
                WeaponInstantiate();
                curhp = player.status.maxhp;
                Time.timeScale = 1;
                StartCoroutine(RandomSpawnManager.Instance.Spawn(wavetime, RandomSpawnManager.Instance.opossum));
                break;
            case sceneE.SHOP:
                RemoveWeaponPrefab();
                Time.timeScale = 0;
                ShopUI();
                Reroll();
                waveEnd = false;
                wave++;
                wavetime += 5;
                scenechangetime = 3f;
                curwavetime = wavetime + 1;
                RandomSpawnManager.Instance.count += 20;
                break;
            case sceneE.GAMEOVER:
                Time.timeScale = 0;
                GameManager.Instance.EventReset();
                break;
            case sceneE.CLEAR:
                Time.timeScale = 0;
                waveEnd = false;
                GameManager.Instance.EventReset();
                break;
        }
        ShowGUIState(scene);
        curScene = scene;
    }

    public void UpdateGUIState()
    {
        switch (curScene)
        {
            case sceneE.TITLE:
                break;
            case sceneE.PLAY:
                break;
            case sceneE.SHOP:
                break;
            case sceneE.GAMEOVER:
                break;
            case sceneE.CLEAR:
                break;
        }
    }

    public void EventSenceChange(int idx)
    {
        SetGUIState((sceneE)idx);
    }

    public void WeaponInstantiate()
    {
        for (int i = 0; i < haveweaponstat.Count; i++)
        {
            var _weaponprefab = weaponPrefab;
            GameObject a = Instantiate(_weaponprefab, player.transform);
            a.GetComponent<Weapon>().idx = i;
            a.GetComponent<Weapon>().player = player;
            weaponPrefabList.Add(a);
        }
    }

    public void RemoveWeaponPrefab()
    {
        for (int i = 0; i < weaponPrefabList.Count; i++)
        {
            Destroy(weaponPrefabList[i]);
        }
        weaponPrefabList.Clear();
    }
    #endregion

    [SerializeField]
    public float maxExp = 100;
    public float curExp = 0;
    public float curhp;
    public int gold = 0;
    public int lv = 1;
    public int wave = 1;
    public float wavetime = 20f;
    public float curwavetime;
    public float scenechangetime = 3f;
    public int rewardcount = 0;
    public int resetgold = 2;
    public int resettime = 0;

    bool testStart;
    public bool waveEnd;
    public bool playerdeath;
    public bool isReward;
    public bool lock1;
    public bool lock2;
    public bool lock3;
    public bool lock4;

    public Camera camera;

    public Slider hpBar;
    public Slider expBar;
    public Slider headHpBar;

    public Text hpText;
    public Text level;
    public Text goldT;
    public Text waveT;
    public Text timer;
    public Text shopRound;
    public Text shopGold;
    public Text shopResetGold;
    public Text damagedText;

    public GameObject lock1s;
    public GameObject lock2s;
    public GameObject lock3s;
    public GameObject lock4s;

    #region WaveControl
    public void HPBarControll()
    {
        hpBar.value = curhp / (float)player.status.maxhp;
        hpText.text = curhp + " / " + player.status.maxhp;
        headHpBar.value = curhp / (float)player.status.maxhp;
        headHpBar.transform.position = camera.WorldToScreenPoint(player.transform.position) + new Vector3(0, 35, 0);
    }

    public void ExpBarControll()
    {
        expBar.value = curExp / maxExp;

        if (curExp >= maxExp)
        {
            float addexp = curExp - maxExp;
            curExp = 0;
            rewardcount++;
            lv++;
            maxExp = maxExp + 50;
            curExp = addexp;
        }

        level.text = "LV . " + lv;
    }

    public void GoldControll()
    {
        goldT.text = "" + gold;
    }

    public void WaveControll()
    {
        waveT.text = "웨이브 " + wave;
        if (curwavetime > 6)
        {
            timer.color = Color.white;
            timer.text = "" + (int)curwavetime;
        }
        else if (curwavetime <= 6)
        {
            timer.color = Color.red;
            timer.text = "" + (int)curwavetime;
        }
        if (waveEnd == false)
        {
            waveendT.gameObject.SetActive(false);
            if (playerdeath == false)
                curwavetime -= Time.deltaTime;
            else if (playerdeath == true)
            {
                RandomSpawnManager.Instance.Delete();
                player.targetList.Clear();
            }
        }
        if (curwavetime <= 0)
        {
            waveEnd = true;
            if (wave < 20)
            {
                if (waveEnd == true)
                {
                    waveendT.SetActive(true);
                    RandomSpawnManager.Instance.Delete();
                    player.targetList.Clear();
                    if (scenechangetime > 0)
                    {
                        scenechangetime -= Time.deltaTime;
                    }
                    if (scenechangetime <= 0)
                    {
                        if (rewardcount > 0)
                        {
                            Time.timeScale = 0;
                            if (!isReward)
                            {
                                rewardPopup.gameObject.SetActive(true);
                                rewardPopup.Init();
                                isReward = true;
                            }
                        }
                        else if (rewardcount <= 0)
                        {
                            waveEnd = false;
                            SetGUIState(sceneE.SHOP);
                        }
                    }
                }
            }
            else if (wave >= 20)
            {
                if (waveEnd == true)
                {
                    waveendT.SetActive(true);
                    RandomSpawnManager.Instance.Delete();
                    player.targetList.Clear();
                    if (scenechangetime > 0)
                    {
                        scenechangetime -= Time.deltaTime;
                    }
                    if (scenechangetime <= 0)
                    {
                        SetGUIState(sceneE.CLEAR);
                    }
                }
            }
        }
    }

    public void RewardButtonClick()
    {
        isReward = false;
    }

    public void rewardselect()
    {
        if (rewardcount > 0)
            rewardcount--;
        else if (rewardcount <= 0)
            SetGUIState(sceneE.SHOP);
    }

    public void GameOver()
    {
        if (playerdeath == false) { }
        else if (playerdeath == true)
        {
            if (scenechangetime > 0)
            {
                scenechangetime -= Time.deltaTime;
            }
            else if (scenechangetime <= 0)
            {
                SetGUIState(sceneE.GAMEOVER);
            }
        }
    }

    public void UITest()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (testStart == false)
            {
                testStart = true;
                Debug.Log("UI Test Mode Started.");
            }
            else if (testStart == true)
            {
                testStart = false;
                Debug.Log("UI Test Mode Ended.");
            }
        }

        if (testStart == true)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (curhp > 0)
                {
                    curhp = curhp - 1;
                    Debug.Log("HP UI TEST");
                }
                else if (curhp <= 0)
                {
                    Debug.Log("Low HP");
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                curExp = curExp + 50;
                Debug.Log("EXP UI TEST");
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                gold = gold + 10;
                Debug.Log("Gold UI TEST");
            }
        }
    }

    #region 공사중
    // 데미지 계산 및 데미지 받았을 때 텍스트 호출 코드
    // 변경사항 1-1 수정 후 진행 예정
    //public void DamagedText()
    //{
    //    if (GameManager.Instance.attacked == true)
    //    {
    //        damagedText.text = "" + player.lastDamage;
    //    }
    //}

    public void Damaged()
    {
        if (GameManager.Instance.attacked == true)
        {

        }
    }
    #endregion

    #endregion

    #region ShopItemData



    #endregion

    #region Shop

    public List<GameObject> haveitemdisplay = new List<GameObject>();
    public List<GameObject> haveitemdisplayback = new List<GameObject>();
    public List<ItemList> haveitemstat = new List<ItemList>();
    public List<GameObject> haveweapondisplay = new List<GameObject>();
    public List<GameObject> haveweapondisplayback = new List<GameObject>();
    public List<ItemList> haveweaponstat = new List<ItemList>();
    public int haveitemcount = 0;
    public int haveweaponcount = 0;

    public GameObject haveitemlayout;
    public GameObject itemdisplay;
    public GameObject itemdisplayslot;

    public GameObject haveweaponlayout;
    public GameObject weapondisplay;
    public GameObject weapondisplayslot;

    public bool itemclickedbool;

    public GameObject prefab;
    public GameObject weapondisplayslotprefab;

    public GameObject clickedItemInfo;
    public GameObject shopBack;
    public GameObject upgradeButton;

    [Header("ShopItemList")]
    public Sprite knife;
    public Sprite handgun;
    public Sprite stick;
    public Sprite branch;
    public Sprite shield;
    public Sprite claw;
    public Sprite magicball;
    public Sprite goldenbug;

    public GameObject item1sp;
    public GameObject item2sp;
    public GameObject item3sp;
    public GameObject item4sp;

    public GameObject item1Name;
    public GameObject item2Name;
    public GameObject item3Name;
    public GameObject item4Name;

    public GameObject item1Info;
    public GameObject item2Info;
    public GameObject item3Info;
    public GameObject item4Info;

    public GameObject item1price;
    public GameObject item2price;
    public GameObject item3price;
    public GameObject item4price;

    public GameObject shopSlot1;
    public GameObject shopSlot2;
    public GameObject shopSlot3;
    public GameObject shopSlot4;

    public void GetItemDatabase1(ItemList _itemlist)
    {
        if (!lock1)
        {
            itemlist1 = _itemlist;
            Debug.Log(itemlist1.name);
        }
    }
    public void GetItemDatabase2(ItemList _itemlist)
    {
        if (!lock2)
        {
            itemlist2 = _itemlist;
            Debug.Log(itemlist2.name);
        }
    }
    public void GetItemDatabase3(ItemList _itemlist)
    {
        if (!lock3)
        {
            itemlist3 = _itemlist;
            Debug.Log(itemlist3.name);
        }
    }
    public void GetItemDatabase4(ItemList _itemlist)
    {
        if (!lock4)
        {
            itemlist4 = _itemlist;
            Debug.Log(itemlist4.name);
        }
    }

    public void LockItem(int idx)
    {
        LockChange(idx);
    }

    public void LockChange(int idx)
    {
        switch (idx)
        {
            case 1:
                if (lock1 == false)
                {
                    lock1 = true;
                    lock1s.SetActive(true);
                }
                else if (lock1 == true)
                {
                    lock1 = false;
                    lock1s.SetActive(false);
                }
                break;
            case 2:
                if (lock2 == false)
                {
                    lock2 = true;
                    lock2s.SetActive(true);
                }
                else if (lock2 == true)
                {
                    lock2 = false;
                    lock2s.SetActive(false);
                }
                break;
            case 3:
                if (lock3 == false)
                {
                    lock3 = true;
                    lock3s.SetActive(true);
                }
                else if (lock3 == true)
                {
                    lock3 = false;
                    lock3s.SetActive(false);
                }
                break;
            case 4:
                if (lock4 == false)
                {
                    lock4 = true;
                    lock4s.SetActive(true);
                }
                else if (lock4 == true)
                {
                    lock4 = false;
                    lock4s.SetActive(false);
                }
                break;
        }
    }

    public int ItemTypeChoose()
    {
        int a = RandomNumberGenerator.Instance.RNGCount(2); // 타입 설정용 랜덤. 1은 Weapon, 2는 Item을 출력한다.
        int b;

        if (a == 1)
        {
            return b = RandomNumberGenerator.Instance.RNGCount(4);
        }
        if (a == 2)
        {
            return b = RandomNumberGenerator.Instance.RNGCount(4) + 4; ;
        }

        return 0;
    }



    public void ShopUI()
    {
        shopRound.text = "상점 (웨이브 " + wave + ")";
        shopGold.text = "" + gold;
        shopResetGold.text = "초기화" + "   " + resetgold;

        if (gold < resetgold)
        {
            shopResetGold.color = Color.red;
        }
        if (gold >= resetgold)
        {
            shopResetGold.color = Color.white;
        }
    }

    public void OnResetButtonClick()
    {
        if (lock1 && lock2 && lock3 && lock4)
        {

        }
        else
        {
            if (gold >= resetgold && gold > 0)
            {
                gold -= resetgold;
                resetgold += (wave - 1) + resettime;
                Reroll();
            }
        }
        ShopUI();
    }

    public void Reroll()
    {
        shopSlot1.SetActive(true);
        shopSlot2.SetActive(true);
        shopSlot3.SetActive(true);
        shopSlot4.SetActive(true);
        if (!lock1)
        {
            GetItemDatabase1(DBLoader.Instance.GetItemListByIdx(ItemTypeChoose())); //DB에서 랜덤아이템 데이터 로드
            SetItemSprite(item1sp, itemlist1); //아이템 스프라이트 로딩
            item1Name.GetComponent<Text>().text = itemlist1.name; //아이템 이름 로딩
            SetItemInfo(itemlist1, item1Info); //아이템 인포 로딩
            SetItemPrice(itemlist1, item1price); //아이템 가격 로딩
        }
        if (!lock2)
        {
            GetItemDatabase2(DBLoader.Instance.GetItemListByIdx(ItemTypeChoose()));
            SetItemSprite(item2sp, itemlist2);
            item2Name.GetComponent<Text>().text = itemlist2.name;
            SetItemInfo(itemlist2, item2Info);
            SetItemPrice(itemlist2, item2price);
        }
        if (!lock3)
        {
            GetItemDatabase3(DBLoader.Instance.GetItemListByIdx(ItemTypeChoose()));
            SetItemSprite(item3sp, itemlist3);
            item3Name.GetComponent<Text>().text = itemlist3.name;
            SetItemInfo(itemlist3, item3Info);
            SetItemPrice(itemlist3, item3price);
        }
        if (!lock4)
        {
            GetItemDatabase4(DBLoader.Instance.GetItemListByIdx(ItemTypeChoose()));
            SetItemSprite(item4sp, itemlist4);
            item4Name.GetComponent<Text>().text = itemlist4.name;
            SetItemInfo(itemlist4, item4Info);
            SetItemPrice(itemlist4, item4price);
        }
    }

    public void SetItemSprite(GameObject a, ItemList itemList) //아이템 스프라이트 로딩
    {
        Debug.Log(itemList.idx);
        switch (itemList.idx)
        {
            case 1:
                a.GetComponent<Image>().sprite = knife;
                break;
            case 2:
                a.GetComponent<Image>().sprite = handgun;
                break;
            case 3:
                a.GetComponent<Image>().sprite = stick;
                break;
            case 4:
                a.GetComponent<Image>().sprite = branch;
                break;
            case 5:
                a.GetComponent<Image>().sprite = shield;
                break;
            case 6:
                a.GetComponent<Image>().sprite = claw;
                break;
            case 7:
                a.GetComponent<Image>().sprite = magicball;
                break;
            case 8:
                a.GetComponent<Image>().sprite = goldenbug;
                break;
        }
        Debug.Log(itemList.idx);
    }

    public void SetItemInfo(ItemList itemList, GameObject iteminfo) //아이템 인포 로딩
    {
        // 하나의 스트링에 필요한 값들을 삼항연산자를 통해 조건식을 걸고 한번에 집어넣을 수 있다.
        string status = "타입 : " + itemList.type + '\n' +
                        (itemList.maxhp != 0 ? "체력 + " + itemList.maxhp + '\n' : "") +
                        (itemList.hpreg != 0 ? "체력재생 + " + itemList.hpreg + '\n' : "") +
                        (itemList.leech != 0 ? "흡수 + " + itemList.leech + '\n' : "") +
                        (itemList.dmgper != 0 ? "데미지 + " + itemList.dmgper + "%" + '\n' : "") +
                        (itemList.meeledmg != 0 ? "근접데미지 + " + itemList.meeledmg + '\n' : "") +
                        (itemList.rangedmg != 0 ? "원거리데미지 + " + itemList.rangedmg + '\n' : "") +
                        (itemList.eledmg != 0 ? "원소데미지 + " + itemList.eledmg + '\n' : "") +
                        (itemList.atkcool != 0 ? "공격속도 + " + itemList.atkcool + "%" + '\n' : "") +
                        (itemList.critchance != 0 ? "치명타 확률 + " + itemList.critchance + '\n' : "") +
                        (itemList.engineer != 0 ? "엔지니어링 + " + itemList.engineer + '\n' : "") +
                        (itemList.atkrange != 0 ? "범위 + " + itemList.atkrange + '\n' : "") +
                        (itemList.armor != 0 ? "방어구 + " + itemList.armor + '\n' : "") +
                        (itemList.avoid != 0 ? "회피 + " + itemList.avoid + "%" + '\n' : "") +
                        (itemList.movespd != 0 ? "이동속도 + " + itemList.movespd + "%" + '\n' : "") +
                        (itemList.luck != 0 ? "행운 + " + itemList.luck + '\n' : "") +
                        (itemList.income != 0 ? "수확 + " + itemList.income : "");


        iteminfo.GetComponent<Text>().text = status;
    }

    public void SetItemPrice(ItemList itemList, GameObject itemprice)
    {
        itemprice.GetComponent<Text>().text = "" + itemList.price;
    }

    public void ItemPurchase(int idx) //아이템 구매
    {
        switch (idx)
        {
            case 1:
                if (gold >= itemlist1.price)
                {
                    if (itemlist1.type == "weapon")
                    {
                        if (haveweaponcount < 6)
                        {
                            gold -= itemlist1.price;
                            shopSlot1.SetActive(false);
                            HaveItemPulling(itemlist1);
                            GetItemStatus(itemlist1);
                        }
                    }
                    else
                    {
                        gold -= itemlist1.price;
                        shopSlot1.SetActive(false);
                        HaveItemPulling(itemlist1);
                        GetItemStatus(itemlist1);
                    }
                }
                lock1 = false;
                break;
            case 2:
                if (gold >= itemlist2.price)
                {
                    if (itemlist2.type == "weapon")
                    {
                        if (haveweaponcount < 6)
                        {
                            gold -= itemlist2.price;
                            shopSlot2.SetActive(false);
                            HaveItemPulling(itemlist2);
                            GetItemStatus(itemlist2);
                        }
                    }
                    else
                    {
                        gold -= itemlist2.price;
                        shopSlot2.SetActive(false);
                        HaveItemPulling(itemlist2);
                        GetItemStatus(itemlist2);
                    }
                }
                lock2 = false;
                break;
            case 3:
                if (gold >= itemlist3.price)
                {
                    if (itemlist3.type == "weapon")
                    {
                        if (haveweaponcount < 6)
                        {
                            gold -= itemlist3.price;
                            shopSlot3.SetActive(false);
                            HaveItemPulling(itemlist3);
                            GetItemStatus(itemlist3);
                        }
                    }
                    else
                    {
                        gold -= itemlist3.price;
                        shopSlot3.SetActive(false);
                        HaveItemPulling(itemlist3);
                        GetItemStatus(itemlist3);
                    }
                }
                lock3 = false;
                break;
            case 4:
                if (gold >= itemlist4.price)
                {
                    if (itemlist4.type == "weapon")
                    {
                        if (haveweaponcount < 6)
                        {
                            gold -= itemlist4.price;
                            shopSlot4.SetActive(false);
                            HaveItemPulling(itemlist4);
                            GetItemStatus(itemlist4);
                        }
                    }
                    else
                    {
                        gold -= itemlist4.price;
                        shopSlot4.SetActive(false);
                        HaveItemPulling(itemlist4);
                        GetItemStatus(itemlist4);
                    }
                }
                lock4 = false;
                break;
        }
        ItemInvLength();
        ShopUI();
    }

    public void HaveItemPulling(ItemList itemList)
    {
        if (itemList.type == "item")
        {
            haveitemcount++;
            if (haveitemdisplay.Count < haveitemcount)
            {
                Debug.Log(haveitemcount);

                for (int i = haveitemdisplay.Count; i < haveitemcount; i++)
                {
                    SetItemSprite(itemdisplayslot, itemList);
                    GameObject itemdisplayprefab = Instantiate(itemdisplay, haveitemlayout.transform);
                    GameObject itemdisplayslotprefab = Instantiate(itemdisplayslot, itemdisplayprefab.transform);
                    haveitemstat.Add(itemList);
                    haveitemdisplayback.Add(itemdisplayprefab);
                    haveitemdisplay.Add(itemdisplayslotprefab);
                    Debug.Log(haveitemdisplay.Count);
                }
            }
        }

        if (itemList.type == "weapon")
        {
            haveweaponcount++;
            if (haveweapondisplay.Count < haveweaponcount)
            {
                for (int i = haveweapondisplay.Count; i < haveweaponcount; i++)
                {
                    SetItemSprite(weapondisplayslot, itemList);
                    GameObject weapondisplayprefab = Instantiate(weapondisplay, haveweaponlayout.transform);
                    weapondisplayslotprefab = Instantiate(weapondisplayslot, weapondisplayprefab.transform);
                    haveweaponstat.Add(itemList);
                    haveweapondisplayback.Add(weapondisplayprefab);
                    haveweapondisplay.Add(weapondisplayslotprefab);
                    Debug.Log(haveweapondisplay.Count);
                }
            }
        }
    }

    public void GetItemStatus(ItemList itemList) //구매한 아이템의 스테이터스를 플레이어의 스테이터스에 더한다.
    {
        if (itemList.type == "item")
        {
            if (itemList.maxhp != 0)
                player.status.maxhp += itemList.maxhp;
            if (itemList.hpreg != 0)
                player.status.hpreg += itemList.hpreg;
            if (itemList.leech != 0)
                player.status.leech += itemList.leech;
            if (itemList.dmgper != 0)
                player.status.dmgper += itemList.dmgper;
            if (itemList.meeledmg != 0)
                player.status.meeledmg += itemList.meeledmg;
            if (itemList.rangedmg != 0)
                player.status.rangedmg += itemList.rangedmg;
            if (itemList.eledmg != 0)
                player.status.eledmg += itemList.eledmg;
            if (itemList.atkcool != 0)
                player.status.atkspd += itemList.atkcool;
            if (itemList.critchance != 0)
                player.status.critchance += itemList.critchance;
            if (itemList.engineer != 0)
                player.status.engineer += itemList.engineer;
            if (itemList.atkrange != 0)
                player.status.atkrange += itemList.atkrange;
            if (itemList.atkrange != 0)
                player.status.atkrange += itemList.atkrange;
            if (itemList.armor != 0)
                player.status.armor += itemList.armor;
            if (itemList.avoid != 0)
                player.status.avoid += itemList.avoid;
            if (itemList.movespd != 0)
                player.status.movespd += itemList.movespd;
            if (itemList.luck != 0)
                player.status.luck += itemList.luck;
            if (itemList.income != 0)
                player.status.income += itemList.income;
        }
    }

    public void ItemInvLength()
    {
        if (haveitemdisplay.Count == 41)
        {
            haveitemlayout.GetComponent<RectTransform>().sizeDelta += new Vector2(0.0f, 60.0f);
        }
        else if (haveitemdisplay.Count == 33)
        {
            haveitemlayout.GetComponent<RectTransform>().sizeDelta += new Vector2(0.0f, 60.0f);
        }
        else if (haveitemdisplay.Count == 25)
        {
            haveitemlayout.GetComponent<RectTransform>().sizeDelta += new Vector2(0.0f, 60.0f);
        }
        else if (haveitemdisplay.Count == 17)
        {
            haveitemlayout.GetComponent<RectTransform>().sizeDelta += new Vector2(0.0f, 40.0f);
        }
    }

    public void HaveItemInfo()
    {
        if (!prefab)
        {
            itemclickedbool = true;
            Vector3 UIMBPoint;
            Vector3 var = new Vector3(0, 0, 0);
            UIMBPoint = new Vector3((Camera.main.ScreenToViewportPoint(Input.mousePosition).x * Screen.width) - (Screen.width / 2),
                (Camera.main.ScreenToViewportPoint(Input.mousePosition).y * Screen.height) - (Screen.height / 2), 0);
            prefab = Instantiate(clickedItemInfo);
            prefab.transform.parent = shopBack.transform;
            prefab.transform.localPosition = UIMBPoint + (new Vector3(-Screen.width / 9, Screen.height / 3.5f, 0) / 2);
            prefab.transform.rotation = Quaternion.identity;
            ItemClick.prefabitemname = Instantiate(Resources.Load("itemname")) as GameObject;
            ItemClick.prefabitemname.transform.parent = prefab.transform;
            ItemClick.prefabitemname.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, -10, 0);
            ItemClick.CheckEqualItem(weapondisplayslotprefab);
            ItemClick.prefabitemname.GetComponent<Text>().text = "" + ItemClick.weapon.name;
        }
    }

    public void GetClickedItemData(ref int weaponidx)
    {
        for(int i = 0; i < haveweapondisplay.Count; i++)
        if (weapondisplayslotprefab == haveweapondisplay[i])
        {
            weaponidx = i;
        }
    }
    #endregion
}