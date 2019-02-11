using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPiece : MonoBehaviour
{
    public bool canAttach;
    public GameObject connectionPoint; 
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
}
