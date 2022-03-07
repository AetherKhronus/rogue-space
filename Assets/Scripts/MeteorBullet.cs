using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBullet : MonoBehaviour {

    public float speed = 7f;
    public GameObject explosion;

    private GameObject player;
    private Vector3 vec;

    void Start() {

        player = GameObject.FindWithTag("Player");
        vec = (player.transform.position - transform.position).normalized;
        LookAtPlayer();

    }

    void Update() {
        
        transform.position = transform.position + vec * speed * Time.deltaTime;

        if (transform.position.x > 9 || transform.position.x < -9 || transform.position.y < -6) {

            Destroy(gameObject);
            
        }

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {

            Instantiate(explosion , transform.position , transform.rotation);
            Destroy(gameObject);
            other.GetComponent<PlayerMovement>().TakeHit();

        } 
        
    }

    private void LookAtPlayer() {

        var target = player.transform.position - transform.position;
        var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    public void Destroy() {

        Destroy(gameObject);

    }

}
