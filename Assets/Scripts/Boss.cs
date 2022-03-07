using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public GameObject explosion;
    public GameObject gold;
    public GameObject powerUp;
    public GameObject bullet;

    public Slider healthBar;

    public int health = 2000;
    public int drops = 25;

    public float speed = 2f;

    private GameObject player;
    private GameObject controller;

    private float switchTimer;

    private int shots = 0;

    private bool animating = true;
    private bool invincible = false;
    private bool stage1 = false;
    private bool stage2 = false;
    private bool goingRight = true;
    private bool canShoot = true;
    private bool normal = true;

    void Start() {
        
        player = GameObject.FindWithTag("Player");
        controller = GameObject.FindWithTag("Controller");

        switchTimer = Random.Range(1f , 6f);

    }

    void Update() {
        
        if (!animating) {

            LookAtPlayer();
            Move();

            if (stage1) {

                if (canShoot) {

                    StartCoroutine(Shoot1());

                }

            } else if (stage2) {

                if (canShoot) {

                    StartCoroutine(Shoot2());

                }
                
            }

        }

    }

    private IEnumerator Shoot1() {

        canShoot = false;

        float waitToShoot = Random.Range(0.5f , 1.6f);

        yield return new WaitForSeconds(waitToShoot);

        Instantiate(bullet , transform.position + Vector3.down , new Quaternion());

        canShoot = true;

    }

    private IEnumerator Shoot2() {

        canShoot = false;

        float waitToShoot = Random.Range(0.25f , 1.1f);

        yield return new WaitForSeconds(waitToShoot);

        if (normal) {
            
            Instantiate(bullet , transform.position + Vector3.down , new Quaternion());

            shots++;
            if (shots == 4) {

                normal = false;
                shots = 0;
            
            }

        } else {

            StartCoroutine(ScatterShot()); 

            normal = true;

        }

        canShoot = true;

    }

    private IEnumerator SetRed() {

        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.25f);

        GetComponent<SpriteRenderer>().color = Color.white;

    }

    private IEnumerator ScatterShot() {

        GameObject b2 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b3 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b4 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b5 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b6 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b7 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b8 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b9 = Instantiate(bullet , transform.position , new Quaternion());
        GameObject b10 = Instantiate(bullet , transform.position , new Quaternion());
        yield return new WaitForSeconds(0.05f);
        var vec = player.transform.position - transform.position;
        b2.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(-8f , -1f , 0)).normalized , transform.rotation , -38.66f);
        b3.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(-6f , -1f , 0)).normalized , transform.rotation , -30.96f);
        b4.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(-4f , -1f , 0)).normalized , transform.rotation , -21.18f);
        b5.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(-2f , -1f , 0)).normalized , transform.rotation , -11.31f);
        b6.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(0f , -1f , 0)).normalized , transform.rotation , 0);
        b7.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(2f , -1f , 0)).normalized , transform.rotation , 11.31f);
        b8.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(4f , -1f , 0)).normalized , transform.rotation , 21.18f);
        b9.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(6f , -1f , 0)).normalized , transform.rotation , 30.96f);
        b10.GetComponent<BossBullet>().SetVecAndRotate((vec + new Vector3(8f , -1f , 0)).normalized , transform.rotation , 38.66f);

    }

    private void LookAtPlayer() {

        var target = player.transform.position - transform.position;
        var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    private void Move() {

        Vector3 pos;

        switchTimer = switchTimer - Time.deltaTime;

        if (switchTimer <= 0f) {

            goingRight = !goingRight;
            switchTimer = Random.Range(1f , 6f);

        }

        if (goingRight) {

            pos = transform.position + Vector3.right * speed * Time.deltaTime;

            if (pos.x > 7.5f) {

                goingRight = !goingRight;

            }

        } else {

            pos = transform.position + Vector3.left * speed * Time.deltaTime;

            if (pos.x < -7.5f) {

                goingRight = !goingRight;
                
            }

        }

        transform.position = pos;

    }

    private IEnumerator Destroy() {

        Instantiate(explosion , transform.position + Vector3.right * 0.5f , transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion , transform.position + Vector3.up * 0.5f , transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion , transform.position + Vector3.left * 0.5f , transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion , transform.position + Vector3.down * 0.5f , transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion , transform.position , transform.rotation);

        for (int i = 0; i < drops; i++) {

            float x = Random.Range(-0.1f , 0.2f);
            float y = Random.Range(-0.1f , 0.2f);
            Instantiate(gold , transform.position + new Vector3(x , y , 0) , transform.rotation);

            yield return new WaitForSeconds(0.05f);
        
        }
        
        controller.GetComponent<Mission4Controller>().Victory();

        Destroy(gameObject);

    }

    public void Animating(bool anim) {

        animating = anim;
        player.GetComponent<PlayerMovement>().AnimatingMission3(false);

    }

    public void TakeHit(int dmg) {

        if (!animating && !invincible) {

            health = health - dmg;
            healthBar.value = health;

            StartCoroutine(SetRed());

            float prob = Random.Range(0.0f , 100.1f);

            if (prob <= 5.0f) {

                Instantiate(powerUp , transform.position , transform.rotation);

            }

            if (health <= 0) {

                invincible = true;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(Destroy());

            } else if (health <= 1000) {

                if (stage1) {

                    stage2 = true;
                    stage1 = false;
                    speed = speed * 2;
                    Instantiate(explosion , transform.position , transform.rotation);
                    Instantiate(explosion , transform.position , transform.rotation);

                }

            } else if (health <= 2000) {

                stage1 = true;

            }

        }

    }

}
