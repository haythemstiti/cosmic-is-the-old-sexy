using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;


[System.Serializable]
public class Boundaries
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody2D rigidBody;
    public Boundaries boundaries;
    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire = 0.0f;
    public float fireRate;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (CnInputManager.GetButton("Jump") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }

    }

    // Update is called once per frame
    void FixedUpdate () {
        //float horizontal = Input.GetAxis("Horizontal");
       // float vertical = Input.GetAxis("Vertical");
        float horizontal = CnInputManager.GetAxis("Horizontal");
        float vertical = CnInputManager.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal,vertical);
      //  Vector2 movement = new Vector2(Input.acceleration.x, Input.acceleration.y);
        rigidBody.velocity = movement * speed;
        rigidBody.position = new Vector2(Mathf.Clamp(rigidBody.position.x, boundaries.xMin, boundaries.xMax),
                                         Mathf.Clamp(rigidBody.position.y, boundaries.yMin, boundaries.yMax));


    }
}
