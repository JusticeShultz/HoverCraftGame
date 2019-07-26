using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {
    public float accel = 0;
    public float baseAccel = 0;
    public float range = 5;
    public float maxSpeed = 1;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        baseAccel = accel;
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            rb.AddForceAtPosition(-transform.up * accel * -1, transform.position);
    }
}
