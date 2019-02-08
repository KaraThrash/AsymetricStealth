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
        GetComponent<Renderer>().material = colors[0];
        Debug.Log("hover enter"); 
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
    }

    public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
        Debug.Log("press exit");
        GetComponent<Renderer>().material = colors[3];
    }
}
