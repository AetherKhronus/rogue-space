using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastVictoryMenu : MonoBehaviour {

    public Text coins1Text;
    public Text coins2Text;
    public Text killsText;
    public GameObject window;

    void Start() {

        coins1Text.text = "Total gold collected: " + PlayerPrefs.GetInt("Total Coins");
        coins2Text.text = "Total gold spent: " + PlayerPrefs.GetInt("Total Coins Spent");
        killsText.text = "Enemies destroyed: " + PlayerPrefs.GetInt("Quest4&5");

        CheckColor();

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

    public void Return() {

        SceneManager.LoadScene("Main Menu");

    }

    public void Quit() {

        Application.Quit();
    }

}

