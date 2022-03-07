using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission3Controller : MonoBehaviour {

    public float spawnRate = 0.95f;
    public float animationTime = 5f;
    public float difficulty = 1f;
    public float waitForGreys = 30f;

    public Sprite[] players;

    public GameObject player;
    public GameObject enemy;
    public GameObject[] stars;
    public GameObject[] meteors; 

    private bool start = false;
    private bool spawn = true;
    private bool paused = false;

    private int enemies = 3;

    private GameObject clone;

    void Start() {

        CheckPlayer();   

        PlayerPrefs.SetInt("MissionKills" , 0);
        PlayerPrefs.Save();

    }

    void Update() {
        
        if (!paused) {

            MoveBackground();

            if (start) {

                StartCoroutine(SpawnEnemies());

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

        player.GetComponent<PlayerMovement>().SetMission(3);

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

    private IEnumerator SpawnEnemies() {

        if (Random.Range(0.0f , 100.1f) <= spawnRate) {

            
            float x = Random.Range(-8.3f , 8.4f);
            int change = Random.Range(0 , 100);

            Vector3 pos = new Vector3(x , 6.25f , 0);
            Quaternion rot = new Quaternion();

            if (change <= 50) {

                int num = Random.Range(2 , 6);
                GameObject o = Instantiate(meteors[num] , pos , rot);
                yield return new WaitForSeconds(0.1f);
                o.GetComponent<MeteorMovement>().SetDifficulty(difficulty);

            } else {

                int num = Random.Range(0 , 2);
                GameObject o = Instantiate(meteors[num] , pos , rot);
                yield return new WaitForSeconds(0.1f);
                o.GetComponent<MeteorMovement>().SetDifficulty(difficulty);

            }

            if (difficulty < 5f) {

                difficulty = difficulty + 0.1f;

            } else if (spawn) {

                StartCoroutine(SpawnGreys());
                spawn = false;

            }

        }

    }

    private IEnumerator SpawnGreys() {

        yield return new WaitForSeconds(waitForGreys);

        start = false;

        Instantiate(enemy , new Vector3(-6f , 7f , 0f) , Quaternion.Euler(0 , 0 , 180));
        //Instantiate(enemy , new Vector3(-3f , 6.5f , 0f) , Quaternion.Euler(0 , 0 , 180));
        Instantiate(enemy , new Vector3(0f , 6f , 0f) , Quaternion.Euler(0 , 0 , 180));
        //Instantiate(enemy , new Vector3(3f , 6.5f , 0f) , Quaternion.Euler(0 , 0 , 180));
        Instantiate(enemy , new Vector3(6f , 7f , 0f) , Quaternion.Euler(0 , 0 , 180));

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

    public IEnumerator Victory() {

        yield return new WaitForSeconds(10f);

        if (player.GetComponent<PlayerMovement>().GetNoDMG()) {

            PlayerPrefs.SetInt("Quest3Completed" , 1);
        }

        int missionCoins = PlayerPrefs.GetInt("MissionCoins");
        int hadCoins = PlayerPrefs.GetInt("Coins");

        int missionKills =  PlayerPrefs.GetInt("MissionKills");
        int hadKills = PlayerPrefs.GetInt("Quest4&5");

        PlayerPrefs.SetInt("Coins" , missionCoins + hadCoins);
        PlayerPrefs.SetInt("Total Coins" , missionCoins + PlayerPrefs.GetInt("Total Coins"));
        PlayerPrefs.SetInt("Quest4&5" , hadKills + missionKills);
        PlayerPrefs.SetInt("MissionsCompleted" , 3);
        PlayerPrefs.SetInt("LastMissionDone" , 3);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Victory");
        
    }

    public void Quit() {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");

    }

    public void RemoveEnemy() {

        enemies = enemies - 1;

        if (enemies == 0) {

            StartCoroutine(Victory());

        }

    }

    public void Pause() {

        paused = !paused;
        
    }

}

