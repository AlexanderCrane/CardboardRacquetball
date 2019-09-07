using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMover : MonoBehaviour {

    public GameObject paddle;

    // Use this for initialization
    void Start () {
		
	}

    // FixedUpdate is called once per physics update
    void FixedUpdate () {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Camera.main.transform.forward, out hit, 100) && hit.transform.tag == "Plane")
        {
            print("There is something in front of the object!");
            paddle.transform.position = hit.point;
        }
    }
}
