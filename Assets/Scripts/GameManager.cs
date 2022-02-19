using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
public static GameManager Instance;
    public int Score;
    public string Name;

    private void Awake()
    {

        
        Instance=this;
        DontDestroyOnLoad(gameObject);
        //LoadScore();
    }

    

[System.Serializable]
class SaveData
{
    public int Score;
    public string Name;
    
}

    public void SaveScore()
    {
        SaveData data=new SaveData();
        data.Score=Score;
        data.Name=Name;

        string json=JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath+ "/savefile1.json",json);
    }

    public void LoadScore()
    {
        string path=Application.persistentDataPath+ "/savefile1.json";
        if(File.Exists(path))
        {
            string json=File.ReadAllText(path);
            SaveData data=JsonUtility.FromJson<SaveData>(json);

            Score=data.Score;
            Name=data.Name;
        }
    }

}
