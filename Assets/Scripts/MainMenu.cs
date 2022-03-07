using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject ship;

    private int color;

    void Start() {

        color = PlayerPrefs.GetInt("Color"); //red green blue

        if (color == 1) {

            ship.GetComponent<SpriteRenderer>().color = Color.red;

        } else if (color == 2) {

            ship.GetComponent<SpriteRenderer>().color = Color.green;

        } else {

            ship.GetComponent<SpriteRenderer>().color = Color.blue;

        }

    }

    public void StartMissions() {

        SceneManager.LoadScene("Missions");

    }

    public void Upgrades() {

        SceneManager.LoadScene("Upgrades");

    }

    public void Quests() {

        SceneManager.LoadScene("Quests");

    }

    public void Quit() {

        SceneManager.LoadScene("Start");

    }
    
}
