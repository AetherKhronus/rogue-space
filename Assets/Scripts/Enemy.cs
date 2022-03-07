using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject bullet;
    public GameObject gold;
    public GameObject explosion;
    public GameObject controller;
    public GameObject player;

    public float animationSpeed = 10f;
    public float speed = 0.5f;
    public float xBoundary = 8f;
    public float yTopBoundary = 4.5f;
    public float yBotBoundary = 0f;

    private int health = 25;
    private int drops = 10;

    private bool animating = true;
    private bool animatingMission2 = false;
    private bool animatingMission4 = false;
    private bool canShoot = true;
    private bool invincible = false;

    private Vector3 dir;

    void Awake() {
        
        controller = GameObject.FindWithTag("Controller");
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>().AnimatingMission3(true);

        float x = Random.Range(-1f , 1f);
        float y = Random.Range(-1f , 1f);
        dir = new Vector3(x , y , 0);

    }

    void Update() {
        
        if (!animatingMission2 && !animatingMission4) {

            if (animating) {

                Animation();

            } else {

                Move();

                if (canShoot) {

                    StartCoroutine(Shoot());
                
                }

            }

        }

    }

    private void Animation() {
        
        var pos = transform.position + Vector3.down * animationSpeed * Time.deltaTime;
        animationSpeed = animationSpeed - 0.1f;

        if (animationSpeed <= 0f) {

            animating = false;
            player.GetComponent<PlayerMovement>().AnimatingMission3(false);

        }

        transform.position = pos;

    }

    private IEnumerator Shoot() {

        canShoot = false;

        float waitToShoot = Random.Range(0.5f , 3.0f);

        yield return new WaitForSeconds(waitToShoot);

        Instantiate(bullet , transform.position + Vector3.down * 0.5f  , new Quaternion());

        canShoot = true;

    }

    private void Move() {
        
        LookAtPlayer();

        var pos = transform.position + dir * speed * Time.deltaTime;

        if (pos.x <= -xBoundary) {

            dir.x = -dir.x;
            dir.y = dir.y * Random.Range(0.5f , 1.6f);
            pos.x = -xBoundary + 0.05f;

        } else if (pos.x >= xBoundary) {

            dir.x = -dir.x;
            dir.y = dir.y * Random.Range(0.5f , 1.6f);
            pos.x = xBoundary - 0.05f;
        }

        if (pos.y <= yBotBoundary) {

            dir.x = -dir.x * Random.Range(0.5f , 1.6f);
            dir.y = -dir.y;
            pos.y = yBotBoundary + 0.05f;

        } else if (pos.y >= yTopBoundary) {

            dir.x = -dir.x * Random.Range(0.5f , 1.6f);
            dir.y = -dir.y;
            pos.y = yTopBoundary - 0.05f;

        }

        transform.position = pos;

    }

    private IEnumerator SetRed() {

        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.25f);

        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void TakeHit(int dmg) {

        if (!animating && !animatingMission2 && !animatingMission4 && !invincible) {

            health = health - dmg;

            StartCoroutine(SetRed());

            if (health <= 0) {

                invincible = true;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(Destroy());
                player.GetComponent<PlayerMovement>().AddKill();

            }

        }

    }

    private IEnumerator Destroy() {

        Instantiate(explosion , transform.position + Vector3.left * 0.5f , transform.rotation);
        yield return new WaitForSeconds(0.25f);
        Instantiate(explosion , transform.position + Vector3.right * 0.5f , transform.rotation);
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < drops; i++) {

            float x = Random.Range(-0.1f , 0.2f);
            float y = Random.Range(-0.1f , 0.2f);
            Instantiate(gold , transform.position + new Vector3(x , y , 0) , transform.rotation);

            yield return new WaitForSeconds(0.05f);
        
        }
        
        Mission3Controller c = controller.GetComponent<Mission3Controller>();

        if (c == null) {

            controller.GetComponent<Mission4Controller>().RemoveEnemy();

        } else {

            c.RemoveEnemy();

        }

        Destroy(gameObject);
    }

    private IEnumerator Explode() {

        Instantiate(explosion , transform.position + new Vector3(0.2f , 0f , 0f) , transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion , transform.position + new Vector3(0f , 0.1f , 0f) , transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion , transform.position + new Vector3(-0.2f , 0f , 0f) , transform.rotation);
        
    }

    public void SetAnimating2() {

        animatingMission2 = true;

    }

    public void SetAnimating4(bool anim) {
        
        animatingMission4 = anim;

    }

    public void LookAtPlayer() {

        var target = player.transform.position - transform.position;
        var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    public void BossDestroy() {

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(Explode());

    }

}
