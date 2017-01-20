using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

    public static ParticleManager instance = null;

    public void Awake() {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

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
