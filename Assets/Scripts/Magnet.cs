using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {

    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Coin") {

            player.GetComponent<PlayerMovement>().AddCoin();
            other.GetComponent<Item>().Pull(player);

        } else if (other.tag == "Power Up") {

            player.GetComponent<PlayerMovement>().ActivatePowerUp();
            other.GetComponent<Item>().Pull(player);

        }

    }

}
