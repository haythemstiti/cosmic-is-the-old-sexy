using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasive : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Boundaries boundaries;

    private float targetManeuver;
    private Rigidbody2D rb;
    private float currentSpeed;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = rb.velocity.y;
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge * -Mathf.Sign(transform.position.x));
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector2(newManeuver,currentSpeed);
        rb.position = new Vector2
            (Mathf.Clamp(rb.position.x, boundaries.xMin, boundaries.xMax),
            Mathf.Clamp(rb.position.y, boundaries.yMin, boundaries.yMax))
            ;
            

    }
}
