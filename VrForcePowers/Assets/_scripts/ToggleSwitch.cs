using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour,      IColliderEventHoverEnterHandler
    , IColliderEventHoverExitHandler
    , IColliderEventPressEnterHandler
    , IColliderEventPressExitHandler
{
    public Material[] colors;

    public GameObject spawnspot,sword,disc;
    public List<GameObject> spawnedObjects;
    private HashSet<ColliderHoverEventData> hovers = new HashSet<ColliderHoverEventData>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        if (Input.GetAxis("HTC_VIU_UnityAxis3") == 0)
        {
            GetComponent<Renderer>().material = colors[0];
            Debug.Log("hover enter");
            GameObject clone = Instantiate(disc, spawnspot.transform.position, spawnspot.transform.rotation);
            // spawnedObjects.Add(clone);
        }
    }

    public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
    {

        GetComponent<Renderer>().material = colors[1];
        Debug.Log("hover exitc");
    }

    public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        Debug.Log("press enter");
        GetComponent<Renderer>().material = colors[2];
        GameObject clone = Instantiate(sword, spawnspot.transform.position, spawnspot.transform.rotation);
       // spawnedObjects.Add(clone);
    }

    public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
        Debug.Log("press exit");
        GetComponent<Renderer>().material = colors[3];
       // foreach (GameObject go in spawnedObjects) { Destroy(go); }
       // spawnedObjects.Clear();
    }
}
