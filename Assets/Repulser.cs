using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Repulser : MonoBehaviour {
    public float accel = 0;
    
    public float baseAccel = 0;
    public float range = 5;
    public float maxSpeed = 1;
    public float baseRange = 0;
    Vector3 hitPos = Vector3.zero;
    public AnimationCurve powerCurve;
    public LayerMask mask;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        baseAccel = accel;
        baseRange = range;
        rb = transform.parent.GetComponent<Rigidbody>();
    }
	
	// Update is called once per framedww
	void Update () {
        RaycastHit hit;
        //if(rb.GetPointVelocity(transform.position).magnitude < maxSpeed) {

            if (Physics.Raycast(new Ray(transform.position, -transform.up), out hit, range, mask))
            {
                var noise = 1;//1-Mathf.PerlinNoise(Time.timeSinceLevelLoad, transform.GetInstanceID())/50;
                hitPos = hit.point;
                Debug.DrawLine(transform.position, hitPos);
                rb.AddForceAtPosition(-transform.up*accel*-1* powerCurve.Evaluate((1-(hit.distance / range) * noise)), transform.position);
            } else
            {
                hitPos = new Vector3(999999,99999,9999);
            }
        //}
	}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + -transform.up*range);
        Gizmos.DrawSphere(hitPos, 0.5f);
    }
}
