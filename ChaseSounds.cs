//=========================================
// Description: Purpose of this code is to 
// trigger music in the final chase scene 
//=========================================

using UnityEngine;
using System.Collections;

public class ChaseSounds : MonoBehaviour 
{

	// Audio sources
	public AudioSource source1;
	public AudioSource source2; 
	public AudioSource source3;
	public AudioSource noise; 

	// Player enters first collider
	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("Player")) 
		{
			source1.Play ();
			noise.Play ();  
			Invoke ("Sound", 2f); 
		}
	}
	
	// Begins first sound
	void Sound ()
	{
		source2.Play ();
		Invoke ("FinalSound", 3f); 
	}

	// Begins second/final sound
	void FinalSound()
	{
		source3.Play (); 
	}
}
		