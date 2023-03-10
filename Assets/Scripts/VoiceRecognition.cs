using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


public class VoiceRecognition : MonoBehaviour
{
	public Image VoiceControl;
	public Text RecognizerReading;

	public static VoiceRecognition Instance { get; set; }
	private DictationRecognizer DictationRecognizer;

	private Dictionary<string, Action> actions = new Dictionary<string, Action>();

	private char[] possible_numbers = { '1', '2', '3', '4', '5', '6', '7', '8' };
	private char[] possible_characters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
	
	public Errors errors_tables;

	private bool DeviceControl()
	{
		foreach (var device in Microphone.devices)
		{
			Debug.Log("Name: " + device);
			if (Microphone.devices.Length == 0)
				return false;
		}

		if (Application.systemLanguage == SystemLanguage.English)
		{
			Debug.Log("This system is in English. ");
		}
		else
		{
			return false;
		}

		if (PhraseRecognitionSystem.isSupported)
		{
			Debug.Log("Phrase Recognition is supported!");
		}
		else
		{
			return false;
		}
		return true;
	}

	public void RestartDictation()
	{
		DictationRecognizer.Stop();
		DictationRecognizer.Dispose();
		DictationRecognizer = new DictationRecognizer();
		DictationRecognizer.AutoSilenceTimeoutSeconds = 20f;
		DictationRecognizer.InitialSilenceTimeoutSeconds = 20f;
		DictationRecognizer.DictationResult += OnRecognizeSpeech;
		DictationRecognizer.Start();
		Debug.Log("RESTART");
	}

	public void Start()
	{
		Instance = this;

		if(DeviceControl())
		{
			DictationRecognizer = new DictationRecognizer();
			DictationRecognizer.AutoSilenceTimeoutSeconds = 20f;
			DictationRecognizer.InitialSilenceTimeoutSeconds = 20f;
			DictationRecognizer.DictationResult += OnRecognizeSpeech;
			DictationRecognizer.Start();
		}
	}

	public void Update()
	{
		if (DictationRecognizer.Status.Equals(SpeechSystemStatus.Running))
		{
			VoiceControl.GetComponent<Image>().color = Color.green;
		}
		else
		{
			VoiceControl.GetComponent<Image>().color = Color.red;
		}

	}

	public void OnRecognizeSpeech(string speech, ConfidenceLevel level)
	{
		Debug.Log("OnRecognizeSpeech");
		Debug.Log(speech);

		string ab = speech.ToString();
		if(ab.Length < 4 || ab.Length > 7)
		{
			RecognizerReading.text = "Couldn't recognize the command. Try again.";
		}
		if (ab.Length > 4)
		{
			ab = ab.Replace(" ", "");
			ab = ValidateWords(ab);
		}

		var first = ab.Substring(0, (int)(ab.Length / 2));
		var last = ab.Substring((int)(ab.Length / 2), (int)(ab.Length / 2));

		var startScreen = gameObject.GetComponent<StartScreen>();

		if (ab == "quit" || ab == "quitgame" || ab == "exit" || ab == "exitgame")
		{
			ChessBoardManager.Instance.popUpExitConfirmation.SetActive(true);
		}
		if (ChessBoardManager.Instance.popUpExitConfirmation.activeInHierarchy == true)
		{
			if (ab == "yes")
			{
				startScreen.LoadStartScene();
			}
			if (ab == "no" || ab == "cancel")
			{
				startScreen.DoNotExitGame();
			}
		}
		if (ChessBoardManager.Instance.popUpWindowForEndGame.activeInHierarchy == true)
		{
			if (ab == "playagain")
			{
				startScreen.LoadGameScene();
				RestartDictation();
				Time.timeScale = 1;

			}
		}

		if (first.Length == 2 && last.Length == 2)
		{
			first = ValidateMovement(first, possible_characters, possible_numbers);
			last = ValidateMovement(last, possible_characters, possible_numbers);

			var positionDictionary = ChessBoardManager.Instance.PreparePositionDictionary();

			string aFind = positionDictionary.FirstOrDefault(x => x.Value == first).Key;
			string bFind = positionDictionary.FirstOrDefault(x => x.Value == last).Key;

			Tuple<int, int> helperX = StringHelper.Instance.GetBoardCoordinates(aFind);
			Tuple<int, int> helperY = StringHelper.Instance.GetBoardCoordinates(bFind);

			RecognizerReading.text = "From:" + first + " to:" + last;

			ExecuteMovement(helperX, helperY);
		}

	}

	private void ExecuteMovement(Tuple<int, int> from, Tuple<int, int> to)
	{
		ChessBoardManager.Instance.SelectChessPiece(from.Item1, from.Item2);
		ChessBoardManager.Instance.MoveChessPiece(to.Item1, to.Item2);
	}

	private string ValidateWords(string input)
	{
		string output = input;
		if(input.Length == 5)
		{
			string copied = Char.ToString(input[0]) + Char.ToString(input[1]);
			string rest = Char.ToString(input[2]) + Char.ToString(input[3]) + Char.ToString(input[4]);
			if (errors_tables.two_digits_errors_with_A_letter.Contains(copied))
			{
				char corrected = 'A';
				output = corrected + rest;
			}
		}
		else if(input.Length == 6)
		{
			string copied = Char.ToString(input[0]) + Char.ToString(input[1]) + Char.ToString(input[2]);
			string rest = Char.ToString(input[3]) + Char.ToString(input[4]) + Char.ToString(input[5]);
			if (errors_tables.three_digits_errors_with_A_letter.Contains(copied))
			{
				char corrected = 'A';
				output = corrected + rest;
			}
		}

		return output;
	}

	private string ValidateMovement(string value, char[] letters, char[] numbers)
	{
		char first = value[0];
		char second = value[1];

		if (!letters.Contains(value[0]))
		{
			if (value[0] == '8')
				first = 'A';

		}
		if (!numbers.Contains(value[1]))
		{
			if (value[1] == 'A')
				second = '8';
		}

		string output = Char.ToString(first) + Char.ToString(second);
		return output;
	}
}

