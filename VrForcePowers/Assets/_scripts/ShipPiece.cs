using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;

public class ShipPiece : MonoBehaviour, IColliderEventPressEnterHandler
{
    public bool mainHubPiece,canAttach,attached;
    public int type;
    public GameObject mainShip,connectionPoint,debugObject,bullet;
    public List<Connector> myConnections;
    public HTC.UnityPlugin.Vive.BasicGrabbable bg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attached == true && bg.enabled == true) { bg.enabled = false; }
    }
    public void FireLaser()
    {
        GameObject clone = Instantiate(bullet,transform.forward + transform.position,transform.rotation);
        clone.GetComponent<Rigidbody>().velocity = (transform.forward + transform.position) - transform.position * clone.GetComponent<Bullet>().speed;

    }
    public void PickedUp()
    {
        canAttach = true;
        GetComponent<Rigidbody>().isKinematic = false;
      //  transform.parent = null;


    }
    public void LetGo()
    {
        if (connectionPoint != null && canAttach == true && connectionPoint.transform.parent.GetComponent<ShipPiece>().attached == true)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = mainShip.transform;
            attached = true;
            mainShip.GetComponent<ShipMain>().AddPiece(type,this.gameObject);

        }
        canAttach = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Connection"  && canAttach==true)
        { connectionPoint = other.gameObject; }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Connection")
        { connectionPoint = null; }
    }
    public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        //if (debugObject != null)
        //{
        //    if (bg.enabled == false) { Instantiate(debugObject, transform.position, transform.rotation); }
        //}
    }

}
