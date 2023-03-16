using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnManager : MonoSingleton<RandomSpawnManager>
{
    public GameObject opossum;
    public GameObject frog;
    public GameObject eagle;

    public int count = 30; //생성할 몬스터(게임 오브젝트)의 개수
    private BoxCollider2D area; //BoxCollider2D의 사이즈를 가져오기 위한 변수
    public static List<GameObject> monsterList = new List<GameObject>(); //생성한 몬스터 오브젝트 리스트

    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        //StartCoroutine("Spawn", 20); //유니티에서만 지원하는 기능. 처리와 처리 사이에 대기시간을 넣어주거나, 여러 처리를 동시에 병렬로 할 수 있게 해줌.
    }

    //게임 오브젝트를 복제하여 scene에 추가
    public IEnumerator Spawn(float delayTime, GameObject enemy)
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.5f);
        for (int i = 0; i < count; i++) //count만큼 몬스터 생성
        {
            if(!GUIManager.Instance.waveEnd)
            {
                Vector3 spawnPos = GetRandomPosition(); //랜덤 위치 return

                //원본, 위치, 회전값을 매개변수로 받아 오브젝트 복제
                GameObject instance = Instantiate(enemy, spawnPos, Quaternion.identity);
                instance.GetComponent<Opossum>().SetTarget(GameManager.Instance.player);
                monsterList.Add(instance); //오브젝트 관리를 위해 리스트에 add

                yield return waitTime;
            }
        }
    }

    public void Delete()
    {
        area.enabled = false;

        for (int i = 0; i < monsterList.Count; i++) //몬스터 삭제
            Destroy(monsterList[i].gameObject);

        monsterList.Clear();           //몬스터 리스트 비우기
        area.enabled = true;
    }

    //BoxCollider2D 내의 랜덤한 위치를 return
    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;  //오브젝트의 위치
        Vector2 size = area.size;                   //box colider2d, 즉 맵의 크기 벡터

        //x, y축 랜덤 좌표 얻기
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    public GameObject GetRandomEnemy()
    {
        switch(RandomNumberGenerator.Instance.RNGCount(3))
        {
            case 1:
                return opossum;
            case 2:
                return frog;
            case 3:
                return eagle;
            default:
                return null;
        }
    }
}
