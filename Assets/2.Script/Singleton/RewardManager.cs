using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoSingleton<RewardManager>
{
    public List<Sprite> imageSource;
    public List<int> randomList;
    // Start is called before the first frame update
    void Start()
    {
        randomList = GameManager.Instance.RandomInts(4, 16);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEvent()
    {
        randomList = GameManager.Instance.RandomInts(4, 16);
    }
}
