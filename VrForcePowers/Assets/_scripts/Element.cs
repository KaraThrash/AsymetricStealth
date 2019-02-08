using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {
    /* 1 Earth
     * 2 Wind
     * 3 Water
     * 4 Fire
     */
    public int elementType;
    public float fireTimer;
    public GameObject fireprefab;
    public float windpower;
    public float curwindpower;
    // Use this for initialization
    void Start () {
     //   elementType = 4;
	}
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.GetComponent<Element>() != null)
        {
            switch (collision.transform.GetComponent<Element>().elementType)
            {
                case 1: //Earth
                    switch (elementType)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            if (fireTimer <= 0)
                            {
                               // if (collision.transform.Find("Fire(Clone)") == null)
                               // {
                                   // GameObject clone = Instantiate(fireprefab, transform.position, transform.rotation) as GameObject;
                                    //clone.transform.parent = collision.transform;
                                   // fireTimer = 2;
                               // }
                            }
                            break;
                    }
                    break;
                case 2: //Wind
                    switch (elementType)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    break;
                case 3: //Water
                    switch (elementType)
                    {
                        case 1:
                            //Earth gets wet
                            break;
                        case 2:
                            //Wind do nothing?
                            break;
                        case 3:
                            //water get MORE wet
                            break;
                        case 4:
                            //Fire gets extinguished
                            collision.gameObject.active = false;
                            Destroy(this.gameObject);
                            //if (transform.Find("Fire(Clone)") != null)
                            //{
                            //    Destroy(transform.Find("Fire(Clone)").gameObject);
                            //}
                            break;
                    }
                    break;
                case 4: //Fire
                    switch (elementType)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                          //  Destroy(this.gameObject);
                            break;
                        case 4:
                            break;
                    }
                    break;
            }
        }
        else
        {
            if (collision.transform.GetComponent<enemy>() != null && elementType == 4)
            {
                collision.transform.GetComponent<enemy>().setOnFire(true);
                if (collision.transform.Find("Fire(Clone)") == null)
                {
                    GameObject clone = Instantiate(fireprefab, collision.transform.position, collision.transform.rotation) as GameObject;
                    clone.transform.parent = collision.transform;
                }
            }


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Element>() != null)
        {
            switch (collision.transform.GetComponent<Element>().elementType)
            {
                case 1: //Earth
                    switch (elementType)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    break;
                case 2: //Wind
                    switch (elementType)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    break;
                case 3: //Water
                    switch (elementType)
                    {
                        case 1:
                            //Earth gets wet
                            break;
                        case 2:
                            //Wind do nothing?
                            break;
                        case 3:
                            //water get MORE wet
                            break;
                        case 4:
                            //Fire gets extinguished
                            if (transform.Find("Fire(Clone)") != null)
                            {
                                Destroy(transform.Find("Fire(Clone)").gameObject);
                            }
                            break;
                    }
                    break;
                case 4: //Fire
                    switch (elementType)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    break;
            }
        }
        else
        {
            if (elementType == 3)
            {
                if (collision.gameObject.GetComponent<Rigidbody>() != null && collision.transform.name != "RigidBodyFPSController")
                {
                    //gameObject.AddComponent<FixedJoint>();
                   // gameObject.GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
                }
            }
            

        }
    }
        
    


    void leftAction()
    {
        switch (elementType) {
            case 1 :
            case 2 :
            case 3 :
            case 4 : //fireRayCheck();
            default :
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer > 0)
        { fireTimer -= Time.deltaTime; }
     }
    public void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<Rigidbody>() != null && elementType == 2)
        {
            other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position) * curwindpower * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
