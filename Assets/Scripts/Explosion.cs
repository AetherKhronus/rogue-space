using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float lifeTime = 5.0f;

    void Update() {

        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0.0f) {

            Destroy(gameObject);

        }
        
    }

}
