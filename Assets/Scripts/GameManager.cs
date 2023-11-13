using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public PlayerStates state;
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

    public void Start()
    {
        bestLapText.gameObject.SetActive(false);
        StartCoroutine(StartCountDownSequence(countDownMessages));
    }

    private void Update()
    {
        currentCountdown -= Time.deltaTime;
        countdownText.color = Color.Lerp(startColor, endColor, currentCountdown / textTime);

        if(inRace == true)
        {
            lapTime += Time.deltaTime;
        }

        DisplayTime(lapTime, timerText);
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
        lapTime = 0;

        bestTime = lapTimes.Min();
        bestLapText.text = "Best Lap: " + bestTime.ToString("F2");
        bestLapText.gameObject.SetActive(true);
    }
}
