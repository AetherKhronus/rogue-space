using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float speed = 10.0f;
    
    public GameObject explosion;
    public GameObject player;

    public bool red = false;

    private int damage;

    void Start() {

        damage = PlayerPrefs.GetInt("DMG");
    }

    void Update() {
        
        transform.position = transform.position + Vector3.up * speed * Time.deltaTime;

        if (transform.position.y > 5f) {

            Destroy(gameObject);

        }

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Meteor") {

            if (red) {

                other.GetComponent<MeteorMovement>().TakeHit(damage * 2);
                Destroy(gameObject);

            } else {

                other.GetComponent<MeteorMovement>().TakeHit(damage);
                Destroy(gameObject);

            }

        } else if (other.tag == "Meteor Bullet") {

            Instantiate(explosion , transform.position , transform.rotation);
            other.GetComponent<MeteorBullet>().Destroy();
            Destroy(gameObject);

        } else if (other.tag == "Boss Bullet") {

            Instantiate(explosion , transform.position , transform.rotation);
            other.GetComponent<BossBullet>().Destroy();
            Destroy(gameObject);

        } else if (other.tag == "Enemy") {

            if (red) {

                other.GetComponent<Enemy>().TakeHit(damage * 2);
                Destroy(gameObject);

            } else {

                other.GetComponent<Enemy>().TakeHit(damage);
                Destroy(gameObject);

            }

        } else if (other.tag == "Boss") {

            if (red) {

                other.GetComponent<Boss>().TakeHit(damage * 2);
                Destroy(gameObject);

            } else {

                other.GetComponent<Boss>().TakeHit(damage);
                Destroy(gameObject);

            }

        }
        
    }

}
