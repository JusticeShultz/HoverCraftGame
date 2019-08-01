using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    [SerializeField] Repulser[] backRepulsers;
    [SerializeField] Repulser[] standardRepulsers;
    public float airDragTimerMax = 0;
    float airDragTimer = 1;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        
    }

	// Update is called once per frame
	void Update () {
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.EulerAngles(-Input.GetAxis("Vertical")*2, 0,Input.GetAxis("Horizontal"))*Quaternion.EulerAngles(0,transform.eulerAngles.y *Mathf.Deg2Rad,0), Time.deltaTime);
        //foreach (var item in frontRepulsers)
        //{
        //    item.transform.localRotation = Quaternion.AngleAxis(70 * Input.GetAxis("Horizontal"), Vector3.forward) * Quaternion.AngleAxis(-70 * Input.GetAxis("Vertical"), Vector3.right);
        //    item.range = item.baseRange + Mathf.Abs(Input.GetAxis("Horizontal"))*4 + Mathf.Abs(Input.GetAxis("Vertical")*4);
        //    //item.accel = item.baseAccel * (1 - -Mathf.Max(Input.GetAxis("Vertical"), 0));
        //}
        
        foreach (var item in backRepulsers)
        {
            item.transform.localRotation = Quaternion.AngleAxis(60 * -Input.GetAxis("Horizontal"), Vector3.forward) * Quaternion.AngleAxis(-70 * Input.GetAxis("Vertical"), Vector3.right);
            item.range = item.baseRange + Mathf.Abs(Input.GetAxis("Horizontal"))*8 + Mathf.Abs(Input.GetAxis("Vertical"))*8;
            item.accel = item.baseAccel * (Mathf.Abs(Input.GetAxis("Horizontal"))+ Mathf.Abs(Input.GetAxis("Vertical")))/2;
            //item.accel = item.baseAccel * (1-Mathf.Max(Input.GetAxis("Vertical"),0));
        }
        //if (Input.GetKeyDown(KeyCode.LeftShift)) rb.AddForce(transform.forward * -1000);
    }
    public float stability = 0.3f;
    public float speed = 2.0f;
    void FixedUpdate()
    {
        Vector3 sumOfNormals = Vector3.zero;
        bool hitSomething = false;
        foreach (var item in standardRepulsers)
        {
            sumOfNormals += (item.hitLastFrame) ? (Vector3.zero) : (item.lastNormal);
            if(hitSomething == false) { hitSomething = item.hitLastFrame; }
        }
        if (!hitSomething)
        {
            airDragTimer -= Time.fixedDeltaTime;
            sumOfNormals = Vector3.up;
        } else
        {
            airDragTimer = airDragTimerMax;
        }
        sumOfNormals.Normalize();
        Vector3 drift = Vector3.Project(rb.velocity, transform.right);
        rb.velocity -= drift * Time.deltaTime;
        rb.velocity -= Vector3.Project(rb.velocity, transform.up) * Time.deltaTime * 0.9f * (Mathf.Max(0,airDragTimer)/airDragTimerMax);
        print((Mathf.Max(0, airDragTimer) / airDragTimerMax));
        rb.velocity += -drift.magnitude * transform.forward * Time.deltaTime * 0.5f;

        
              
        Vector3 predictedUp = Quaternion.AngleAxis(
            rb.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
            rb.angularVelocity
        ) * transform.up;
        Vector3 up = sumOfNormals;
        Vector3 torqueVector = Vector3.Cross(predictedUp,  up );
        rb.AddTorque(torqueVector * speed * speed);
    }
}
