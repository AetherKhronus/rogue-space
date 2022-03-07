using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour {

    public GameObject[] items;
    public GameObject explosion;
    public GameObject player;

    public float dropRate = 30f;
    public float speed = 1f;

    public int health;

    public bool big = false;
    public bool medium = false;

    private Vector3 mov;
    
    void Start() {
        
        if (big) {

            health = 7;

        } else if (medium) {

            health = 5;

        } else {

            health = 1;

        }

        int rand = Random.Range(0 , 101);
        float x;
        float y;

        if (rand <= 40) {

            x = Random.Range(-4.0f , 4.1f);
            y = Random.Range(-2.0f , -1.0f);

        } else {

            x = Random.Range(-1.0f , 1.1f);
            y = Random.Range(-1.5f , -1.0f);
        }
        
        mov = new Vector3(x , y , 0);

    }

    void Update() {
        
        var pos = transform.position + mov * speed * Time.deltaTime;

        if (pos.x > 9.3 || pos.x < -9.3 || pos.y < -5.5) {

            Destroy(gameObject);

        }

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {

            Destroy();
            other.GetComponent<PlayerMovement>().TakeHit();

        } 
        
    }

    private void Destroy() {

        Instantiate(explosion , transform.position , transform.rotation);

        if (big) {

            TripleDrop();

        } else if (medium) {

            DoubleDrop();

        } else {

            SingleDrop();

        }
        
        Destroy(gameObject);
    }

    private void SingleDrop() {

        if (Random.Range(0.0f , 100.1f) < dropRate) {               // drops?

            if (Random.Range(0.0f , 100.1f) < 50f) {                // what drops?

                Instantiate(items[0] , transform.position , transform.rotation);

            } else {

                Instantiate(items[1] , transform.position , transform.rotation);

            }

        }

    }

    private void DoubleDrop() {

        for (int i = 0; i < 2; i++) {                               // how many tries for drops?

            if (Random.Range(0.0f , 100.1f) < dropRate) {           // drops?

                float x = Random.Range(-0.2f , 0.3f);
                float y = Random.Range(-0.2f , 0.3f);

                if (Random.Range(0.0f , 100.1f) < 50f) {            // what drops?   

                    Instantiate(items[0] , transform.position + new Vector3(x , y , 0)  , transform.rotation);

                } else {

                    Instantiate(items[1] , transform.position + new Vector3(x , y , 0) , transform.rotation);

                }

            }

        }

    }

    private void TripleDrop() {

        for (int i = 0; i < 3; i++) {                               // how many tries for drops?

            if (Random.Range(0.0f , 100.1f) < dropRate) {           // drops?

                float x = Random.Range(-0.2f , 0.3f);
                float y = Random.Range(-0.2f , 0.3f);

                if (Random.Range(0.0f , 100.1f) < 50f) {            // what drops?   

                    Instantiate(items[0] , transform.position + new Vector3(x , y , 0) , transform.rotation);

                } else {

                    Instantiate(items[1] , transform.position + new Vector3(x , y , 0) , transform.rotation);

                }

            }

        }

    }

    private IEnumerator SetRed() {

        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.25f);

        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void TakeHit(int dmg) {

        health = health - dmg;

        StartCoroutine(SetRed());

        if (health <= 0) {

            GetComponent<SpriteRenderer>().color = Color.red;
            Destroy();
            player.GetComponent<PlayerMovement>().AddKill();

        }

    }

    public void SetDifficulty(float dif) {

        speed = speed * dif;
        health = health * Mathf.RoundToInt(dif);

    }

}
