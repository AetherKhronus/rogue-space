using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public GameObject controller;
    public GameObject missile;
    public GameObject redMissile;
    public GameObject explosion;
    public GameObject pauseMenu;
    public GameObject magnet;
    public GameObject bubble;
    public GameObject dodgeBuff;

    public Sprite[] dodgeVariants; 

    public Text hpText;
    public Text coinsText;
    public Text coinsTextPause;
    public Text powerUpText;
    public Text buffsText;

    public int powerUpType;

    private int dmg;
    private int hp;
    private int maxHp;
    private int sp;
    private int mg;
    private int coins;
    private int mission;
    private int shotCounter;

    private float animationTime = 3f;
    private float fireRate = 0.85f;
    private float powerFireRate = 0.35f;
    private float powerUpTime = 3.5f;
    private float powerUpTimer = 3.5f;

    private bool canMove = false; 
    public bool canShoot = false;
    private bool noDMG = true;
    private bool gamePaused = false;
    private bool hasPowerUp = false;
    public bool animatingMission3 = false;
    public bool animatingMission4Boss = false;

    public bool[] buffs; //[fireRate moreBullet goldWorth dodgeChance every5Bullet]

    void Start() {
        
        dmg = PlayerPrefs.GetInt("DMG");
        hp = PlayerPrefs.GetInt("HP");
        sp = PlayerPrefs.GetInt("SP");
        mg = PlayerPrefs.GetInt("MG");

        PlayerPrefs.SetInt("MissionCoins" , 0);
        PlayerPrefs.Save();
        shotCounter = 0;
        maxHp = hp;

        bubble.SetActive(false);

        hpText.text = hp + "/" + maxHp + " HP";
        coinsText.text = coins + " Gold";
        powerUpText.text = "Power Up: None";
        buffsText.text = "";

        magnet.GetComponent<CircleCollider2D>().radius = mg * 0.5f + 1f;

        transform.position = new Vector3(0 , -7.5f , 0);

        gamePaused = false;

        GetBuffs();
        
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            PauseGame();

        }

        if (canMove) {

            Movement();
            Shooting();

        } else {

            Animation();

        }

        if (hasPowerUp) {

            PowerUp();

        }

    }

    private void PowerUp() {

        powerUpTimer = powerUpTimer - Time.deltaTime;

        if (powerUpTimer > powerUpTime/3 * 2) {

            powerUpText.color = Color.green;

        } else if (powerUpTimer > powerUpTime/3 && powerUpTimer <= powerUpTime/3 * 2) {

            powerUpText.color = Color.yellow;

        } else if (powerUpTimer <= powerUpTime/3) {

            powerUpText.color = Color.red;

        }

        if (powerUpTimer <= 0f) {

            if (powerUpType == 3) {
                
                bubble.SetActive(false);

            }
            
            powerUpText.text = "Power Up: None";
            powerUpText.color = Color.white;
            hasPowerUp = false;

        }

    }

    private void Movement() {

        if (Input.GetKey(KeyCode.W)) {

            var mov = transform.position + Vector3.up * sp * Time.deltaTime;

            if (mov.y > 4.4f) {

                mov.y = 4.4f;

            }

            transform.position = mov;

        }

        if (Input.GetKey(KeyCode.D)) {

            var mov = transform.position + Vector3.right * sp * Time.deltaTime;

            if (mov.x > 8f) {

                mov.x = 8f;

            }

            transform.position = mov;

        }

        if (Input.GetKey(KeyCode.A)) {

            var mov = transform.position + Vector3.left * sp * Time.deltaTime;

            if (mov.x < -8f) {

                mov.x = -8f;

            }

            transform.position = mov;

        }

        if (Input.GetKey(KeyCode.S)) {

            var mov = transform.position + Vector3.down * sp * Time.deltaTime;

            if (mov.y < -4.4f) {

                mov.y = -4.4f;

            }

            transform.position = mov;

        }

    }

    private void GetBuffs() {

        string[] buffsString = PlayerPrefs.GetString("Buffs").Split('_');
        buffs = new bool[buffsString.Length];

        for (int i = 0; i < buffsString.Length; i++) {

            if (buffsString[i] == "0") {

                buffs[i] = false;

            } else {

                buffs[i] = true; 

            }
            
        }

        if (buffs[0]) {

            fireRate = fireRate - 0.2f;

        }

        SetBuffs();
        
    }

    private void SetBuffs() {

        int color = PlayerPrefs.GetInt("Color");

        if (color == 0) {

            dodgeBuff.GetComponent<SpriteRenderer>().sprite = dodgeVariants[0];

        } else if (color == 1) {

            dodgeBuff.GetComponent<SpriteRenderer>().sprite = dodgeVariants[1];

        } else {

            dodgeBuff.GetComponent<SpriteRenderer>().sprite = dodgeVariants[2];

        }

        for (int i = 0; i < buffs.Length; i++) {
            
            if (buffs[i]) {

                if (i == buffs.Length - 1) {

                    buffsText.text = buffsText.text + "X";

                } else {

                    buffsText.text = buffsText.text + "X  ";

                }

            } else {

                if (i == buffs.Length - 1) {

                    buffsText.text = buffsText.text + "-";

                } else {

                    buffsText.text = buffsText.text + "-  ";

                }

            }
        }

    }

    private void Shooting() {

        if (Input.GetKey(KeyCode.Space) && canShoot && !animatingMission3 && !animatingMission4Boss) {

            StartCoroutine(Shoot());

        }

    }

    private IEnumerator Shoot() {

        canShoot = false;

        if (buffs[4]) {

            shotCounter++;    

        }   

        if (hasPowerUp) {

            if (powerUpType == 1) { // Fire Rate

                if (buffs[1]) { // Double Missils

                    if (shotCounter == 5) {
                        
                        DoubleShot(1);
                        yield return new WaitForSeconds(fireRate - powerFireRate);

                    } else {

                        DoubleShot(0);
                        yield return new WaitForSeconds(fireRate - powerFireRate);

                    }
            
                } else {

                    if (shotCounter == 5) {

                        SingleShot(1);
                        yield return new WaitForSeconds(fireRate - powerFireRate);

                    } else {

                        SingleShot(0);
                        yield return new WaitForSeconds(fireRate - powerFireRate);

                    }

                }

            } else  if (powerUpType == 2) { // More Missils

                if (buffs[1]) { // Double Missils

                    if (shotCounter == 5) {

                        QuadrupleShot(1);
                        yield return new WaitForSeconds(fireRate);

                    } else {

                        QuadrupleShot(0);
                        yield return new WaitForSeconds(fireRate);

                    }
            
                } else {

                    if (shotCounter == 5) {

                        TripleShot(1);
                        yield return new WaitForSeconds(fireRate);

                    } else {

                        TripleShot(0);
                        yield return new WaitForSeconds(fireRate);
                        
                    }

                }  

            } else {

                if (buffs[1]) {

                    if (shotCounter == 5) {

                        DoubleShot(1);
                        yield return new WaitForSeconds(fireRate);

                    } else {

                        DoubleShot(0);
                        yield return new WaitForSeconds(fireRate);

                    }
                
                } else {

                    if (shotCounter == 5) {

                        SingleShot(1);
                        yield return new WaitForSeconds(fireRate);

                    } else {

                        SingleShot(0);
                        yield return new WaitForSeconds(fireRate);
                        
                    }

                }

            }

        } else {

            if (buffs[1]) {

                if (shotCounter == 5) {

                    DoubleShot(1);
                    yield return new WaitForSeconds(fireRate);

                } else {

                    DoubleShot(0);
                    yield return new WaitForSeconds(fireRate);
                        
                }
            
            } else {

                if (shotCounter == 5) {

                    SingleShot(1);
                    yield return new WaitForSeconds(fireRate);

                } else {

                    SingleShot(0);
                    yield return new WaitForSeconds(fireRate);
                        
                }

            }

        }

        if (shotCounter == 5) {

            shotCounter = 0;

        }
        
        canShoot = true;

    }

    private void SingleShot(int type) {

        if (type == 1) {

            Instantiate(redMissile , transform.position + new Vector3(0.0f , 0.75f , 0) , transform.rotation);

        } else {

            Instantiate(missile , transform.position + new Vector3(0.0f , 0.75f , 0) , transform.rotation);

        }

    }

    private void DoubleShot(int type) {

        if (type == 1) {

            Instantiate(redMissile , transform.position + new Vector3(0.25f , 0.75f , 0) , transform.rotation);
            Instantiate(redMissile , transform.position + new Vector3(-0.25f , 0.75f , 0) , transform.rotation);

        } else {

            Instantiate(missile , transform.position + new Vector3(0.25f , 0.75f , 0) , transform.rotation);
            Instantiate(missile , transform.position + new Vector3(-0.25f , 0.75f , 0) , transform.rotation);

        }
        

    }

    private void TripleShot(int type) {

        if (type == 1) {

            Instantiate(redMissile , transform.position + new Vector3(0.5f , 0.75f , 0) , transform.rotation);
            Instantiate(redMissile , transform.position + new Vector3(0.0f , 0.75f , 0) , transform.rotation);
            Instantiate(redMissile , transform.position + new Vector3(-0.5f , 0.75f , 0)  , transform.rotation);

        } else {

            Instantiate(missile , transform.position + new Vector3(0.5f , 0.75f , 0) , transform.rotation);
            Instantiate(missile , transform.position + new Vector3(0.0f , 0.75f , 0) , transform.rotation);
            Instantiate(missile , transform.position + new Vector3(-0.5f , 0.75f , 0)  , transform.rotation);

        }

    }

    private void QuadrupleShot(int type) {
        
        if (type == 1) {

            Instantiate(redMissile , transform.position + new Vector3(0.75f , 0.75f , 0) , transform.rotation);
            Instantiate(redMissile , transform.position + new Vector3(0.25f , 0.75f , 0) , transform.rotation);
            Instantiate(redMissile , transform.position + new Vector3(-0.25f , 0.75f , 0)  , transform.rotation);
            Instantiate(redMissile , transform.position + new Vector3(-0.75f , 0.75f , 0)  , transform.rotation);

        } else {

            Instantiate(missile , transform.position + new Vector3(0.75f , 0.75f , 0) , transform.rotation);
            Instantiate(missile , transform.position + new Vector3(0.25f , 0.75f , 0) , transform.rotation);
            Instantiate(missile , transform.position + new Vector3(-0.25f , 0.75f , 0)  , transform.rotation);
            Instantiate(missile , transform.position + new Vector3(-0.75f , 0.75f , 0)  , transform.rotation);

        }

    }

    private void ControllerPause() {

        if (mission == 1) {

            controller.GetComponent<Mission1Controller>().Pause();

        } else if (mission == 2) {

            controller.GetComponent<Mission2Controller>().Pause();

        } else if (mission == 3) {

            controller.GetComponent<Mission3Controller>().Pause();

        } else {

            controller.GetComponent<Mission4Controller>().Pause();
                
        }

    }

    public void PauseGame() {

        if (!gamePaused) {

            gamePaused = !gamePaused;
            pauseMenu.SetActive(true);
            coinsTextPause.text = coins + " Gold";
            Time.timeScale = 0f;
            
            ControllerPause();

        } else {

            gamePaused = !gamePaused;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);

            ControllerPause();

        }
    }

    public void TakeHit() {

        if (hasPowerUp && powerUpType == 3) { // Invincible

            Instantiate(explosion , transform.position , transform.rotation);

        } else {

            hp--;
            noDMG = false;
            hpText.text = hp + "/" + maxHp + " HP";
            Instantiate(explosion , transform.position , transform.rotation);
            
            if (hp <= 0) {

                sp = 0;
                int e = Random.Range(2 , 6);

                for (int i = 0; i < e; i++) {

                    float x = Random.Range(-0.5f , 0.6f);
                    float y = Random.Range(-0.5f , 0.6f);
                    Instantiate(explosion , transform.position + new Vector3(x , y , 0) , transform.rotation);
                
                }

                Invoke("CallDefeat", 2);

            }

        }

    }

    private void CallDefeat() {

        if (mission == 1) {

            controller.GetComponent<Mission1Controller>().Defeat();

        } else if (mission == 2) {

            controller.GetComponent<Mission2Controller>().Defeat();

        } else if (mission == 3) {

            controller.GetComponent<Mission3Controller>().Defeat();

        } else {

            controller.GetComponent<Mission4Controller>().Defeat();
                
        }

    }

    private void Animation() { 

        animationTime = animationTime - Time.deltaTime;

        if (animationTime > 1.18) {

            transform.position = transform.position + new Vector3(0 , Time.deltaTime , 0) * 5;

        } else if (animationTime >= 0) { 

            transform.position = transform.position - new Vector3(0 , Time.deltaTime , 0) * 5;

        } else {

            canMove = true;
            canShoot = true;

            if (mission == 1) {

                controller.GetComponent<Mission1Controller>().StartSpawn();

            } else if (mission == 2) {

                controller.GetComponent<Mission2Controller>().StartSpawn();

            } else if (mission == 3) {

                controller.GetComponent<Mission3Controller>().StartSpawn();

            } else {

                controller.GetComponent<Mission4Controller>().StartSpawn();

            }
            

        }

    }

    public bool GetNoDMG() {

        return noDMG;

    }

    public void AddKill() {

        PlayerPrefs.SetInt("MissionKills" , PlayerPrefs.GetInt("MissionKills") + 1);
        PlayerPrefs.Save();

    }

    public void AddCoin() {

        if (buffs[2]) {

            coins = PlayerPrefs.GetInt("MissionCoins") + 2;
            PlayerPrefs.SetInt("MissionCoins" , coins);
            PlayerPrefs.Save();

        } else {

            coins = PlayerPrefs.GetInt("MissionCoins") + 1;
            PlayerPrefs.SetInt("MissionCoins" , coins);
            PlayerPrefs.Save();

        }
        
        coinsText.text = coins + " Gold";
        
    }

    public void ActivatePowerUp() {

        
        if (hasPowerUp) {

            powerUpTimer = powerUpTime;

        } else {

            hasPowerUp = true;
            powerUpTimer = powerUpTime;
            powerUpType = Random.Range(1 , 4);

            if (powerUpType == 1) {

                powerUpText.text = "Power Up: Rapid Fire";

            } else if (powerUpType == 2) {

                powerUpText.text = "Power Up: More Missils";

            } else {

                bubble.SetActive(true);
                powerUpText.text = "Power Up: Invincible";

            }

        }

    }

    public void SetMission(int m) {

        mission = m;

    }

    public void AnimatingMission3(bool ani) {

        animatingMission3 = ani;

    }

    public void AnimatingMission4Boss(bool ani) {

        animatingMission4Boss = ani;

    }

}
