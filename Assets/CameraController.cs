using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] Transform trackedObject = null;
    // Update is called once per frame
    void Update () {
        transform.position = Vector3.Lerp(transform.position, trackedObject.position, Mathf.Min(1,Time.deltaTime*8));
        transform.rotation = Quaternion.Lerp(transform.rotation,trackedObject.rotation, Mathf.Min(1,Time.deltaTime*8));
	}
}
