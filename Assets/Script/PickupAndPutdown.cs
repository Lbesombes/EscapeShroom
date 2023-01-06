using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndPutdown : MonoBehaviour
{
    // The object that the player is currently carrying
    private GameObject carriedObject;

    // The distance at which the player can pick up or put down objects
    public float pickupDistance = 1.0f;

    void Update()
    {
        // Check if the player is trying to pick up an object
        if (Input.GetButtonDown("Pick Up"))
        {
            // Check if the player is already carrying an object
            if (carriedObject == null)
            {
                // Check if there is an object within range
                GameObject objectInRange = GetObjectInRange();
                if (objectInRange != null)
                {
                    // Pick up the object
                    carriedObject = objectInRange;
                    carriedObject.GetComponent<Rigidbody>().isKinematic = true;
                    carriedObject.transform.parent = transform;
                    carriedObject.GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                // Put down the object
                carriedObject.GetComponent<Rigidbody>().isKinematic = false;
                carriedObject.transform.parent = null;
                carriedObject.GetComponent<Collider>().enabled = true;
                carriedObject = null;
            }
        }
    }

    void FixedUpdate()
    {
        // Check if the player is not carrying an object
        if (carriedObject == null)
        {
            // Check if there is an object within range
            GameObject objectInRange = GetObjectInRange();
            if (objectInRange != null)
            {
                // Disable the object's rigidbody
                objectInRange.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    // Returns the first object within range, or null if there are no objects within range
    GameObject GetObjectInRange()
    {
        // Get a list of all the colliders within range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupDistance);

        // Go through the colliders and find the first one that is tagged "Pickup"
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Pickup"))
            {
                return collider.gameObject;
            }
        }

        // If no objects are found, return null
        return null;
    }
}
