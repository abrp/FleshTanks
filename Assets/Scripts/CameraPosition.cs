using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    private Player[] m_Players;
    private float[] m_Distances;

    private Camera m_Camera;

    private void Start() {
        PlayerManager.instance.onInstantiatePlayerCallBack += UpdatePlayer;
    }

    public void UpdatePlayer() {
        m_Players = FindObjectsOfType<Player>();
        m_Distances = new float[m_Players.Length];
        Debug.Log("Camera PlayerList Updated");
    }

    public void Update() {
        if (m_Players == null) {
            return;
        }

        if (m_Players.Length > 0) { 
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Distances[i] = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(m_Players[i].transform.position.x,0, m_Players[i].transform.position.z)) * 0.5f;
            }
        }

        float desiredZoom = Mathf.Max(m_Distances);

        if (desiredZoom != transform.position.y)
        {
            /*mainCamera.transform.position = Vector3.MoveTowards(
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
                new Vector3(desiredPosition.x, desiredPosition.y, mainCamera.transform.position.z),
                distance * 0.03f
                );*/

            this.transform.position = Vector3.MoveTowards(
                this.transform.position, new Vector3(0, desiredZoom, 0), desiredZoom * 0.1f);
        }


    }
}
