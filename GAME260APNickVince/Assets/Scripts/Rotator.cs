using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    private float angle = 0;
	
	void Update ()
    {
        angle++;
        Vector3 rotEuler = transform.rotation.eulerAngles;
        rotEuler.y = angle;
        Quaternion rot = transform.rotation;
        rot.eulerAngles = rotEuler;
        transform.rotation = rot;
	}
}
