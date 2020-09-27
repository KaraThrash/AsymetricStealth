﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bending : MonoBehaviour
{
    public float watertimer,handtrackingtimer,climbspeed;
    public Material[] colors;
    public GameObject focuspoint, righthand, rightwrist, debugObject, debugObject2, debugtextsitspot;
    public GameObject heldElement;
    public GameObject firePrefab;
    public GameObject windPrefab;
    public GameObject grabbedEarth;
    public GameObject heldfire;
    public Vector3 objectPoint, handPoint,grabpos,playerstartpos; //where the object/hand where in space when the element was grabbed
    public int elementequipped;
    public bool focusOn, canChangeElement;
    public List<GameObject> heldElementGroup;
    public List<Vector3> handhistory;
    public Text debugtext;
    public string elementequippedstring,inputaxis;
    public GameObject visualHand,player;
    public bool onHandHold;
    // Start is called before the first frame update


// HTC_VIU_UnityAxis27
    void Start()
    {
        handhistory.Add(Vector3.zero);
        handhistory.Add(Vector3.zero);
        handhistory.Add(Vector3.zero);
    }


    void Update()
    {
        // Debug.Log("1: " + Input.GetAxis("HTC_VIU_UnityAxis1") + " 2: " + Input.GetAxis("HTC_VIU_UnityAxis2") + " 4: " + Input.GetAxis("HTC_VIU_UnityAxis4") + " 5: " + Input.GetAxis("HTC_VIU_UnityAxis5"));
        //26 left hand 27 right hand isolated from 3rd axis
        // 1 left hort 4 RIGHT hort
        // 2 left vert 5 right vert


        debugtext.transform.position = debugtextsitspot.transform.position;
        debugtext.transform.rotation = debugtextsitspot.transform.rotation;







    if (grabpos != Vector3.zero && Input.GetAxis(inputaxis) != 0)
    {
        player.GetComponent<Rigidbody>().useGravity = false;


        Vector3 targetos = ( visualHand.transform.position - transform.position).normalized * 0.5f;
        player.transform.position = Vector3.MoveTowards(player.transform.position, playerstartpos + targetos, Time.deltaTime * climbspeed );
        //* Vector3.Distance(playerstartpos + targetos, player.transform.position)



    }
    else {
      if (Input.GetAxis(inputaxis) == 0)
      {
          //player.GetComponent<Rigidbody>().useGravity = true;

          if (grabpos != Vector3.zero)
          {
              player.GetComponent<Rigidbody>().useGravity = true;
          }
          grabpos = Vector3.zero; playerstartpos = Vector3.zero;

      }

        if (heldElementGroup.Count > 0)
        {
            HoldingElement();


        }

      if (Input.GetAxis(inputaxis) > 0)
      {
          UsePower();

      }
      else
      {
          NotUsingPower();

      }
    }



    }
    public void OnHandHold(HandHold handhold)
    {
        onHandHold = true; visualHand.transform.rotation = handhold.transform.rotation;
        if (grabpos == Vector3.zero)
        {
            if (Input.GetAxis(inputaxis) != 0)
            {
                grabpos = transform.position;
                playerstartpos = player.transform.position;
                visualHand.transform.position = transform.position;

            }
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.transform.GetComponent<HandHold>() != null)
        {
            OnHandHold(col.transform.GetComponent<HandHold>());

        }
    }
    public void OnTriggerExit(Collider col)
    {
        // if (col.transform.GetComponent<HandHold>() != null)
        // {
        //   onHandHold = false;
        //   grabpos = Vector3.zero;
        //   playerstartpos = player.transform.position;
        //   visualHand.transform.position = Vector3.zero;
        //
        // }
    }

    public void NotUsingPower()
    {
        if (heldElementGroup.Count > 0)
        {
            foreach (GameObject go in heldElementGroup)
            { go.GetComponent<Rigidbody>().useGravity = true; }
            heldElementGroup.Clear();
            heldElement = null;

        }
        handtrackingtimer -= Time.deltaTime;
        if (handtrackingtimer <= 0)
        {
            UpdateHandHistory(transform.position);
            handtrackingtimer = 0.1f;
        }


            if ( Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown("joystick button 5"))
                {
                if (elementequippedstring == "air") { elementequippedstring = "earth"; elementequipped = 1; }
                else if (elementequippedstring == "earth") { elementequippedstring = "fire"; elementequipped = 2; }
                else if (elementequippedstring == "fire") { elementequippedstring = "water"; elementequipped = 3; }
                else { elementequippedstring = "air"; elementequipped = 0; }

                if (colors.Length >= elementequipped) { rightwrist.GetComponent<Renderer>().material = colors[elementequipped]; }
            }





    }

    public void UpdateHandHistory(Vector3 newpos)
    {
        handhistory[0] = handhistory[1];
        handhistory[1] = handhistory[2];
        handhistory[2] = (newpos - handhistory[2]).normalized;
        debugtext.text = newpos.ToString() + "\n" + debugtext.text.Substring(0,20);
        // if (handhistory[2].z > 0.1f && handhistory[2].y > 0.1f && handhistory[2].x > 0.1f) { Instantiate(firePrefab, transform.position, transform.rotation); }
        // if (handhistory[2].x < -0.1f && handhistory[2].y < -0.1f && handhistory[2].x < -0.1f) { Instantiate(windPrefab, transform.position, transform.rotation); }

        // if (handhistory[2].x > 0.1f) { Instantiate(firePrefab, transform.position, transform.rotation); }
        // if (handhistory[2].y > 0.1f) { Instantiate(firePrefab, transform.position, transform.rotation); }
        // if (handhistory[2].z > 0.1f) { Instantiate(firePrefab, transform.position, transform.rotation); }
        // if (handhistory[2].x < -0.1f) { Instantiate(windPrefab, transform.position, transform.rotation); }
        // if (handhistory[2].y < -0.1f) { Instantiate(windPrefab, transform.position, transform.rotation); }
        // if (handhistory[2].z < -0.1f) { Instantiate(windPrefab, transform.position, transform.rotation); }
    }

    public void HoldingElement()
    {
        // heldElement.transform.position = Vector3.MoveTowards(heldElement.transform.position,focuspoint.transform.position,2.0f * Time.deltaTime);
        //debugObject2.transform.position = Vector3.MoveTowards(debugObject2.transform.position, heldElement.transform.position + (righthand.transform.position - handPoint), 2.0f * Time.deltaTime);
        //if (Vector3.Distance(righthand.transform.position, handPoint) > 0.1f)
        //{ heldElement.GetComponent<Rigidbody>().AddForce((righthand.transform.position - handPoint) * 2.0f * Time.deltaTime, ForceMode.Impulse); }
        //else { heldElement.GetComponent<Rigidbody>().velocity = Vector3.zero; }

        foreach (GameObject go in heldElementGroup)
        {
            if (go != null && go.GetComponent<Rigidbody>() == true)
            {


                if (Vector3.Distance(righthand.transform.position, handPoint) > 0.1f)
                {
                  go.GetComponent<Rigidbody>().velocity = (debugObject2.transform.position - go.transform.position).normalized * 3.0f;
                   // go.GetComponent<Rigidbody>().AddForce((righthand.transform.position - handPoint).normalized * 3.0f * Time.deltaTime, ForceMode.Impulse);

                 }
                else
                {
                    // if (Vector3.Distance(heldElementGroup[0].transform.position, go.transform.position) > 1.0f)
                    // {
                    //     go.GetComponent<Rigidbody>().velocity = (heldElementGroup[0].transform.position - go.transform.position) * Vector3.Distance(heldElementGroup[0].transform.position, go.transform.position);
                    // }
                    // else
                    // {//if close float around main object like an atom
                    //     go.GetComponent<Rigidbody>().velocity = (heldElementGroup[0].transform.position - go.transform.position).normalized * 3.0f;
                    // }

                }
            }
        }
    }

    public void UsePower()
    {
        Debug.Log("27 right hand");
        // focuspoint = righthand;
        if (heldElementGroup.Count <= 0)
        { rayCheck(); }
        else
        {
            debugObject2.transform.position = Vector3.MoveTowards(debugObject2.transform.position, objectPoint + (transform.position - handPoint), 2.0f * Time.deltaTime);

            if (Input.GetAxis(inputaxis) > 0)
            {
                CollectElement(elementequippedstring);
            }
            else if (Input.GetAxis("HTC_VIU_UnityAxis5") < 0)
            {
                if (canChangeElement == true && heldElementGroup.Count > 1)
                {
                    heldElementGroup[heldElementGroup.Count - 1].GetComponent<Rigidbody>().useGravity = true;
                    heldElementGroup[heldElementGroup.Count - 1].GetComponent<Rigidbody>().AddForce(Vector3.up * 2.0f * Time.deltaTime, ForceMode.Impulse);
                    heldElementGroup.RemoveAt(heldElementGroup.Count - 1);
                    canChangeElement = false;
                }
                if (canChangeElement == true && elementequipped == 2)
                {
                    FireSize(-1);
                    canChangeElement = false;
                }
            }
            else { canChangeElement = true; }

            //if (Input.GetAxis("HTC_VIU_UnityAxis5") >= 0.5f || Input.GetAxis("HTC_VIU_UnityAxis5") <= -0.5f) { handPoint = righthand.transform.position; }

        }
    }


    public void FireSize(int growOrShrink)
    {
        heldElementGroup[0].transform.localScale = new Vector3(heldElementGroup[0].transform.localScale.x + (0.1f * growOrShrink), heldElementGroup[0].transform.localScale.y + (0.1f * growOrShrink), heldElementGroup[0].transform.localScale.z + (0.1f * growOrShrink));
    }

    public void CollectElement(string elementtype)
    {
        RaycastHit[] hit = Physics.SphereCastAll(heldElementGroup[0].transform.position, 35.0f, -Vector3.up);
        Debug.Log("Grab more elementtype: " + hit.Length);
        foreach (RaycastHit go in hit)
        {
            if (go.transform.tag == elementtype)
            {
                if (heldElementGroup.Contains(go.transform.gameObject) == false)
                {
                    heldElementGroup.Add(go.transform.gameObject);
                    go.transform.GetComponent<Rigidbody>().useGravity = false;
                    go.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.0f, ForceMode.Impulse); }

            }
        }


    }
    void rayCheck()
    {
        RaycastHit hit;

        //Physics.Raycast(focuspoint.transform.position, rightwrist.transform.forward, out hit, 1000f);
        // Then you could find your GO with a specific tag by doing something like:

if(Physics.Raycast(focuspoint.transform.position, rightwrist.transform.forward, out hit, 1000f) ){
        if (hit.transform.tag == elementequippedstring)
        {
            Debug.Log("Hit ELement");
            heldElementGroup.Add(hit.transform.gameObject);
            if (heldElement == null)
            {
                debugObject.transform.position = hit.point;
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z);
                heldElement = hit.transform.gameObject;



                objectPoint = hit.point;
                handPoint = transform.position;
            }


        }
        else
        {
            if (hit.transform.GetComponent<Rigidbody>() != null && hit.transform.tag == "toy")
            {
                //  hit.transform.GetComponent<Rigidbody>().AddForce(rightwrist.transform.position - hit.transform.position * Time.deltaTime * 1.0f,ForceMode.Impulse);
                //hit.transform.GetComponent<Rigidbody>().velocity = (rightwrist.transform.position - (hit.transform.position + transform.up)).normalized * 10;
hit.transform.GetComponent<Rigidbody>().velocity = (hit.transform.position - rightwrist.transform.position).normalized * 5.0f;
                // hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, Time.deltaTime * 5.0f);
            }

            //GameObject go = hit.collider.gameObject;

            //if (go.transform.Find("Fire(Clone)") == null )
            //{
            //    GameObject clone = Instantiate(firePrefab, go.transform.position, go.transform.rotation) as GameObject;
            //    clone.transform.parent = go.transform;
            //    //GameObject.Destroy(clone, 3);
            //}
        }
}
    }

}
