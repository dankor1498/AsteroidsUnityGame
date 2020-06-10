using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float speed;
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetButton("Fire1"))
        {
            x = 0;
            y = 0;
            z = 1;
        }
        GetComponent<Rigidbody>().velocity = (new Vector3(x, y, z)) * speed;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
