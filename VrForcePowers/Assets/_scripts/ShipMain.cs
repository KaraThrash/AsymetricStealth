using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMain : MonoBehaviour
{
    public Vector3 currentVelocity;
    public float timer,speed;
    public List<GameObject> engines,lasers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,transform.position + currentVelocity, speed * 1 * Time.deltaTime);
            timer -= Time.deltaTime;
            Quaternion tempQuat = Quaternion.LookRotation(currentVelocity, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, tempQuat, Time.deltaTime );

        }
        else { currentVelocity = Vector3.zero; }
      
    }
    public void AddPiece(int type,GameObject newpiece)
    {
        if (type == 0) { lasers.Add(newpiece); } else if (type == 1) { engines.Add(newpiece); } else { }
    }
    public void ActivateLasers()
    { foreach (GameObject go in lasers)
        { go.GetComponent<ShipPiece>().FireLaser(); }

    }
    public void ActivateEngines()
    {
        Vector3 tempvec = Vector3.zero;
        foreach (GameObject go in engines)
        { tempvec += go.transform.forward; }
        // transform.position += 
        timer = 1.0f;
            currentVelocity = tempvec;
    }
}
