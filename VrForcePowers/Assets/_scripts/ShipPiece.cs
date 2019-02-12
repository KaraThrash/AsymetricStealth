using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;

public class ShipPiece : MonoBehaviour, IColliderEventPressEnterHandler
{
    public bool canAttach;
    public GameObject connectionPoint,debugObject;
    public HTC.UnityPlugin.Vive.BasicGrabbable bg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickedUp()
    {
        canAttach = false;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
    public void LetGo()
    {
        if (connectionPoint != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = connectionPoint.transform.parent;
            bg.enabled = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Connection")
        { connectionPoint = other.gameObject; }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Connection")
        { connectionPoint = null; }
    }
    public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        if (bg.enabled == true) { Instantiate(debugObject, transform.position, transform.rotation); }
      
    }

}
