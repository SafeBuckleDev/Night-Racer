using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.IO;

public class LeaderBoardLoader : MonoBehaviour
{
    [SerializeField] private List<RaceData> saves = new List<RaceData>();
    private List<float> saveLapT = new List<float>();

    [SerializeField] private List<RaceData> sortedSaves = new List<RaceData>();

    [SerializeField] private GameObject listParent;
    [SerializeField] private GameObject leaderBoardListTxtPrefab;

    private void Start()
    {
        LoadSaveFiles();
    }

    public void LoadSaveFiles()
    {
        foreach (var file in Directory.EnumerateFiles(Application.persistentDataPath + Path.AltDirectorySeparatorChar))
        {
            Debug.Log(file);

            using StreamReader reader = new StreamReader(file);
            string json = reader.ReadToEnd();

            RaceData data = JsonUtility.FromJson<RaceData>(json);

            saves.Add(data);
        }

        sortSaveFiles();
    }

    public void sortSaveFiles()
    {
        foreach (RaceData d in saves)
        {
            saveLapT.Add(d.raceTime);
        }

        saveLapT.Sort();

        foreach (float t in saveLapT)
        {
            foreach (RaceData s in saves)
            {
                if(t == s.raceTime)
                {
                    sortedSaves.Add(s);
                }
            }
        }

        sortedSaves.Reverse();

        foreach (RaceData d in sortedSaves)
        {
            GameObject txtObj = leaderBoardListTxtPrefab;

            Instantiate(txtObj, listParent.transform);
            leaderBoardListTxtPrefab.GetComponent<TMP_Text>().text = "NAME:" + d.name + " TIME:" + d.raceTime;
        }
    }
}
