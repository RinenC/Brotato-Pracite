using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public GameObject target;

    public ItemList weaponStat;

    public int idx = 0;

    public GameObject bullet;
    public float shotCoolTime;
    public float shotAfterTime;
    public int maxweapon = 6;

    private void Start()
    {
        RotateWeapon();
        weaponStat = GUIManager.Instance.haveweaponstat[idx];
        shotCoolTime = GUIManager.Instance.haveweaponstat[idx].atkcool;
        shotAfterTime = 0;
    }

    private void Update()
    {
        RangeCheck();
        ShotCoolTimer();
        WeaponLookAt();
    }

    public void RotateWeapon()
    {
        List<Vector3> notOpposite = CalcLinePos(3, 0.25f, false);
        List<Vector3> opposite = CalcLinePos(3, 0.25f, true);
        switch (idx)
        {
            case 0:
                transform.localPosition = notOpposite[idx];
                break;
            case 1:
                transform.localPosition = notOpposite[idx];
                break;
            case 2:
                transform.localPosition = notOpposite[idx];
                break;
            case 3:
                GetComponent<SpriteRenderer>().flipY = true;
                transform.localPosition = opposite[idx-3];
                break;
            case 4:
                GetComponent<SpriteRenderer>().flipY = true;
                transform.localPosition = opposite[idx-3];
                break;
            case 5:
                GetComponent<SpriteRenderer>().flipY = true;
                transform.localPosition = opposite[idx-3];
                break;
        }
    }

    List<Vector3> CalcLinePos(int _count, float _radius, bool _isOppositeSide)
    {
        var linePosList = new List<Vector3>();

        if (!_isOppositeSide)
        {
            float startAngle = ((60 * (_count - 1)) / 2);

            for (int i = 0; i < _count; i++)
            {
                float x = _radius * Mathf.Cos((startAngle - (60 * i)) * Mathf.Deg2Rad);
                float y = _radius * Mathf.Sin((startAngle - (60 * i)) * Mathf.Deg2Rad);

                linePosList.Add(new Vector3(x, y, y));
                //tankerTypeMinionList[i].linePos = new Vector3(x, y, y);
            }
        }
        else
        {
            float startAngle = ((60 * (_count - 1)) / 2);

            for (int i = 0; i < _count; i++)
            {
                float x = _radius * Mathf.Cos((startAngle - (60 * i)) * Mathf.Deg2Rad);
                float y = _radius * Mathf.Sin((startAngle - (60 * i)) * Mathf.Deg2Rad);

                linePosList.Add(new Vector3(-x, y, y));
                //tankerTypeMinionList[i].linePos = new Vector3(x, y, y);
            }
        }

        return linePosList;
    }

    public void WeaponLookAt()
    {
        if (target != null)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            targetPos.z = myPos.z;

            Vector3 vectorToTarget = targetPos - myPos;
            Vector3 quaternionToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 10000 * Time.deltaTime);
            if (transform.localEulerAngles.z <= 90 || transform.localEulerAngles.z >= 270)
                GetComponent<SpriteRenderer>().flipY = false;
            else
                GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    public void ShotCoolTimer()
    {
        if (target != null)
        {
            if (shotAfterTime <= 0)
            {
                Shot();
                shotAfterTime = shotCoolTime;
            }
            else if (shotAfterTime > 0)
            {
                shotAfterTime -= Time.deltaTime;
            }
        }
    }

    public void Shot()
    {
        var CopyBullet = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
        CopyBullet.player = player;
        CopyBullet.weapon = this;
        Vector3 vDist = target.transform.position - transform.position;
        Vector3 vDir = vDist.normalized;
        CopyBullet.vDist = vDir;
        shotAfterTime = 0f;
    }

    public void RangeCheck()
    {
        float mindist = player.status.atkrange;
        GameObject target = null;
        float dist;
        for (int i = 0; i < player.targetList.Count; i++)
        {
            dist = Vector3.Distance(this.transform.position, player.targetList[i].transform.position);
            if (dist < mindist)
            {
                mindist = dist;
                target = player.targetList[i];
            }
        }
        this.target = target;
    }
}
