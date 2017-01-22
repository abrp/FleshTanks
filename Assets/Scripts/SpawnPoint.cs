using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public bool m_IsValid = true;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<KillZone>()) {
            m_IsValid = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<KillZone>())
        {
            m_IsValid = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 2);
    }


}
