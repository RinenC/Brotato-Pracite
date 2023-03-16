using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public CharacrStatus status;// { get; protected set; }
    public Sprite live;
    public Sprite death;

    [SerializeField] public List<GameObject> targetList = new List<GameObject>();

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;
    public GameObject target6;

    public float attackaftertime1;
    public float attackaftertime2;
    public float attackaftertime3;
    public float attackaftertime4;
    public float attackaftertime5;
    public float attackaftertime6;

    public float attackcool1;
    public float attackcool2;
    public float attackcool3;
    public float attackcool4;
    public float attackcool5;
    public float attackcool6;

    public bool canattack1;
    public bool canattack2;
    public bool canattack3;
    public bool canattack4;
    public bool canattack5;
    public bool canattack6;

    public GameObject bullet;

    public bool playerdeath;

    void Awake()
    {
        GUIManager.Instance.curhp = status.maxhp;
    }

    void Update()
    {
        MoveProcess();
        Deathcheck();
        RangeCheck();
    }

    public void SetStatus(CharacrStatus _status)
    {
        status = _status;

        Debug.Log(status.name);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, status.atkrange);
    }

    public void MoveProcess()
    {
        if (GUIManager.Instance.waveEnd == false && playerdeath == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * status.movespd * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += Vector3.down * status.movespd * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * status.movespd * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * status.movespd * Time.deltaTime;
            }
        }
    }

    public void Deathcheck()
    {
        if (GUIManager.Instance.curhp > 0)
        {
            playerdeath = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = live;
        }
        else if (GUIManager.Instance.curhp <= 0)
        {
            playerdeath = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = death;
        }
    }

    public void AttackTimer1()
    {
        attackcool1 = GUIManager.Instance.haveweaponstat[0].atkcool * (1 - (status.atkspd / 100));
        attackaftertime1 -= Time.deltaTime;
        if (attackaftertime1 > 0)
            canattack1 = false;
        else if (attackaftertime1 <= 0)
        {
            if (canattack1)
            {
                attackaftertime1 = attackcool1;
                canattack1 = true;
            }
        }
    }

    public void RangeCheck()
    {
        float dist;
        for (int i = 0; i < RandomSpawnManager.monsterList.Count; i++)
        {
            dist = Vector3.Distance(gameObject.transform.position, RandomSpawnManager.monsterList[i].transform.position);
            if (dist <= status.atkrange)
            {
                if (targetList.Contains(RandomSpawnManager.monsterList[i])) { }
                else if (!targetList.Contains(RandomSpawnManager.monsterList[i]))
                    targetList.Add(RandomSpawnManager.monsterList[i]);
            }
            if (dist > status.atkrange)
            {
                if (targetList.Contains(RandomSpawnManager.monsterList[i]))
                    targetList.Remove(RandomSpawnManager.monsterList[i]);
                else if (!targetList.Contains(RandomSpawnManager.monsterList[i])) { }
            }
        }
    }

    public void Shot()
    {
        var prefab = bullet;
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
