using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Rigidbody2D rgBody;
    public float speed;

    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        rgBody.velocity = transform.up * speed;
    }
}
