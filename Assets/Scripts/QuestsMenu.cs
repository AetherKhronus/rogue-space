using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestsMenu : MonoBehaviour {

    public GameObject window;
    public GameObject canvasQuests;
    public GameObject canvasChoice;
    public GameObject buffImgs;
    public GameObject buffMenu;

    public Button claim1Button;
    public Button claim2Button;
    public Button claim3Button;
    public Button claim4Button;
    public Button claim5Button;

    public Text claim1Text;
    public Text claim2Text;
    public Text claim3Text;
    public Text claim4Text;
    public Text claim5Text;
    public Text quest4Text;
    public Text quest5Text;

    void Start() {

        BorderColor();
        CheckQuests();

    }

    private void BorderColor() {

        var color = PlayerPrefs.GetInt("Color"); //red green blue

        if (color == 1) {

            window.GetComponent<SpriteRenderer>().color = Color.red;

        } else if (color == 2) {

            window.GetComponent<SpriteRenderer>().color = Color.green;

        } else {

            window.GetComponent<SpriteRenderer>().color = Color.blue;

        }

    }

    private void CheckQuests() { //1 or 0

        var q1 = PlayerPrefs.GetInt("Quest1Completed"); 
        var q2 = PlayerPrefs.GetInt("Quest2Completed");
        var q3 = PlayerPrefs.GetInt("Quest3Completed");
        var q4 = PlayerPrefs.GetInt("Quest4Completed");
        var q5 = PlayerPrefs.GetInt("Quest5Completed");

        var q1C = PlayerPrefs.GetInt("Quest1Claimed");
        var q2C = PlayerPrefs.GetInt("Quest2Claimed");
        var q3C = PlayerPrefs.GetInt("Quest3Claimed");
        var q4C = PlayerPrefs.GetInt("Quest4Claimed");
        var q5C = PlayerPrefs.GetInt("Quest5Claimed");

        CheckQuest(1, q1, q1C);
        CheckQuest(2, q2, q2C);
        CheckQuest(3, q3, q3C);
        CheckQuest(4, q4, q4C);
        CheckQuest(5, q5, q5C);
    }

    private void CheckQuest(int nr, int q , int qC) {

        Button claimButton;
        Text claimText;
        int k = PlayerPrefs.GetInt("Quest4&5");

        switch (nr) {

            case 1:
                claimButton = claim1Button;
                claimText = claim1Text;
            break;

            case 2:
                claimButton = claim2Button;
                claimText = claim2Text;
            break;

            case 3:
                claimButton = claim3Button;
                claimText = claim3Text;
            break;

            case 4:
                claimButton = claim4Button;
                claimText = claim4Text;
 
                if (k > 500) {

                    quest4Text.text = "Quest 4: Destroy (500/500) Enemies";
                    PlayerPrefs.GetInt("Quest4Completed" , 1);
                    PlayerPrefs.Save();
                    q = 1;

                } else {

                    quest4Text.text = "Quest 4: Destroy (" + PlayerPrefs.GetInt("Quest4&5") + "/500) Enemies";

                }
            break;

            default:
                claimButton = claim5Button;
                claimText = claim5Text;

                if (k > 1000) {

                    quest5Text.text = "Quest 5: Destroy (1000/1000) Enemies";
                    PlayerPrefs.GetInt("Quest5Completed" , 1);
                    PlayerPrefs.Save();
                    q = 1;

                } else {

                    quest5Text.text = "Quest 5: Destroy (" + PlayerPrefs.GetInt("Quest4&5") + "/1000) Enemies";

                }
            break;
        }

        if (q == 0) { // In Progress

            claimButton.interactable = false;
            claimText.text = "In Progress";

        } else if (qC == 0) { // Claim

            claimButton.interactable = true;
            claimText.text = "Claim";
            claimText.color = Color.yellow;

        } else { // Claimed

            claimButton.interactable = false;
            claimText.text = "Claimed";
            claimText.color = new Color(90 , 90 , 90);

        }
    }

    public void GoBack() {

        SceneManager.LoadScene("Main Menu");

    }

    public void ClaimQuest1() {

        canvasQuests.SetActive(false);

        PlayerPrefs.SetInt("Quest1Claimed" , 1);
        PlayerPrefs.Save();

        CheckQuest(1, 1, 1);
        
        canvasChoice.SetActive(true);
        buffImgs.SetActive(true);
        buffMenu.SetActive(true);

    }

    public void ClaimQuest2() {
        
        canvasQuests.SetActive(false);

        PlayerPrefs.SetInt("Quest2Claimed" , 1);
        PlayerPrefs.Save();

        CheckQuest(2, 1, 1);
        
        canvasChoice.SetActive(true);
        buffImgs.SetActive(true);
        buffMenu.SetActive(true);

    }

    public void ClaimQuest3() {
        
        canvasQuests.SetActive(false);

        PlayerPrefs.SetInt("Quest3Claimed" , 1);
        PlayerPrefs.Save();

        CheckQuest(3, 1, 1);
        
        canvasChoice.SetActive(true);
        buffImgs.SetActive(true);
        buffMenu.SetActive(true);
        
    }

    public void ClaimQuest4() {
        
        canvasQuests.SetActive(false);

        PlayerPrefs.SetInt("Quest4Claimed" , 1);
        PlayerPrefs.Save();

        CheckQuest(4, 1, 1);
        
        canvasChoice.SetActive(true);
        buffImgs.SetActive(true);
        buffMenu.SetActive(true);
        
    }

    public void ClaimQuest5() {
        
        canvasQuests.SetActive(false);

        PlayerPrefs.SetInt("Quest5Claimed" , 1);
        PlayerPrefs.Save();

        CheckQuest(5, 1, 1);

        canvasChoice.SetActive(true);
        buffImgs.SetActive(true);
        buffMenu.SetActive(true);
        
    }    

}
