using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct RaceData // save info
{
    public string name; // name of racer

    public float raceTime; // time of race

    //public List<GhostWaypoint> ghostWaypoints;
}

[System.Serializable]
public struct GhostWaypoint
{
    public Vector3 pos;
    public Quaternion rot;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool debug;
    public PlayerStates state;
    [HideInInspector]
    public string racerName;
    [SerializeField] private GameObject carObj;
    public int totalLaps;
    public int currentLap;
    [Space]

    [Header("GameCountdown")]
    [SerializeField] private string[] countDownMessages;
    [SerializeField] private float textTime;
    [SerializeField]private Color startColor;
    [SerializeField] private Color endColor;
    private float currentCountdown;
    [SerializeField] private TMP_Text countdownText;
    [Space]
    private bool inRace;
    private float lapTime;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text bestLapText;
    private List<float> lapTimes = new List<float>();
    private float bestTime;
    [Space]
    private string path;
    private string persistentPath;
    private List<GhostWaypoint> pWaypoints;
    [Space]
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private TMP_Text lapcounterTxt;
    private bool canExit;

    public void Start()
    {
        canExit = false;

        currentLap = 1;

        racerName = PlayerPrefs.GetString("DriverName", name);

        bestLapText.gameObject.SetActive(false);
        StartCoroutine(StartCountDownSequence(countDownMessages));
    }

    private void Update()
    {
        if(debug == true && Input.GetKeyDown(KeyCode.L))
        {
            TriggerLap();
        }

        currentCountdown -= Time.deltaTime;
        countdownText.color = Color.Lerp(startColor, endColor, currentCountdown / textTime);

        if(inRace == true)
        {
            lapTime += Time.deltaTime;
        }

        DisplayTime(lapTime, timerText);

        if(canExit == true && Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void DisplayTime(float val, TMP_Text text)
    {
        if(val < 0)
        {
            val = 0;
        }

        float min = Mathf.FloorToInt(val / 60);
        float sec = Mathf.FloorToInt(val % 60);

        text.text = "Time: " + val.ToString("F2");
    }

    public IEnumerator StartCountDownSequence(string[] messages)
    {
        foreach (string msg in messages)
        {
            yield return new WaitForSeconds(textTime);
            currentCountdown = textTime;
            countdownText.text = msg;
        }

        state = PlayerStates.racing;
        inRace = true;
    }

    public void TriggerLap()
    {
        Debug.Log("LAP!");
        lapTimes.Add(lapTime);

        if (currentLap < totalLaps)
        {
            currentLap++;
            lapTime = 0;
        }
        else
        {
            inRace = false;

            CalTotalRaceTime();

            SaveScore(racerName, CalTotalRaceTime());

            StartCoroutine(EndRaceSequence());
        }

        StartCoroutine(RecordBestLap());
    }

    private IEnumerator EndRaceSequence()
    {
        finishPanel.SetActive(true);

        lapcounterTxt.text = "LAP TIMES:\n";

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < lapTimes.Count; i++)
        {
            string lineTekst = "LAP" + (i + 1) + ": " + lapTimes[i].ToString("F2") + "\n";
            lapcounterTxt.text = lapcounterTxt.text + lineTekst;
            yield return new WaitForSeconds(1f);
        }

        lapcounterTxt.text = lapcounterTxt.text + "\n\nTOTAL TIME:";
        yield return new WaitForSeconds(1.25f);
        lapcounterTxt.text = lapcounterTxt.text + "\n" + CalTotalRaceTime().ToString("F2");
        yield return new WaitForSeconds(2.5f);
        lapcounterTxt.text = lapcounterTxt.text + "\n\nPRESS ANY KEY TO CONTINUE";
        canExit = true;
    }

    private IEnumerator RecordBestLap()
    {
        bestTime = lapTimes.Min();

        bestLapText.text = "Best Lap: " + bestTime.ToString("F2");
        bestLapText.gameObject.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            bestLapText.gameObject.SetActive(!bestLapText.gameObject.activeInHierarchy);
            yield return new WaitForSeconds(.3f);
        }
    }

    private float CalTotalRaceTime()
    {
        float time = 0;

        foreach (float lt in lapTimes)
        {
            time += lt;
        }

        Debug.Log("TOTAL TIME: " + time);

        return time;
    }

    private void SetDataPath(string name)
    {
        string filename = name + ".json";

        path = Application.dataPath + Path.AltDirectorySeparatorChar + filename;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + filename;
    }

    private void SaveData(RaceData data)
    {
        string savePath = persistentPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(data);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    private void SaveScore(string name ,float time)
    {
        RaceData data;
        data.name = name;
        data.raceTime = time;

        SetDataPath(name);

        SaveData(data);
    }
}