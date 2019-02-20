using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
       // GetComponent<Rigidbody>().velocity = (transform.forward - transform.position) * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
      
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) { Destroy(this.gameObject); }
    }
}
