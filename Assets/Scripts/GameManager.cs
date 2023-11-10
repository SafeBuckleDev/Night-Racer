using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void Start()
    {
        StartCoroutine(StartCountDownSequence(countDownMessages));
    }

    private void Update()
    {
        currentCountdown -= Time.deltaTime;
        countdownText.color = Color.Lerp(startColor, endColor, currentCountdown / textTime);
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
    }
}
