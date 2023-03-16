using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    public Player player;
    public EnemyStatus enemystatus;

    public GameObject playerGO;
    public GameObject itemGO;

    public float speed = 0.5f;

    public bool isTrigger = false;
    public bool death = false;

    public float attackCooltime = 1f;
    public float attackAfterTime = 0f;
    public float curhp;

    void Start()
    {
        GetEnemyDatabase(DBLoader.Instance.GetEnemyStatByIdx(0));
        curhp = enemystatus.maxhp;
        attackAfterTime = 0f;
    }

    void Update()
    {
        if (GUIManager.Instance.waveEnd == false)
        {
            MoveProcess();
            DamageProcess();
        }
    }

    public void GetEnemyDatabase(EnemyStatus status)
    {
        enemystatus = status;
    }

    public void MoveProcess()
    {
        Vector3 vTargetPos = playerGO.transform.position;
        Vector3 vMyPos = transform.position;

        Vector3 vDist = vTargetPos - vMyPos;//위치의 차이를 이용한 거리구하기
        Vector3 vDir = vDist.normalized;//두물체사이의 방향(평준화-거리를뺀 이동량) //< normalized = 길이가 1인 백터 ( 힘이 1이고 방향만 있음.) 상태로 만들어줌.
        float fDist = vDist.magnitude; //두물체사이의 거리(스칼라-순수이동량)

        if (fDist > speed * Time.deltaTime)//한프레임의 이동거리보다 클때만 이동한다.
        {
            transform.position += vDir * speed * Time.deltaTime;
        }
    }

    public void DamageProcess()
    {
        attackAfterTime -= Time.deltaTime;

        if (attackAfterTime <= 0 && isTrigger)
        {
            GameManager.Instance.attacked = true;
            if (GUIManager.Instance.curhp > 0)
            {
                GUIManager.Instance.curhp -= enemystatus.dmg;
                attackAfterTime = attackCooltime;
                GameManager.Instance.attacked = false;
            }
        }
    }
    public void SetTarget(Player _player)
    {
        player = _player;
        playerGO = _player.gameObject;
    }

    public void DeathCheck()
    {
        if (curhp <= 0)
        {
            death = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
        }
        if (collision.CompareTag("Bullet"))
        {
            GameManager.Instance.DamageCalProcess(this, collision.GetComponent<Bullet>().player, collision.GetComponent<Bullet>().weapon.weaponStat);
            DeathCheck();
            if (death)
            {
                RandomSpawnManager.monsterList.Remove(this.gameObject);
                ItemManager.Instance.SetItemType(ItemManager.ItemTypeE.EXP);
                ItemManager.Instance.ExpItemSprite(itemGO);
                Instantiate(itemGO, transform.position, Quaternion.identity).GetComponent<Item>();
                collision.GetComponent<Bullet>().weapon.target = null;
                collision.GetComponent<Bullet>().player.targetList.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
