using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

    public float speed = 8f;
    public GameObject explosion;
    public GameObject controller;

    private GameObject player;
    private GameObject boss;
    private Vector3 vec;

    void Start() {

        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        controller = GameObject.FindWithTag("Controller");
        vec = (player.transform.position - transform.position).normalized;
        LookAtPlayer();

    }

    void Update() {
        
        transform.position = transform.position + vec * speed * Time.deltaTime;

        if (transform.position.x > 16 || transform.position.x < -9 || transform.position.y < -6) {

            Destroy(gameObject);
                
        }


    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {

            Instantiate(explosion , transform.position , transform.rotation);
            Destroy(gameObject);
            other.GetComponent<PlayerMovement>().TakeHit();

        } else if (other.tag == "Enemy") {
            
            DestroyEnemy(other);

        }
        
    }

    private void DestroyEnemy(Collider2D other) {

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        other.GetComponent<Enemy>().BossDestroy();
        controller.GetComponent<Mission4Controller>().DestroyEnemy();

        Destroy(gameObject);

    }

    private void LookAtPlayer() {

        var target = player.transform.position - transform.position;
        var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    public void Destroy() {

        Destroy(gameObject);

    }

    public void SetVecAndRot(Vector3 vector , Quaternion rot) {

        vec = vector * 5f;
        transform.rotation = rot;

    }

    public void SetVecAndRotate(Vector3 vector , Quaternion rot, float z) {

        vec = vector;
        transform.rotation = rot;
        transform.Rotate(new Vector3(0 , 0 , z));

    }

    public void ScatterShot() {

        vec = transform.position - boss.transform.position;
        var normal = new Vector3(0, -1 , 0);
        float angle = Mathf.Acos((vec.x * normal.x + vec.y * normal.y) / (Mathf.Sqrt(Mathf.Pow(vec.x , 2) + Mathf.Pow(vec.y , 2)) * Mathf.Sqrt(Mathf.Pow(normal.x , 2) + Mathf.Pow(normal.y , 2))));
        transform.rotation = Quaternion.Euler(0 , 0 , angle);
    }

}
