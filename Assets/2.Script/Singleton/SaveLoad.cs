//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SaveLoad : MonoBehaviour
//{
//    #region 저장불러오기
//    [Header("저장파일")]

//    private string m_sSaveFileDirectory;  // 저장할 폴더 경로
//    private string m_sSaveFileName = "/GameData.json"; // 파일 이름

//    public SaveInfo saveInfo;

//    public void RemoteStart()
//    {
//        m_sSaveFileDirectory = Application.persistentDataPath + "/Save/";
//        string filecheck = m_sSaveFileDirectory + m_sSaveFileName;

//        if (!Directory.Exists(m_sSaveFileDirectory)) // 해당 경로가 존재하지 않는다면
//            Directory.CreateDirectory(m_sSaveFileDirectory); // 폴더 생성(경로 생성)

//        _load();
//        _save();

//        //string jdata = JsonConvert.SerializeObject(gd);
//    }


//    public void _reset()
//    {
//        //string jdata = JsonConvert.SerializeObject(gData, Formatting.Indented);
//        //File.WriteAllText(Application.persistentDataPath + "/czSaveData.json", jdata);
//        string filecheck = m_sSaveFileDirectory + m_sSaveFileName;
//        File.Delete(filecheck);
//    }



//    public void _save()
//    {
//        saveInfo.GOLD = m_nGold;
//        saveInfo.LV = m_nNowWeaponLv;
//        saveInfo.ID = m_nNowWeaponID;

//        string jdata = JsonUtility.ToJson(saveInfo);

//        File.WriteAllText(m_sSaveFileDirectory + m_sSaveFileName, jdata);
//    }

//    public void _load()
//    {
//        string filecheck = m_sSaveFileDirectory + m_sSaveFileName;
//        //JObject
//        //Debug.Log(File.Exists(filecheck) +"   " + jdata);
//        if (File.Exists(filecheck))
//        {
//            string jdata = File.ReadAllText(m_sSaveFileDirectory + m_sSaveFileName);

//            saveInfo = JsonConvert.DeserializeObject<SaveInfo>(jdata);
//            //GameManager.instance.getSaveLoad().gData = gData;
//            //Debug.Log("파일 불러오기");

//            m_nGold = saveInfo.GOLD;
//            m_nNowWeaponLv = saveInfo.LV;
//            m_nNowWeaponID = saveInfo.ID;
//        }
//        else
//        {
//            saveInfo = new SaveInfo();

//            //Debug.Log("파일 새로 생성");
//        }
//    }

//    #endregion
//    [System.Serializable]
//    public class SaveInfo
//    {
//        public int GOLD;
//        public int LV;
//        public int ID;

//        public SaveInfo()
//        {
//            GOLD = 500;
//            LV = 1;
//            ID = 1;
//        }
//    }
//}
