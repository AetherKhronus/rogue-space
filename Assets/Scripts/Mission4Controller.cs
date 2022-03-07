using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4Controller : MonoBehaviour {

    public float spawnRate = 0.95f;
    public float animationTime = 5f;
    public float difficulty = 1f;
    public float waitForGreys = 30f;
    public float bossRateDefault = 0.25f;

    public Sprite[] players;

    public Vector3[] positions;

    public GameObject bossHealth;
    public GameObject player;
    public GameObject enemy;
    public GameObject bossBullet;
    public GameObject boss;
    public GameObject[] stars;
    public GameObject[] meteors; 
    public GameObject[] enemies; 

    private bool start = false;
    private bool paused = false;
    private bool animating1 = false;
    private bool animating2 = false;
    private bool animating3 = false;
    private bool allAlive = true;
    private bool[] inPosition;

    private int enemyCount = 4;
    public int shipsCount = 11;

    private float bossRate;

    void Start() {

        CheckPlayer();   

        PlayerPrefs.SetInt("MissionKills" , 0);
        PlayerPrefs.Save();

        inPosition = new bool[enemies.Length];
        bossRate = bossRateDefault;

        boss.SetActive(false);
        bossHealth.SetActive(false);

    }

    void Update() {
        
        if (!paused) {

            MoveBackground();

            if (start) {

                SpawnGreys();

            } else if (animating1) {

                AnimateLookingAtPlayer();
                Animation1();

            } else if (animating2 && allAlive) {

                AnimateLookingAtPlayer();

                bossRate = bossRate - Time.deltaTime;

                if (bossRate <= 0f) {

                    StartCoroutine(Animation2());
                    bossRate = bossRateDefault;

                }

            } else if (animating3) {

                Animation3();

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

        player.GetComponent<PlayerMovement>().SetMission(4);

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

    private void SpawnGreys() {

        start = false;

        Instantiate(enemy , new Vector3(-4.5f , 7f , 0f) , Quaternion.Euler(0 , 0 , 180));
        Instantiate(enemy , new Vector3(-1.5f , 6f , 0f) , Quaternion.Euler(0 , 0 , 180));
        Instantiate(enemy , new Vector3(1.5f , 7f , 0f) , Quaternion.Euler(0 , 0 , 180));
        Instantiate(enemy , new Vector3(4.5f , 7f , 0f) , Quaternion.Euler(0 , 0 , 180));

    }

    private void Animation1() {

        for (int i = 0; i < enemies.Length; i++) {

            var dis = Vector3.Distance(enemies[i].transform.position, positions[i]);
            float stepSpeed = 5f;

            if (dis > 0.001f) {

                if (stepSpeed <= 0.1f) {

                    float step = 0.1f * Time.deltaTime;
                    enemies[i].transform.position = Vector3.MoveTowards(enemies[i].transform.position, positions[i], step);

                } else {

                    float step = stepSpeed * Time.deltaTime;
                    enemies[i].transform.position = Vector3.MoveTowards(enemies[i].transform.position, positions[i], step);
                    stepSpeed = stepSpeed - 0.1f;

                }
                
            } else {

                inPosition[i] = true;

            }
            
        }

        StartCoroutine(CheckPositions());

    }

    private IEnumerator Animation2() {

        float x = Random.Range(11.0f , 15.6f);
        float y = Random.Range(-1.0f , 4.5f);
        var o = Instantiate(bossBullet , new Vector3(x , y , 0f) , transform.rotation);
        yield return new WaitForSeconds(0.05f);
        o.GetComponent<BossBullet>().SetVecAndRot(Vector3.left , Quaternion.Euler(0 , 0 , 90));

    }

    private void Animation3() {

        var pos = boss.transform.position + Vector3.down * Time.deltaTime;

        if (pos.y <= 4f) {

           boss.GetComponent<Boss>().Animating(false); 
           animating3 = false;
           player.GetComponent<PlayerMovement>().AnimatingMission4Boss(false);

        } else {

            boss.transform.position = pos;

        }

    }

    private IEnumerator ActivateEnemies() {

        yield return new WaitForSeconds(5f);

        foreach (GameObject enemy in enemies) {

            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().SetAnimating4(true);

        }

        animating1 = true;

    }

    private void AnimateLookingAtPlayer() {

        for (int i = 0; i < enemies.Length; i++) {

            enemies[i].GetComponent<Enemy>().LookAtPlayer();
                
        }

    }

    private IEnumerator CheckPositions() {

        var yes = true;

        foreach (bool pos in inPosition) {

            if (!pos) {

                yes = false;

            }

        }

        if (yes) {

            yield return new WaitForSeconds(3f);
            animating1 = false;
            animating2 = true;

        }
    }

    private IEnumerator ActivateBoss() {

        yield return new WaitForSeconds(3f);
        boss.SetActive(true);
        bossHealth.SetActive(true);
        animating3 = true;
        
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

        yield return new WaitForSeconds(5f);

        int missionCoins = PlayerPrefs.GetInt("MissionCoins");
        int hadCoins = PlayerPrefs.GetInt("Coins");

        int missionKills =  PlayerPrefs.GetInt("MissionKills");
        int hadKills = PlayerPrefs.GetInt("Quest4&5");

        PlayerPrefs.SetInt("Coins" , missionCoins + hadCoins);
        PlayerPrefs.SetInt("Total Coins" , missionCoins + PlayerPrefs.GetInt("Total Coins"));
        PlayerPrefs.SetInt("Quest4&5" , hadKills + missionKills);
        PlayerPrefs.SetInt("MissionsCompleted" , 4);
        PlayerPrefs.SetInt("LastMissionDone" , 4);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Last Victory");
        
    }

    public void Quit() {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");

    }

    public void RemoveEnemy() {

        enemyCount = enemyCount - 1;

        if (enemyCount == 0) {

            StartCoroutine(ActivateEnemies());

        }

    }

    public void DestroyEnemy() {

        shipsCount = shipsCount - 1;

        if (shipsCount == 0) {

            allAlive = false;
            animating2 = false;
            player.GetComponent<PlayerMovement>().AnimatingMission4Boss(true);
            StartCoroutine(ActivateBoss());

        }

    }

    public void Pause() {

        paused = !paused;
        
    }

}


