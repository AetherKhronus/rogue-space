using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionsMenu : MonoBehaviour {

    public Button mission1Button;
    public Button mission2Button;
    public Button mission3Button;
    public Button mission4Button;
    public Text mission1Text;
    public Text mission2Text;
    public Text mission3Text;
    public Text mission4Text;

    void Start() {
        
        var completed = PlayerPrefs.GetInt("MissionsCompleted");

        if (completed == 1) {

            mission2Button.interactable = true;
            mission2Text.color = Color.white;

        } else if (completed == 2) {

            mission2Button.interactable = true;
            mission2Text.color = Color.white;
            mission3Button.interactable = true;
            mission3Text.color = Color.white;

        } else if (completed == 3) {

            mission2Button.interactable = true;
            mission2Text.color = Color.white;
            mission3Button.interactable = true;
            mission3Text.color = Color.white;
            mission4Button.interactable = true;
            mission4Text.color = Color.white;

        } 

    }

    public void PlayMission1() {

        SceneManager.LoadScene("Mission 1");

    }

    public void PlayMission2() {

        SceneManager.LoadScene("Mission 2");

    }

    public void PlayMission3() {

        SceneManager.LoadScene("Mission 3");

    }

    public void PlayMission4() {

        SceneManager.LoadScene("Mission 4");

    }

    public void GoBack() {

        SceneManager.LoadScene("Main Menu");

    }

}
