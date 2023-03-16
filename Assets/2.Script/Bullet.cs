using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon weapon;
    public Player player;
    public Vector3 vDist;
    public float bulletSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move(bulletSpeed);
    }

    public void Move(float speed)
    {
        transform.position += vDist * speed * Time.deltaTime;

        //Vector3 vDist = targetGO.transform.position - transform.position;
        //Vector3 vDir = vDist.normalized;
        //float fDist = vDist.magnitude;
        //  
        //if (fDist > speed * Time.deltaTime)
        //{
        //    transform.position += vDir * speed * Time.deltaTime;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
