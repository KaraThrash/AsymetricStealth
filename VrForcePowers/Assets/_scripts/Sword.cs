using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Sword handle,blade;
    public bool canStick,stuckInWall,isBlade;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Grabbed()
    {
        blade.canStick = true;
        blade.stuckInWall = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (stuckInWall == false && canStick == true && isBlade == true)
        {

            stuckInWall = true;
            canStick = false;
            handle.GetComponent<Rigidbody>().isKinematic = true;

        }
    }
}
