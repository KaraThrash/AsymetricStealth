using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public float timer;
    public double health;
    
    public bool onFire;
    public double fireDOT;

    // Use this for initialization
    void Start () {
        onFire = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (onFire == true)
        {
            fire(fireDOT/60);
        }
        if (transform.Find("Fire(Clone)") == null)
        {
            onFire = false;
            Debug.Log(health);
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {if (collision.transform.GetComponent<Element>() != null)
        { if (collision.transform.GetComponent<Element>().elementType == 1 && collision.transform.GetComponent<Rigidbody>().velocity.magnitude > 1) { loseHealth(1); }  }
        //if (collision.transform.tag == "Water")
        //{
        //    onFire = false;
        //    if (transform.Find("Fire(Clone)") != null)
        //    {
        //        Destroy(transform.Find("Fire(Clone)").gameObject);
        //    }
        //}
    }

    public double getHealth()
    {
        return health;
    }

    public void loseHealth(int i)
    {
        health = health - i;
    }

    public void fire(double damage)
    {
        health = health - damage;
    }

    public bool getOnFire()
    {
        return onFire;
    }

    public void setOnFire(bool fire)
    {
        onFire = fire;
    }

    public void setFireDOT(double damage)
    {
        fireDOT = damage;
    }
}