using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	public GameObject mainMenuPanel;
	public GameObject controlsPanel;
	public GameObject ohHiMark;
	public Button returnToMenuButton;
	public bool controlsOn = false;
	public bool creditsOn = false;

	void Start() {
		ohHiMark.SetActive(false);
		controlsPanel.SetActive(false);
		returnToMenuButton.gameObject.SetActive(false);
	}

	public void StartNewGame() {
		SceneManager.LoadScene("SampleScene");
	}

	public void QuitTheGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void DisplayCredits() {
		creditsOn = true;
		ohHiMark.SetActive(true);
		returnToMenuButton.gameObject.SetActive(!false);
		mainMenuPanel.SetActive(false);
	}

	public void DisplayControls() {
		controlsOn = true;
		controlsPanel.SetActive(true);
		returnToMenuButton.gameObject.SetActive(!false);
		mainMenuPanel.SetActive(false);
	}

	public void FuckGoBack() {
		controlsOn = creditsOn = false;
		controlsPanel.SetActive(false);
		ohHiMark.SetActive(false);
		mainMenuPanel.SetActive(true);
		returnToMenuButton.gameObject.SetActive(false);
	}
}
