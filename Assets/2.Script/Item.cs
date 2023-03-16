using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Drain();
        if (GUIManager.Instance.waveEnd)
            WaveEndDrain();
    }

    public void Drain()
    {
        float speed = 3f;
        Vector3 vTarget = ItemManager.Instance.player.transform.position;
        Vector3 vPos = transform.position;

        Vector3 vDist = vTarget - vPos;
        Vector3 vDir = vDist.normalized;
        float fDist = vDist.magnitude;

        if (fDist < ItemManager.Instance.drainDist)
        {
            transform.position += vDir * speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (ItemManager.Instance.curstate == ItemManager.ItemTypeE.EXP)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ItemManager.Instance.GiveExp(10);
                ItemManager.Instance.GiveGold(5);
                Destroy(this.gameObject);
            }
        }
        else if (ItemManager.Instance.curstate == ItemManager.ItemTypeE.HP)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ItemManager.Instance.GiveHp(2);
                Destroy(this.gameObject);
            }
        }
    }

    void WaveEndDrain()
    {
        float speed = 10f;
        Vector3 vTarget = ItemManager.Instance.player.transform.position;
        Vector3 vPos = transform.position;

        Vector3 vDist = vTarget - vPos;
        Vector3 vDir = vDist.normalized;
        float fDist = vDist.magnitude;

        transform.position += vDir * speed * Time.deltaTime;
    }
}
