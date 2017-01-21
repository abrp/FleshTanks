using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunationBox : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed = 50f;

    [SerializeField]
    private float m_ReactivateTime = 3f;

    public bool isActive = true;

	// Use this for initialization
	void Start () {
        isActive = true;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime , Space.World);
    }

    void OnTriggerEnter(Collider col)
    {

        //Debug.Log("Ammonition Box Trigger: " + isActive);
        if(isActive) { 
            GetComponent<Renderer>().enabled = false;
            isActive = false;
            Invoke("Reactivate", m_ReactivateTime);
            // TODO Give Player Ammonition
            /*if (col.GetComponent)
            {
                Debug.Log("Player enter");
                isActive = false;
                enabled = false;
            }*/
        }
    }

    void Reactivate()
    {
        GetComponent<Renderer>().enabled = true;
        isActive = true;
    }
}
