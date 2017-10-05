using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    // Use this for initialization
    LineRenderer laser;    
    public Vector3 origin;
    RaycastHit hitInfo;
    public Vector3 hitPoint;
    void Start()
    {
        laser = gameObject.GetComponent<LineRenderer>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            origin = transform.position;
            laser.SetPosition(0, origin);
            laser.SetPosition(1, hitPoint);            
    }
}
