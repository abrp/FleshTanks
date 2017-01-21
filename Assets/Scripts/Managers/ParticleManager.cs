using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    public static ParticleManager instance = null;

    //==============================================================================
    // MonoBehaviour
    //==============================================================================

    //==============================================================================
    public void Awake() {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
    }

    //==============================================================================
    public void InstantiateParticleSystem(ParticleSystem particleSystem, Vector3 position){
		if (particleSystem != null) {
			ParticleSystem ps = Instantiate (particleSystem, position, Quaternion.identity) as ParticleSystem;
			AutoDestroy ad = ps.gameObject.AddComponent<AutoDestroy> ();
			ad.time = ps.startLifetime;
		} else {
			Debug.LogError("No Particle System",gameObject);
		}
	}
}
