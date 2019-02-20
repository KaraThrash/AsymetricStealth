using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipButtons : MonoBehaviour
{
    public int type;
    public ShipMain mainShip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        switch (type)
        {
            case 0:
                mainShip.ActivateLasers();
                break;
              
            case 1:
                mainShip.ActivateEngines();
                break;
            case 2:
                break;
            default:
                break;


        }
    }
}
