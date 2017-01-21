using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {



    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            Player player = GetComponent<Player>();
            if (player.IsAlive) { 
                other.GetComponent<Player>().Die();
            }
        }
    }
}
