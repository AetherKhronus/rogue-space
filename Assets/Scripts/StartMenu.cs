using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Button continueButton;
    public Text continueText;

    void Start() {
        
        if (PlayerPrefs.HasKey("MissionsCompleted")) {

            continueButton.interactable = true;
            continueText.color = Color.white;
        }

    }

    public void NewGame() {

        ResetStats();

        SceneManager.LoadScene("Choose");

    }

    public void ContinueGame() {

        SceneManager.LoadScene("Main Menu");

    }

    public void QuitGame() {

        Application.Quit();

    }

    private void ResetStats() {

        PlayerPrefs.SetInt("MissionsCompleted" , 0);
        PlayerPrefs.SetInt("LastMissionDone" , 0);
        PlayerPrefs.SetInt("MissionKills" , 0);
        PlayerPrefs.SetInt("MissionCoins" , 0);

        PlayerPrefs.SetInt("Quest1Completed" , 0);
        PlayerPrefs.SetInt("Quest2Completed" , 0);
        PlayerPrefs.SetInt("Quest3Completed" , 0);
        PlayerPrefs.SetInt("Quest4Completed" , 0);
        PlayerPrefs.SetInt("Quest5Completed" , 0);

        PlayerPrefs.SetInt("Quest1Claimed" , 0);
        PlayerPrefs.SetInt("Quest2Claimed" , 0);
        PlayerPrefs.SetInt("Quest3Claimed" , 0);
        PlayerPrefs.SetInt("Quest4Claimed" , 0);
        PlayerPrefs.SetInt("Quest5Claimed" , 0);

        PlayerPrefs.SetInt("Quest4&5" , 0);

        PlayerPrefs.SetInt("DMG" , 1);
        PlayerPrefs.SetInt("HP" , 1);
        PlayerPrefs.SetInt("SP" , 1);
        PlayerPrefs.SetInt("MG" , 0);
        PlayerPrefs.SetInt("Coins" , 0);
        PlayerPrefs.SetInt("Total Coins" , 0);
        PlayerPrefs.SetInt("Total Coins Spent" , 0);

        PlayerPrefs.SetString("Buffs" , "0_0_0_0_0");

        PlayerPrefs.Save();

    }

}
