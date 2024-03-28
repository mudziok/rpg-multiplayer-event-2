using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RunningGirl : MonoBehaviour
{
    // Miku przed kamer¹ pobiegnie do przodu, gdy pierwszy raz prze³¹czy siê kamera
    private bool hasBeenTriggered = false;
    private float remainingTime = 1f;
    // Collider kamery, na której prze³¹czenie czekamy
    public FixedCameraSwitcher cameraCollider;
    public int forceMultiplier = 5;
    public Rigidbody rb;
    public ConstantForce cf;
    void Start()
    {
        cameraCollider.OnPrioritySet += MakeGirlRun;
    }

    void MakeGirlRun()
    {
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            rb.maxLinearVelocity = 5f;
            Vector3 force = transform.forward * forceMultiplier;
            rb.AddForce(force, ForceMode.Impulse);
            Debug.Log(rb.velocity);
            // TODO: ona nigdy nie znika lol
        }
    }
}
