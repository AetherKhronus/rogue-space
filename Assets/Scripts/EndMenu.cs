using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {

    public Text coinsText;
    public Text questText;
    public Text enemiesText;
    public GameObject window;

    void Start() {

        int missionDone = PlayerPrefs.GetInt("LastMissionDone");

        if (missionDone != -1) {

            CheckQuests(missionDone);
            CheckGoldVictory();

            enemiesText.text = "Enemies destroyed: " + PlayerPrefs.GetInt("MissionKills");


        } else {

            questText.text = "";
            CheckGoldDefeat();

            enemiesText.text = "";
        }

        CheckColor();

    }

    private void CheckQuests(int missionDone) {

        int quest;

        if (missionDone == 1) {

            quest = PlayerPrefs.GetInt("Quest1Completed");

            if (quest == 1) {

                questText.text = "Quest 1 done: yes";

            } else {

                questText.text = "Quest 1 done: no";

            }

        } else if (missionDone == 2) {

            quest = PlayerPrefs.GetInt("Quest2Completed");

            if (quest == 1) {

                questText.text = "Quest 2 done: yes";

            } else {

                questText.text = "Quest 2 done: no";

            }

        } else {

            quest = PlayerPrefs.GetInt("Quest3Completed");

            if (quest == 1) {

                questText.text = "Quest 3 done: yes";

            } else {

                questText.text = "Quest 3 done: no";

            }
            
        }

    }

    private void CheckGoldVictory() {

        int coins = PlayerPrefs.GetInt("MissionCoins");
        coinsText.text = "Gold collected: " + coins;
    }

    private void CheckGoldDefeat() {

        int coins = PlayerPrefs.GetInt("MissionCoins") / 10;
        int missionCoins = PlayerPrefs.GetInt("MissionCoins");
        coinsText.text = "Gold collected: " + coins + " (10% of " + missionCoins + ")";
    }

    private void CheckColor() {

        int color = PlayerPrefs.GetInt("Color"); //red green blue

        if (color == 1) {

            window.GetComponent<SpriteRenderer>().color = Color.red;

        } else if (color == 2) {

            window.GetComponent<SpriteRenderer>().color = Color.green;

        } else {

            window.GetComponent<SpriteRenderer>().color = Color.blue;

        }

    }

    public void Continue() {

        SceneManager.LoadScene("Main Menu");

    }

    public void Return() {

        SceneManager.LoadScene("Main Menu");

    }

}
