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
