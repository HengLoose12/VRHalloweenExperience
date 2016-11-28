//=========================================
// Description: Purpose of this code is to 
// trigger floating dead bodies to appear
//=========================================

using UnityEngine;
using System.Collections;

public class floating_bodies_trigger : MonoBehaviour {

	// Game objects 
	public GameObject body1; 
	public GameObject body2; 
	public GameObject body3;

	// Audio sources 
	public AudioSource source; 
	public AudioSource source2; 
	public AudioSource scareAudioSource; 

	// Initialize all to false, not in game
	void Start() {
		body1.SetActive (false);
		body2.SetActive (false);
		body3.SetActive (false);
	}

	// Trigger bodies rise to surface 
	void OnTriggerEnter(Collider other) {
		print ("Hit Trigger");
		if (other.CompareTag ("Player")) {
			source.Play (); 
			body1.SetActive (true);
			body2.SetActive (true);
			body3.SetActive (true);
			scareAudioSource.Play(); 
			Invoke ("Sound", 2.5f);
		} 
	}

	// Invoke sound 
	void Sound()
	{
		source2.Play ();
	}
}