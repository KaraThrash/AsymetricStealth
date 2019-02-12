using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    public float speed;
    public int bounces, maxBounces;
    public Transform returnSpot;
    public GameObject lastCollision;
        public bool canBounce;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bounces >= maxBounces)
        {
            if (Vector3.Distance(transform.position, returnSpot.position) > 1)
            {
                rb.velocity = (returnSpot.position - transform.position).normalized * speed;

            }
                else {
                    canBounce = false;
                    bounces = 0;
                }
        }
    }
    public void Thrown()
    {
        lastCollision = null;
        bounces = 0;
        canBounce = true;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (canBounce == true )
        {
            if (bounces < maxBounces)
            {
                if (lastCollision != collision.gameObject)
                {
                    lastCollision = collision.gameObject;
                    RaycastHit hit;

                    Physics.Raycast(transform.position, transform.right, out hit, 100f);

                    bounces++;
                    float velMag = rb.velocity.magnitude;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.velocity = (hit.point - transform.position).normalized * velMag * 2;
                  
                }
            }
            else
            {
                if (lastCollision != collision.gameObject)
                {
                    canBounce = false; bounces = 0;
                }
                    float velMag = rb.velocity.magnitude;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.rotation = Quaternion.identity;
                rb.velocity = (returnSpot.position - transform.position) * speed;
            }       
        }
    }
}
