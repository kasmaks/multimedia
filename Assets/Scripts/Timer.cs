using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
	#region fields
	string TimerValue = GameOptionsManager.timeValue;
    string WhiteName = GameOptionsManager.playerWhiteName;
    string BlackName = GameOptionsManager.playerBlackName;

    int customTimer = GameOptionsManager.customTimerValue;
    int customIncr = GameOptionsManager.customIncrementValue; 

    public static Timer Instance { get; set; }

    public float timeRemainingWhite = 25;
	public bool timerIsRunningWhite = false;
    public Text timeTextWhite;

    public float timeRemainingBlack = 25;
    public bool timerIsRunningBlack = false;
    public Text timeTextBlack;

    public bool ifIncrement;
    public int incrementValue;

    public string result;

    public ScoreManager manager;
    public int flag = 0;

    #endregion

    private void Start()
	{
        Instance = this;
        SetTimers();
        Init();
    }

    void Init()
	{
        timerIsRunningWhite = true;
        timerIsRunningBlack = true;
        DisplayTime(timeRemainingWhite, timeTextWhite);
        DisplayTime(timeRemainingBlack - 1, timeTextBlack);
        flag = 0;
    }

    void SetTimers()
	{
        switch (TimerValue)
        {
            case "10":
                timeRemainingBlack = 10 * 60;
                timeRemainingWhite = 10 * 60;
                break;
            case "10 + 1":
                timeRemainingBlack = 10 * 60;
                timeRemainingWhite = 10 * 60;
                ifIncrement = true;
                incrementValue = 1;
                break;
            case "3":
                timeRemainingBlack = 3 * 60;
                timeRemainingWhite = 3 * 60;
                break;
            case "3 + 1": //3 + 1s increment
                
                break;
            case "15":
                timeRemainingBlack = 15 * 60;
                timeRemainingWhite = 15 * 60;
                break;
            case "15 + 1": //15 + 1s increment
                timeRemainingBlack = 15 * 60;
                timeRemainingWhite = 15 * 60;
                ifIncrement = true;
                incrementValue = 1;
                break;
            case "No time limit":
                GetComponent<Timer>().enabled = false;
                GameObject.Find("Timers").SetActive(false);
                break;
            case "Custom":
                timeRemainingBlack = customTimer * 60;
                timeRemainingWhite = customTimer * 60;
                ifIncrement = true;
                incrementValue = customIncr;
                break;
            default:
                timeRemainingBlack = 10 * 60;
                timeRemainingWhite = 10 * 60;
                break;
        }
    }

    void Update()
	{
        DateTime now = DateTime.Now;

        if (ChessBoardManager.Instance.isWhiteTurn)
		{
			timerIsRunningBlack = false;
            timerIsRunningWhite = true;

			if (ChessBoardManager.Instance.isMate) {
				timeRemainingWhite = 0;
				timerIsRunningWhite = false;
			}
            if (timerIsRunningWhite)
            {
                if (timeRemainingWhite > 0)
                {
                    timeRemainingWhite -= Time.deltaTime;
                    DisplayTime(timeRemainingWhite, timeTextWhite);
                }
                else
                {
                    result = BlackName + " won by timeout!";
                    ChessBoardManager.Instance.EndGamePopUp(result);
                    timeRemainingWhite = 0;
                    timerIsRunningWhite = false;
                    if(flag == 0)
					{
                        ChessBoardManager.Instance.manager.AddScore(new Score("1", BlackName, WhiteName + " (timeout)", now.ToString("MM/dd/yyyy H:mm"), ChessBoardManager.Instance.numberOfMoves, ChessBoardManager.Instance.chessNotation));
                        flag = 1;
                    }
                }
            }
        }
        else
		{
            timerIsRunningBlack = true;
            timerIsRunningWhite = false;
			if (ChessBoardManager.Instance.isMate) {
				timeRemainingBlack = 0;
				timerIsRunningBlack = false;
			}
            if (timerIsRunningBlack)
            {
                if (timeRemainingBlack > 0)
                {
                    timeRemainingBlack -= Time.deltaTime;
                    DisplayTime(timeRemainingBlack, timeTextBlack);
                }
                else
                {
                    result = WhiteName + " won by timeout!";
                    ChessBoardManager.Instance.EndGamePopUp(result);
                    timeRemainingBlack = 0;
                    timerIsRunningBlack = false;
                    
                    if(flag == 0)
					{
                        ChessBoardManager.Instance.manager.AddScore(new Score("1", WhiteName, BlackName + " (timeout)", now.ToString("MM/dd/yyyy H:mm"), ChessBoardManager.Instance.numberOfMoves, ChessBoardManager.Instance.chessNotation));
                        flag = 1;
					}
                }
            }
        }
    }

    public void DisplayTime(float timeToDisplay, Text timeText)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetWhiteName()
	{
        return WhiteName;
	}

    public string GetBlackName()
	{
        return BlackName;
	}

}

