using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float speed = 2f;

    public int pullStrength = 200;

    private bool collision;

    private GameObject player;

    void Update() {
        
        if (!collision) {

            transform.position = transform.position + Vector3.down * speed * Time.deltaTime;

            if (transform.position.y < -8f) {

                Destroy(gameObject);

            }

        } else {

            var target = player.transform.position - transform.position;
            transform.position = transform.position + target * 10f * Time.deltaTime;

        }
        
    }

    public void Pull(GameObject player) {

        collision = true;
        this.player = player;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player") {

            Destroy(gameObject);

        } 

    }

}
