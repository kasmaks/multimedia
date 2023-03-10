using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class handling game options
 */
public class GameOptionsManager : MonoBehaviour
{
	public Dropdown timeDropdown;
	public GameObject customOptions;
	public Text PlayerWhiteName;
	public Text PlayerBlackName;
	public Text CustomTimer;
	public Text CustomIncrement;
	public Toggle readMovesToggle;
	public Toggle cameraRotationToggle;

	public static string timeValue;
	public static string playerWhiteName;
	public static string playerBlackName;

	public static int customTimerValue;
	public static int customIncrementValue;

	public static bool VoiceReadMovesEnabled;
	public static bool CameraRotationEnabled;


	public void Update()
	{
		timeValue = timeDropdown.options[timeDropdown.value].text;
		playerWhiteName = PlayerWhiteName.text; 
		playerBlackName = PlayerBlackName.text;

		VoiceReadMovesEnabled = readMovesToggle.isOn;
		CameraRotationEnabled = cameraRotationToggle.isOn;

		SetCustomOptions();
	}

	public void EnableCustomOptions()
	{
		if(timeDropdown.options[timeDropdown.value].text == "Custom")
		{
			customOptions.SetActive(true);
		}
		else
		{
			customOptions.SetActive(false);
		}
	}

	public void SetCustomOptions()
	{
		if (timeDropdown.options[timeDropdown.value].text == "Custom")
		{
			if (!String.IsNullOrEmpty(CustomTimer.text))
				customTimerValue = int.Parse(CustomTimer.text);
			if (!String.IsNullOrEmpty(CustomIncrement.text))
				customIncrementValue = int.Parse(CustomIncrement.text);
		}
	}

}
