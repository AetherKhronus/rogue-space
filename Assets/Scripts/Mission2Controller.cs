using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2Controller : MonoBehaviour {

    public float spawnRate = 0.65f;
    public float gameTime = 120f; 
    public float stopSpawns = 5f;
    private float animationTime = 5f;

    public Sprite[] players;

    public GameObject player;
    public GameObject enemy;
    public GameObject[] stars;
    public GameObject[] meteors; 

    private bool start = false;
    private bool animating = false;
    private bool instantiated = false;
    private bool goingUp = true;
    private bool goingRight = false;
    private bool goingDown = false;
    private bool paused = false;

    private GameObject clone;

    void Start() {

        CheckPlayer();   

        PlayerPrefs.SetInt("MissionKills" , 0);
        PlayerPrefs.Save();

    }

    void Update() {
        
        if (!paused) {

            MoveBackground();

            if (start && gameTime > stopSpawns) {

                SpawnEnemies();
            }

            if (gameTime <= 0f) {

                animating = true;

                if(!instantiated) { 

                    clone = Instantiate(enemy , new Vector3(5f , -7f , 0) , transform.rotation);
                    instantiated = true;
                    clone.GetComponent<Enemy>().SetAnimating2();

                }

            } else {

                gameTime = gameTime - Time.deltaTime;

            }

            if (animating) {

                Animation();

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

        player.GetComponent<PlayerMovement>().SetMission(2);

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

            
            float x = Random.Range(-8.3f , 8.4f);
            int change = Random.Range(0 , 100);

            Vector3 pos = new Vector3(x , 5.5f , 0);
            Quaternion rot = new Quaternion();

            if (change <= 50) {

                int num = Random.Range(2 , 6);
                Instantiate(meteors[num] , pos , rot);

            } else {

                int num = Random.Range(0 , 2);
                Instantiate(meteors[num] , pos , rot);

            }

        }

    }

    private void Animation() {

        animationTime = animationTime - Time.deltaTime;

        if (goingUp) {

            clone.transform.position = clone.transform.position + Vector3.up * 20 * Time.deltaTime;

            if (clone.transform.position.y > 7f) {

                goingUp = false;
                clone.transform.position = new Vector3(-20f , 2f , 0f);
                clone.transform.rotation = Quaternion.Euler(0 , 0 , -90);
                goingRight = true;

            }

        } else if (goingRight) {

            clone.transform.position = clone.transform.position + Vector3.right * 15 * Time.deltaTime;

            if (clone.transform.position.x > 9f) {

                goingRight = false;
                clone.transform.position = new Vector3(11f , 6f , 0f);
                clone.transform.rotation = Quaternion.Euler(0 , 0 , 135);
                goingDown = true;

            }

        } else if (goingDown) {
            
            clone.transform.position = clone.transform.position + new Vector3(-1 , -1 , 0) * 10 * Time.deltaTime;

        }


        if (animationTime <= 0f) {

            Victory();

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

            PlayerPrefs.SetInt("Quest2Completed" , 1);
        }

        int missionCoins = PlayerPrefs.GetInt("MissionCoins");
        int hadCoins = PlayerPrefs.GetInt("Coins");

        int missionKills =  PlayerPrefs.GetInt("MissionKills");
        int hadKills = PlayerPrefs.GetInt("Quest4&5");

        PlayerPrefs.SetInt("Coins" , missionCoins + hadCoins);
        PlayerPrefs.SetInt("Total Coins" , missionCoins + PlayerPrefs.GetInt("Total Coins"));
        PlayerPrefs.SetInt("Quest4&5" , hadKills + missionKills);
        PlayerPrefs.SetInt("MissionsCompleted" , 2);
        PlayerPrefs.SetInt("LastMissionDone" , 2);
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

