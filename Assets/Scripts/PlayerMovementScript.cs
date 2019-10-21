using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    public Transform target;

    void Update()
    {
        this.transform.parent = target;
        this.transform.RotateAround(target.position, Vector3.up, 5);
    }
}
