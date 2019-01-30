using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public float watertimer;
    public Material iceColor;
    public GameObject focuspoint;
    public GameObject firePrefab;
    public GameObject windPrefab;
    public GameObject grabbedEarth;
    public GameObject heldfire;
    public int elementequipped;
    public bool focusOn;
    public List<GameObject> nearbywater;
    public GameObject player;
    // Use this for initialization
    void Start()
    {

    }
    public void leftability()
    {
        focusOn = true;
        switch (elementequipped)
        {
            case 1:
                if (grabbedEarth == null)
                { focusOn = true; }
                break;
            case 2:
                GameObject clone = Instantiate(windPrefab, focuspoint.transform.position, focuspoint.transform.rotation) as GameObject;
                clone.GetComponent<Element>().curwindpower = clone.GetComponent<Element>().windpower;
                focusOn = false;
                break;
            case 3:
                focusOn = true;
                break;
            case 4:
                break;
        }


    }
    public void rightability()
    {
        RaycastHit hit;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f);
        // Then you could find your GO with a specific tag by doing something like:
        if (hit.transform.GetComponent<Element>() != null)
        {
            Debug.Log("EARTHHHHH");
            //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z);

            switch (elementequipped)
            {
                case 0:
                    break;
                case 1:
                    if (hit.transform.GetComponent<Element>().elementType == 1 && grabbedEarth == null)
                    { grabbedEarth = hit.transform.gameObject; }
                    break;
                case 2:
                    GameObject clone = Instantiate(windPrefab, focuspoint.transform.position, focuspoint.transform.rotation) as GameObject;
                    clone.GetComponent<Element>().curwindpower = clone.GetComponent<Element>().windpower * -1;
                    focusOn = false;
                    break;
                case 3:
                    break;
                case 4:
                    if (hit.transform.GetComponent<Element>().elementType == 4)
                    {
                        if (heldfire == null)
                        {
                            heldfire = Instantiate(firePrefab, focuspoint.transform.position, transform.rotation) as GameObject;
                            // heldfire.transform.parent = transform;
                            //GameObject.Destroy(clone, 3);
                        }
                    }
                    break;
            }

        }
    }
    void rayCheck()
    {
        RaycastHit hit;

        Physics.Raycast(focuspoint.transform.position,focuspoint.transform.forward, out hit, 1000f);
        // Then you could find your GO with a specific tag by doing something like:
        if (hit.transform.GetComponent<Element>() != null)
        {
            Debug.Log("EARTHHHHH");
            //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z);
            grabbedEarth = hit.transform.gameObject;
            switch (elementequipped)
            {
                case 0:
                    break;
                case 1:
                    if (hit.transform.GetComponent<Element>().elementType == 1 && grabbedEarth == null)
                    { grabbedEarth = hit.transform.gameObject; grabbedEarth.transform.position = new Vector3(grabbedEarth.transform.position.x, transform.position.y + 1, grabbedEarth.transform.position.z); grabbedEarth.GetComponent<Rigidbody>().useGravity = false; }
                    break;
                case 2:
                    //GameObject clone = Instantiate(windPrefab, focuspoint.transform.position, focuspoint.transform.rotation) as GameObject;
                    //clone.GetComponent<Element>().curwindpower = clone.GetComponent<Element>().windpower * -1;
                    focusOn = false;
                    break;
                case 3:
                    break;
                case 4:
                    if (hit.transform.GetComponent<Element>().elementType == 4)
                    {
                        if (heldfire == null)
                        {
                            heldfire = Instantiate(firePrefab, focuspoint.transform.position, transform.rotation) as GameObject;
                            // heldfire.transform.parent = transform;
                            //GameObject.Destroy(clone, 3);
                        }
                    }
                    break;
            }

        }
        else
        {
            //GameObject go = hit.collider.gameObject;

            //if (go.transform.Find("Fire(Clone)") == null )
            //{
            //    GameObject clone = Instantiate(firePrefab, go.transform.position, go.transform.rotation) as GameObject;
            //    clone.transform.parent = go.transform;
            //    //GameObject.Destroy(clone, 3);
            //}
        }


        //if(hit.transform.tag == "Enemy")
        //{
        //    hit.transform.GetComponent<enemy>().setOnFire(true);
        //    hit.transform.GetComponent<enemy>().setFireDOT(1);
        //    Debug.Log(hit.transform.GetComponent<enemy>().getHealth());
        //}
    }

    // Update is called once per frame
    public void focusing()
    {
        if (nearbywater.Count > 0)
        {
            foreach (GameObject child in nearbywater)
            {
                child.GetComponent<Rigidbody>().AddForce((child.transform.position - focuspoint.transform.position) * -125.0f * Time.deltaTime);
            }
        }
        if (heldfire != null) { heldfire.GetComponent<Rigidbody>().AddForce((heldfire.transform.position - focuspoint.transform.position) * -95.0f * Time.deltaTime); }
        if (grabbedEarth != null)
        {

            grabbedEarth.GetComponent<Rigidbody>().AddForce((grabbedEarth.transform.position - focuspoint.transform.position) * -255.0f * Time.deltaTime);
            if (Input.GetMouseButtonDown(1))
            {

                focusOn = false;
                grabbedEarth.GetComponent<Rigidbody>().useGravity = true;
                grabbedEarth.GetComponent<Rigidbody>().AddForce((player.transform.position - focuspoint.transform.position) * -650.0f * Time.deltaTime, ForceMode.Impulse);
                grabbedEarth = null;
            }

        }
        if (Input.GetMouseButton(1))
        {
            if (nearbywater.Count > 0)
            {
                if (watertimer > 0.1f)
                {
                    watertimer = 0;
                    nearbywater[0].transform.tag = "Ice";
                    nearbywater[0].GetComponent<Rigidbody>().isKinematic = true;
                    nearbywater[0].GetComponent<Renderer>().material = iceColor;
                    nearbywater[0].transform.GetChild(0).gameObject.active = true;
                    nearbywater.RemoveAt(0);
                }
                watertimer += Time.deltaTime;
            }
        }


    }

    void Update()
    {
        // Debug.Log("Grab Button set");
        // Debug.Log("Grab Button set");
        if (grabbedEarth != null)
        { grabbedEarth.transform.position = Vector3.MoveTowards(grabbedEarth.transform.position,focuspoint.transform.position,2.0f * Time.deltaTime); }
        if (Input.GetAxis("HTC_VIU_UnityAxis3") != 0)
        {
            Debug.Log("HTC_VIU_UnityAxis3");
        }
        else { grabbedEarth = null; }

        if (Input.GetAxis("HTC_VIU_UnityAxis1") != 0)
        {
            rayCheck();
            Debug.Log("HTC_VIU_UnityAxis1");
        }
        //if (Input.GetAxis("HTC_VIU_UnityAxis4") != 0)
        //{
        //    Debug.Log("HTC_VIU_UnityAxis4");
        //}
        //if (Input.GetAxis("HTC_VIU_UnityAxis3") != 0)
        //{
        //    Debug.Log("HTC_VIU_UnityAxis3");
        //}
        //if (Input.GetAxis("HTC_VIU_UnityAxis2") != 0)
        //{
        //    Debug.Log("HTC_VIU_UnityAxis2");
          
        //}
        //if (Input.GetAxis("HTC_VIU_UnityAxis12") != 0)
        //{
        //    Debug.Log("HTC_VIU_UnityAxis12");
        //}
        //if (Input.GetAxis("HTC_VIU_UnityAxis11") != 0)
        //{
        //    Debug.Log("HTC_VIU_UnityAxis11");
        //}

        //if (Input.GetAxis("Vertical") != 0)
        //{
        //    Debug.Log("vert");
        //}


        //if (Input.GetKeyDown(KeyCode.JoystickButton8))
        //{
        //    Debug.Log("b8");
        //}
        //if (Input.GetKeyDown(KeyCode.JoystickButton7))
        //{
        //    Debug.Log("b7");
        //}
        //if (Input.GetKeyDown(KeyCode.JoystickButton6))
        //{
        //    Debug.Log("b6");
        //}
        //if (Input.GetKeyDown(KeyCode.JoystickButton5))
        //{
        //    Debug.Log("b5");
           
        //}
        //if (Input.GetKeyDown(KeyCode.JoystickButton4))
        //{
        //    Debug.Log("b4");
        //}
        //if (Input.GetKeyDown(KeyCode.JoystickButton3))
        //{
        //    Debug.Log("b3");
        //}
        //if (Input.GetKeyDown(KeyCode.JoystickButton2))
        //{
        //    Debug.Log("b2");

        //}
        //if (Input.GetKeyDown(KeyCode.JoystickButton1))
        //{
        //    Debug.Log("b1");
        //}


        if (focusOn == true)
        { focusing(); }

        if (Input.GetMouseButtonDown(0))
        {
            rayCheck();
            leftability();
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightability();
            if (grabbedEarth != null && elementequipped == 1)
            {
                focusOn = false;
                grabbedEarth.GetComponent<Rigidbody>().useGravity = true;
                grabbedEarth.GetComponent<Rigidbody>().AddForce((player.transform.position - focuspoint.transform.position) * -450.0f, ForceMode.Impulse);
                grabbedEarth = null;
            }
            if (heldfire != null && elementequipped == 4)
            {


                heldfire.GetComponent<Rigidbody>().AddForce((player.transform.position - focuspoint.transform.position) * -50.0f, ForceMode.Impulse);

            }
            if (nearbywater.Count > 0)
            {
                foreach (GameObject child in nearbywater)
                {
                    child.GetComponent<Rigidbody>().useGravity = true;
                }

            }
            heldfire = null;
            nearbywater.Clear();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Input.GetMouseButton(1)) { }
            else
            {
                if (grabbedEarth != null)
                { grabbedEarth.GetComponent<Rigidbody>().useGravity = true; grabbedEarth = null; }
                if (nearbywater.Count > 0)
                {
                    foreach (GameObject child in nearbywater)
                    {
                        child.GetComponent<Rigidbody>().useGravity = true;
                    }

                }
                focusOn = false;
                nearbywater.Clear();
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (Input.GetMouseButton(0)) { }
            else
            {
                if (grabbedEarth != null)
                { grabbedEarth.GetComponent<Rigidbody>().useGravity = true; grabbedEarth = null; }
                if (nearbywater.Count > 0)
                {
                    foreach (GameObject child in nearbywater)
                    {
                        child.GetComponent<Rigidbody>().useGravity = true;
                    }

                }
                focusOn = false;
                nearbywater.Clear();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { elementtypechange(1);  }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { elementtypechange(2);  }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        { elementtypechange(3);  }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        { elementtypechange(4);  }
    }

    public void elementtypechange(int newelement)
    {
        elementequipped = newelement;
    }
    public void OnTriggerStay(Collider other)
    {
        if (focusOn == true && other.transform.GetComponent<Element>() != null)
        {
            if (grabbedEarth == null && other.GetComponent<Element>().elementType == 1 && elementequipped == 1)
            { grabbedEarth = other.gameObject; grabbedEarth.transform.position = new Vector3(grabbedEarth.transform.position.x, transform.position.y + 1, grabbedEarth.transform.position.z); grabbedEarth.GetComponent<Rigidbody>().useGravity = false; }
            if (other.transform.GetComponent<Element>().elementType == 3 && elementequipped == 3)
            {
                if (nearbywater.Contains(other.transform.gameObject) == false)
                {
                    other.GetComponent<Rigidbody>().useGravity = false;
                    nearbywater.Add(other.transform.gameObject);
                }
            }
        }
    }
}