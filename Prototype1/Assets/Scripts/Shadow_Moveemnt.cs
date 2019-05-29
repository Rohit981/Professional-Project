using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Moveemnt : MonoBehaviour {
    internal Rigidbody rb;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject ShadowPrefab;

    [SerializeField]
    private float Z_plane_position;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject child = Instantiate(ShadowPrefab, transform.position, transform.rotation);
        child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, Z_plane_position);
        child.transform.SetParent(this.transform);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float x_value = Input.GetAxis("Horizontal_1") * speed;
       

        Vector3 vel = rb.velocity;

        
        vel.x = x_value;
        rb.velocity = vel;

    }
}
