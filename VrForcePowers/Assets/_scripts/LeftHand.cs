using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LeftHand : MonoBehaviour
{

    public GameObject visualHand,player;
    public bool onHandHold;
    public float climbspeed;
    public Vector3 grabpos,playerstartpos;
    public string inputaxis;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (grabpos != Vector3.zero && Input.GetAxis(inputaxis) > 0)
        {
            player.GetComponent<Rigidbody>().useGravity = false;


            Vector3 targetos = ( visualHand.transform.position - transform.position).normalized * 0.5f;
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerstartpos + targetos, Time.deltaTime * climbspeed * Vector3.Distance(playerstartpos + targetos, player.transform.position));



            if (Input.GetAxis(inputaxis) <= 0)
            {
                //player.GetComponent<Rigidbody>().useGravity = true;
                grabpos = Vector3.zero;
                visualHand.transform.position = Vector3.zero;

            }
        }
        else {

            if (grabpos != Vector3.zero)
            {
                player.GetComponent<Rigidbody>().useGravity = true;
            }
            grabpos = Vector3.zero; playerstartpos = Vector3.zero;

        }

    }


    public void OnHandHold(HandHold handhold)
    {
        onHandHold = true; visualHand.transform.rotation = handhold.transform.rotation;
        if (grabpos == Vector3.zero)
        {
            if (Input.GetAxis(inputaxis) > 0)
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
        //if (col.transform.GetComponent<HandHold>() != null)
        //{ onHandHold = false; grabpos = Vector3.zero; }
    }

}
