using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    private Player[] m_Players;
    private float[] m_Distances;
    private Vector3 m_Center;

    private Camera m_Camera;

    private void Start() {
        PlayerManager.instance.onInstantiatePlayerCallBack += UpdatePlayer;
    }

    public void UpdatePlayer() {
        m_Players = FindObjectsOfType<Player>(); ;
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
                m_Distances[i] = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(m_Players[i].transform.position.x,0, m_Players[i].transform.position.z)) * 3;
            }
        }

        float desiredZoom = Mathf.Max(m_Distances);

        m_Center = FindCentroid(m_Players);

        if (desiredZoom != transform.position.y)
        {
            /*mainCamera.transform.position = Vector3.MoveTowards(
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
                new Vector3(desiredPosition.x, desiredPosition.y, mainCamera.transform.position.z),
                distance * 0.03f
                );*/

            this.transform.position = Vector3.MoveTowards(
                this.transform.position, new Vector3(m_Center.x, desiredZoom, m_Center.z), desiredZoom * 0.1f);
        }


    }

    private Vector3 FindCentroid(Player[] targets)
    {

        Vector3 centroid;
        Vector3 minPoint = targets[0].transform.position;
        Vector3 maxPoint = targets[0].transform.position;

        for (int i = 1; i < targets.Length; i++)
        {
            Vector3 pos = targets[i].transform.position;
            if (pos.x < minPoint.x)
                minPoint.x = pos.x;
            if (pos.x > maxPoint.x)
                maxPoint.x = pos.x;
            if (pos.y < minPoint.y)
                minPoint.y = pos.y;
            if (pos.y > maxPoint.y)
                maxPoint.y = pos.y;
            if (pos.z < minPoint.z)
                minPoint.z = pos.z;
            if (pos.z > maxPoint.z)
                maxPoint.z = pos.z;
        }

        centroid = minPoint + 0.5f * (maxPoint - minPoint);

        return centroid;

    }
}
