//=========================================
// Description: Purpose of this code is to 
// trigger child scare in the open water scene  
//=========================================

using UnityEngine;
using System.Collections;

public class child_trigger : MonoBehaviour 
{

    // Game objects 
    public GameObject monster1; 
    public GameObject monster2; 
    public GameObject boat;

    // Audio sources
	public AudioSource scare1;
	public AudioSource scare2;
	public AudioSource childvoice;

    // Public variables 
    public Transform end1;
    public float speed = 10f;

    // Private variables
    private float startTime, journeyLength;

    Camera playerCamera;
    TriggerState state;
    Vector3 start, end;

    void Start() 
    {
		monster1.SetActive (true);
		monster2.SetActive (false);
        playerCamera = boat.GetComponentInChildren<Camera>();
        state = TriggerState.START;
	}

	// Make non-animated child dissapear 
	void OnTriggerEnter(Collider other) 
    {
		print ("Hit Trigger");
		if (other.CompareTag ("Player")) 
        {
			childvoice.Play (); 
			print ("Detected Player");
			monster1.SetActive (false); 
			Invoke ("Activate", 10f);
		} 
	}

    // Make the non-animated child appear 
	void Activate()
    {
        state = TriggerState.ACTIVE;
        monster2.SetActive(true);
        startTime = Time.time;
        start = monster2.transform.localPosition;
        end = end1.localPosition;
        journeyLength = Vector3.Distance(start, end);
		scare1.Play ();
	}

    // Make animated child zoom into the players screen
    void Zoom()
    {
        state = TriggerState.ZOOM;
        start = monster2.transform.localPosition;
        end = playerCamera.transform.localPosition;
        startTime = Time.time;
        journeyLength = Vector3.Distance(start, end);
		scare2.Play ();
    }

    // Remove child from screen and speed up the boat
    void Deactivate()
    {
		print("deactivated");
        state = TriggerState.INACTIVE;
        monster2.SetActive(false);
        AccelerateBoat();
    }

    // Capture position of the player head movement so the child's 
    // head moves along with player movement
    public float beta = 20;
    void Update()
    {
        if (state == TriggerState.ACTIVE || state == TriggerState.ZOOM)
        {
            if (state == TriggerState.ZOOM) 
                end = playerCamera.transform.localPosition;
            Vector3 pos = monster2.transform.localPosition;
            float amount = Mathf.Exp(-Time.deltaTime * beta);
            monster2.transform.localPosition = amount * pos + (1 - amount) * end;
            if (Time.time - startTime > 2f)
            {
                if (state == TriggerState.ACTIVE) 
                    Wait();
                else 
                    Deactivate();
            }
        } 
        else if (state == TriggerState.WAIT)
        {
            Vector3 monsterPos = monster2.transform.position;
            Vector3 playerPos = playerCamera.transform.position;
            Vector3 dir = (monsterPos - playerPos).normalized;
            Vector3 look = playerCamera.transform.forward;
            if (Vector3.Dot(dir, look) > 0.5) 
            {
                Invoke("Zoom", 2f);
                state = TriggerState.INACTIVE;
            }
        }  
    }

    void Wait()
    {
        state = TriggerState.WAIT;
    }

    // Increase boat speed
    void AccelerateBoat()
    {
        print("accelerating boat");
        boat.GetComponent<CustomSplineInterpolator>().setAccelerationController(0, 10, 0.2f);
    }
}
