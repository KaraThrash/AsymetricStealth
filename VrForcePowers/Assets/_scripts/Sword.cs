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
      //  if (blade.transform.childCount > 0)
      //  { foreach (Transform go in blade.transform) { go.GetComponent<Rigidbody>().isKinematic = false; go.transform.parent = null; } }
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (stuckInWall == false && canStick == true && isBlade == true)
        {
          //  if (collision.GetComponent<Rigidbody>() != null)
          //  {
          //      collision.GetComponent<Rigidbody>().isKinematic = true;
          //      collision.transform.root.parent = this.transform;
          //  }
           // else
           // {
                stuckInWall = true;
                canStick = false;
                handle.GetComponent<Rigidbody>().isKinematic = true;
          //  }
        }
    }
}
