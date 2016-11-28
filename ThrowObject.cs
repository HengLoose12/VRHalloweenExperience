//=========================================
// Description: Purpose of this code is to 
// trigger objects that are being thrown at player 
//=========================================

using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour 
{

    // Game objects (Array of toys)
    public GameObject[] objectsToThrow;

    // variables for throwing
    public Transform objectStart;
    public Vector3 deltaBoatPos = new Vector3(0, 5, 0);
    public float speed = 25;
    public Vector3 angularVelocity;
    public float destroyTime = 5f;

    // use for testing
    public bool debug = false; 

    GameObject[] a;

    void Start()
    {
        ThrowObject g = transform.parent.GetComponent<ThrowObject>();
        if (g != null)
            a = transform.parent.GetComponent<ThrowObject>().objectsToThrow;
    }

    // Begin throwing when player reaches trigger
	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || debug)
        {
            Vector3 boatPos = other.transform.position;
            Vector3 targetPos = boatPos + deltaBoatPos;
            Vector3 objPos = objectStart.position;
            GameObject objectToThrow = a[Random.Range(0, a.Length - 1)];
            GameObject obj = Instantiate(objectToThrow, objPos, transform.rotation, transform.parent) as GameObject;
            Vector3 v = Vector3.Normalize(targetPos - objPos) * speed;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(v, ForceMode.VelocityChange);
            rb.maxAngularVelocity = 1e9f;
            rb.AddTorque(angularVelocity, ForceMode.VelocityChange);
            Destroy(obj, destroyTime);
        }
    }

    void Update()
    {
        // testing purposes
        if (debug && Input.GetKeyDown("a"))
        {
            OnTriggerEnter(GetComponent<Collider>());
        }

    }
}
