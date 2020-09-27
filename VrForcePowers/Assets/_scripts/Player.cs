using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bending lefthand, righthand;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckForGravity()
    {
        //if neither hand is holding onto as climbable object then use gravity
        if (righthand.grabpos != Vector3.zero || lefthand.grabpos != Vector3.zero)
        { rb.useGravity = false; }
        else 
        { rb.useGravity = true; }

    }

}
