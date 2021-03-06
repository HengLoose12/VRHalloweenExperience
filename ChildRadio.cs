//=========================================
// Description: Purpose of this code is to 
// trigger the final scare scene
//=========================================

using UnityEngine;
using System.Collections;

public enum TriggerState { START, ACTIVE, WAIT, ZOOM, INACTIVE }

public class child_radio : MonoBehaviour 
{

    // Game Objects 
    public GameObject boat;
	public GameObject jumpscare; 

    // Audio Sources
    public AudioSource radio; 
    public AudioClip lets_play; 
	public AudioClip simon_says;
	public AudioSource scare1;

    // Getting distance and angle of child
    public float distToChild = 0.1f;
    public float upAngle = 15f;
    public float forwardAngle = 20f;
    public float beta = 20;

    private float startTime, journeyLength;

	RaycastHit hit; 
	Camera playerCamera;
	TriggerState state;
	Vector3 start, end;

	void Start()
	{
		playerCamera = boat.GetComponentInChildren<Camera> ();
		state = TriggerState.INACTIVE;
		jumpscare.SetActive (false); 
	}

    // Reaches end of river, play the radio
	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("Player")) 
		{
			radio.PlayOneShot(lets_play); 
			radio.PlayOneShot(simon_says);
			state = TriggerState.START;
		}
	}

    float Angle()
    {
        Vector3 direction = playerCamera.transform.forward;
        return Mathf.Acos(Vector3.Dot(direction, Vector3.up)) * Mathf.Rad2Deg;
    }

    // Check if player looked up 
    bool PlayerLookingUp()
    {
        Vector3 direction = playerCamera.transform.forward;
        return Vector3.Dot(direction, Vector3.up) > Mathf.Cos(Mathf.Deg2Rad * upAngle);
    }


    // Check if player looked back down at child 
    bool PlayerLookingAtChild()
    {
        Vector3 direction = playerCamera.transform.forward;
        return Physics.Raycast(playerCamera.transform.position, direction, out hit, 5) &&
               hit.collider.gameObject.tag.Equals("Child");
    }

    // Child follows where player looks
    void UpdateChildPosition()
    {
        Vector3 direction = playerCamera.transform.forward;
        direction.y = 0;
        direction = Vector3.Normalize(direction);
        jumpscare.transform.position = playerCamera.transform.position + direction;
        jumpscare.transform.LookAt(playerCamera.transform);
    }

    // Triggers scare 
    void Activate()
    {
        jumpscare.SetActive(true);
        state = TriggerState.WAIT;
        UpdateChildPosition();
    }

	void Update()
    {
        if (state == TriggerState.START)
		{
			if (PlayerLookingUp())
			{
                Activate();
			}
        } 
        else if (state == TriggerState.WAIT)
        {
            if (Angle() < forwardAngle)
            { 
                UpdateChildPosition();
            }
            if (PlayerLookingAtChild())
            {
                state = TriggerState.INACTIVE;
				scare1.Play ();
                Invoke("FadeToBlack", 2f);
            }
        }
	}

    // Screen fades to black, end the game
    void FadeToBlack()
    {
        ScreenFader f = GetComponent<ScreenFader>();
        f.fadeIn = false;
    }
}

   