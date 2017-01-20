using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

    public static ParticleManager instance;

	public void InstantiateParticleSystem(ParticleSystem particleSystem, Vector3 position){
		if (particleSystem != null) {
			Vector3 ZpositionFixed = new Vector3(position.x,position.y,-position.z);
			ParticleSystem ps = Instantiate (particleSystem, ZpositionFixed, Quaternion.identity) as ParticleSystem;
			AutoDestroy ad = ps.gameObject.AddComponent<AutoDestroy> ();
			ad.time = ps.startLifetime;
		} else {
			Debug.LogError("No Particle System",gameObject);
		}
	}
}
