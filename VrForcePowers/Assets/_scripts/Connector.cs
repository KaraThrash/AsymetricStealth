using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    // Start is called before the first frame update

    public ShipPiece myPiece;
    public bool connectedToMainBody, canAttach;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LetGo()
    { }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.GetComponent<Connector>() != null )
    //    { if (other.transform.GetComponent<Connector>().connectedToMainBody == true) { canAttach = true; } }
    //}
    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.GetComponent<Connector>() != null)
    //     { canAttach = false; } 
    //}
}
