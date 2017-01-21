using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnRail : MonoBehaviour {

    [SerializeField]
    private Transform[] m_railPoints;

    
    private float m_maxDistance = 0.1f;

    [SerializeField]
    private float m_deltaDistance = 0.5f;

    private int m_currentRailPoint = 0;

    // Use this for initialization
    void Start() {
        m_currentRailPoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(m_currentRailPoint < m_railPoints.Length ) {
            Transform next = m_railPoints[m_currentRailPoint];
            float distance = Vector3.Distance(
                    new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),
                    new Vector3(next.transform.position.x,next.transform.position.y,next.transform.position.z)
                );
            Debug.Log("distance = " + distance + " m_currentRailPoint = " + m_currentRailPoint);
            if (distance > m_maxDistance) { 
                this.transform.position = Vector3.MoveTowards(
                        this.transform.position,
                        next.transform.position,
                        m_maxDistance * m_deltaDistance
                );
            }
            else
            {
                m_currentRailPoint++;
            }
        }

    }

}

