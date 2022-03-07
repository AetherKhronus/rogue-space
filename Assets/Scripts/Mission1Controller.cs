using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission1Controller : MonoBehaviour {

    public float spawnRate = 0.4f;
    public float gameTime = 120f; 
    public float stopSpawns = 5f;

    public Sprite[] players;

    public GameObject player;
    public GameObject[] stars;
    public GameObject[] meteors; 

    private bool start = false;
    private bool paused = false;

    void Start() {

        CheckPlayer();   

        PlayerPrefs.SetInt("MissionKills" , 0);
        PlayerPrefs.SetInt("MissionCoins" , 0);
        PlayerPrefs.Save();

    }

    void Update() {
        
        if (!paused) {

            MoveBackground();

            if (start && gameTime > stopSpawns) {

                SpawnEnemies();
            }

            gameTime = gameTime - Time.deltaTime;

            if (gameTime <= 0f) {

                Victory();

            }

        }

    }

    private void CheckPlayer() {

        var color = PlayerPrefs.GetInt("Color"); //red green blue

        if (color == 1) {

            player.GetComponent<SpriteRenderer>().sprite = players[0];

        } else if (color == 2) {

            player.GetComponent<SpriteRenderer>().sprite = players[1];

        } else {

            player.GetComponent<SpriteRenderer>().sprite = players[2];

        }

        player.GetComponent<PlayerMovement>().SetMission(1);

    }

    private void MoveBackground() {

        foreach (GameObject starsM in stars) {

            var pos = starsM.transform.position + Vector3.down * Time.deltaTime;
            
            if (pos.y < -10) {

                pos = new Vector3(0 , 10 , 0);
            }

            starsM.transform.position = pos;

        }

    }

    private void SpawnEnemies() {

        if (Random.Range(0.0f , 100.1f) <= spawnRate) {

            int num = Random.Range(0 , 2);
            float x = Random.Range(-8.3f , 8.4f);

            Vector3 pos = new Vector3(x , 5.5f , 0);
            Quaternion rot = new Quaternion();

            Instantiate(meteors[num] , pos , rot);
        }
    }

    public void StartSpawn() {

        start = true;
    }

    public void Defeat() {

        int coins = PlayerPrefs.GetInt("MissionCoins");
        int hadCoins = PlayerPrefs.GetInt("Coins");

        PlayerPrefs.SetInt("Coins" , (coins / 10) + hadCoins);
        PlayerPrefs.SetInt("Total Coins" , (coins / 10) + PlayerPrefs.GetInt("Total Coins"));
        PlayerPrefs.SetInt("LastMissionDone" , -1);
        PlayerPrefs.SetInt("MissionCoins" , coins);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Defeat");

    }

    public void Victory() {

        if (player.GetComponent<PlayerMovement>().GetNoDMG()) {

            PlayerPrefs.SetInt("Quest1Completed" , 1);
        }

        int missionCoins = PlayerPrefs.GetInt("MissionCoins");
        int hadCoins = PlayerPrefs.GetInt("Coins");

        int missionKills =  PlayerPrefs.GetInt("MissionKills");
        int hadKills = PlayerPrefs.GetInt("Quest4&5");

        PlayerPrefs.SetInt("Coins" , missionCoins + hadCoins);
        PlayerPrefs.SetInt("Total Coins" , missionCoins + PlayerPrefs.GetInt("Total Coins"));
        PlayerPrefs.SetInt("Quest4&5" , hadKills + missionKills);
        PlayerPrefs.SetInt("MissionsCompleted" , 1);
        PlayerPrefs.SetInt("LastMissionDone" , 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Victory");
        
    }

    public void Quit() {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");

    }

    public void Pause() {

        paused = !paused;
        
    }

}
