using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    public float watertimer;
    public Material[] colors;
    public GameObject focuspoint,  righthand, rightwrist, debugObject, debugObject2;
    public GameObject heldElement;
    public GameObject firePrefab;
    public GameObject windPrefab;
    public GameObject grabbedEarth;
    public GameObject heldfire;
    public Vector3 objectPoint, handPoint; //where the object/hand where in space when the element was grabbed
    public int elementequipped;
    public bool focusOn, canChangeElement;
    public List<GameObject> heldElementGroup;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("1: " + Input.GetAxis("HTC_VIU_UnityAxis1") + " 2: " + Input.GetAxis("HTC_VIU_UnityAxis2") + " 4: " + Input.GetAxis("HTC_VIU_UnityAxis4") + " 5: " + Input.GetAxis("HTC_VIU_UnityAxis5"));
        //26 left hand 27 right hand isolated from 3rd axis
        // 1 left hort 4 RIGHT hort
        // 2 left vert 5 right vert
        



        if (heldElement != null)
        {
            heldElement.GetComponent<Rigidbody>().useGravity = false;
            // heldElement.transform.position = Vector3.MoveTowards(heldElement.transform.position,focuspoint.transform.position,2.0f * Time.deltaTime);
            debugObject2.transform.position = Vector3.MoveTowards(debugObject2.transform.position, heldElement.transform.position + (righthand.transform.position - handPoint), 2.0f * Time.deltaTime);
            if (Vector3.Distance(righthand.transform.position, handPoint) > 0.1f)
            { heldElement.GetComponent<Rigidbody>().AddForce((righthand.transform.position - handPoint) * 2.0f * Time.deltaTime, ForceMode.Impulse); }
            else { heldElement.GetComponent<Rigidbody>().velocity = Vector3.zero; }


        }

        if (Input.GetAxis("HTC_VIU_UnityAxis27") > 0)
        {
            Debug.Log("27 right hand");
            focuspoint = righthand;
            if (heldElementGroup.Count <= 0)
            { rayCheck(); }
            else {
                    debugObject2.transform.position = Vector3.MoveTowards(debugObject2.transform.position, heldElement.transform.position + (righthand.transform.position - handPoint), 2.0f * Time.deltaTime);

                    if (elementequipped == 3) { CollectWater(); }
                if (Input.GetAxis("HTC_VIU_UnityAxis4") > 0 )
                {
                    if (canChangeElement == true && elementequipped == 1)
                    {
                        GrabMoreEarth();
                        canChangeElement = false;
                    }

                }
                else if (Input.GetAxis("HTC_VIU_UnityAxis4") < 0)
                {
                    if (canChangeElement == true && heldElementGroup.Count > 1)
                    {
                        heldElementGroup[heldElementGroup.Count - 1].GetComponent<Rigidbody>().useGravity = true;
                        heldElementGroup[heldElementGroup.Count - 1].GetComponent<Rigidbody>().AddForce(Vector3.up * 2.0f * Time.deltaTime,ForceMode.Impulse);
                        heldElementGroup.RemoveAt(heldElementGroup.Count - 1);
                        canChangeElement = false;
                    }

                }
                else { canChangeElement = true; }
                if (Input.GetAxis("HTC_VIU_UnityAxis5") >= 0.5f || Input.GetAxis("HTC_VIU_UnityAxis5") <= -0.5f) { handPoint = righthand.transform.position; }
                foreach (GameObject go in heldElementGroup)
                            {
                                if (go.GetComponent<Rigidbody>() == true)
                                {
                                    if (Vector3.Distance(righthand.transform.position, handPoint) > 0.1f )
                                    { go.GetComponent<Rigidbody>().AddForce((righthand.transform.position - handPoint).normalized * 3.0f * Time.deltaTime, ForceMode.Impulse); }
                                    else { go.GetComponent<Rigidbody>().AddForce((debugObject2.transform.position - go.transform.position) * Vector3.Distance(debugObject2.transform.position , go.transform.position)  * Time.deltaTime, ForceMode.Impulse); }
                                }
                            }
                }


        }
        else
        {
            if (heldElementGroup.Count > 0)
            {
                foreach (GameObject go in heldElementGroup)
                { go.GetComponent<Rigidbody>().useGravity = true; }
              

            }
            heldElementGroup.Clear();
            heldElement = null;
            if (Input.GetAxis("HTC_VIU_UnityAxis5") != 0)
            {

                if (canChangeElement == true && Input.GetAxis("HTC_VIU_UnityAxis5") > 0)
                {

                    elementequipped++;
                }
                if (canChangeElement == true && Input.GetAxis("HTC_VIU_UnityAxis5") < 0) { elementequipped--; }
                canChangeElement = false;
                if (elementequipped < 0) { elementequipped = colors.Length - 1; }
                if (elementequipped > colors.Length - 1) { elementequipped = 0; }


                rightwrist.GetComponent<Renderer>().material = colors[elementequipped];
            }
            else { canChangeElement = true; }

        }
    }
    public GameObject GrabMoreEarth()
    {
        RaycastHit[] hit = Physics.SphereCastAll(heldElementGroup[0].transform.position, 35.0f, -Vector3.up);
        Debug.Log("Grab more earth hit: " + hit.Length);
        foreach  (RaycastHit go in hit)
            { if  (go.transform.GetComponent<Element>() != null)
                {
                    if (go.transform.GetComponent<Element>().elementType == 1 && heldElementGroup.Contains(go.transform.gameObject) == false)
                    {
                    heldElementGroup.Add(go.transform.gameObject);
                    go.transform.GetComponent<Rigidbody>().useGravity = false;
                    go.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
                    return go.transform.gameObject;
                    }

                }
         }

        return null;
    }
    public void CollectWater()
    {
        RaycastHit[] hit = Physics.SphereCastAll(heldElementGroup[0].transform.position, 35.0f, -Vector3.up);
        Debug.Log("Grab more water hit: " + hit.Length);
        foreach (RaycastHit go in hit)
        {
            if (go.transform.GetComponent<Element>() != null)
            {
                if (go.transform.GetComponent<Element>().elementType == 3 && heldElementGroup.Contains(go.transform.gameObject) == false)
                { heldElementGroup.Add(go.transform.gameObject); go.transform.GetComponent<Rigidbody>().useGravity = false;  go.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.0f, ForceMode.Impulse); }

            }
        }

    
    }
    void rayCheck()
    {
        RaycastHit hit;

        Physics.Raycast(focuspoint.transform.position, focuspoint.transform.forward, out hit, 1000f);
        // Then you could find your GO with a specific tag by doing something like:
        if (hit.transform.GetComponent<Element>() != null)
        {
            Debug.Log("Hit ELement");
            if (hit.transform.GetComponent<Element>().elementType == elementequipped)
            {
                heldElementGroup.Add(hit.transform.gameObject);

                debugObject.transform.position = hit.point;
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z);
                heldElement = hit.transform.gameObject;



                objectPoint = hit.point;
                handPoint = righthand.transform.position;
                switch (elementequipped)
                {
                    case 0://air
                        break;
                    case 1://earth
                        if (hit.transform.GetComponent<Element>().elementType == 1 && heldElement == null)
                        {
                            heldElement = hit.transform.gameObject;
                            heldElement.transform.position = new Vector3(heldElement.transform.position.x, transform.position.y + 1, heldElement.transform.position.z);
                            heldElement.GetComponent<Rigidbody>().useGravity = false;
                            heldElementGroup.Add(heldElement);
                        }
                        break;
                    case 2://fire
                        if (hit.transform.GetComponent<Element>().elementType == 2)
                        {
                            if (heldfire == null)
                            {
                                heldfire = Instantiate(firePrefab, focuspoint.transform.position, transform.rotation) as GameObject;
                                // heldfire.transform.parent = transform;
                                //GameObject.Destroy(clone, 3);
                            }
                        }

                        //GameObject clone = Instantiate(windPrefab, focuspoint.transform.position, focuspoint.transform.rotation) as GameObject;
                        //clone.GetComponent<Element>().curwindpower = clone.GetComponent<Element>().windpower * -1;
                        focusOn = false;
                        break;
                    case 3://water

                        // CollectWater();
                        break;
                    case 4:

                        break;
                }
            }
        }
        else
        {
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                //  hit.transform.GetComponent<Rigidbody>().AddForce(rightwrist.transform.position - hit.transform.position * Time.deltaTime * 1.0f,ForceMode.Impulse);
                hit.transform.GetComponent<Rigidbody>().velocity = rightwrist.transform.position - hit.transform.position ;
             //   hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, Time.deltaTime * 5.0f);
            }
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

}
