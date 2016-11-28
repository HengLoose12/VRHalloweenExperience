//=========================================
// Description: Purpose of this code is to 
// trigger ghost rushing forward scene
//=========================================

using UnityEngine;
using System.Collections;

public class ghost_dissapearing : MonoBehaviour 
{

	// Game objects 
	public GameObject ghost1; 
	public GameObject ghost2;
	public GameObject ghost3;

	// Audio sources
	public AudioSource ghostDash;
	public AudioSource intro; 

	void Start() 
	{
		ghost1.SetActive (true);
		ghost2.SetActive (false); 
		ghost3.SetActive (false); 
	}
		
	void OnTriggerEnter(Collider other) 
	{  
		if (other.CompareTag ("Player")) 
		{
			intro.Play(); 
			print ("Detected Light");
			Invoke ("MoveGhost", 1.5f);
			Invoke ("FinalScare", 3f);
		}
	}

	// Move Ghost forward one step  
	void MoveGhost() 
	{
		ghost1.SetActive (false);
		ghost2.SetActive (true);
	}
		
	// Initialize animation, ghost dash towards player 
	void FinalScare() 
	{
		ghost2.SetActive (false); 
		StartCoroutine( HandleIt() );
	}

	// Remove ghost from game
	void Deactivate()
	{
		ghost3.SetActive (false); 
	}
	
	// Pausing Function 
	private IEnumerator HandleIt()
	{
		yield return new WaitForSeconds (5.0f);
		ghost3.SetActive (true); 
		ghostDash.Play ();
		Invoke ("Deactivate", 2f);
	}

} 
