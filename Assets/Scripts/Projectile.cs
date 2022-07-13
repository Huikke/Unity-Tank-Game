using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float time;
    public float radius;
    public float damage;

    private Rigidbody rb;
    private float t;

    void Start()
    {
        t = time;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }


    void Update()
    {
        t -= Time.deltaTime;
        if(t < 0)
        {
            Destroy(gameObject);
        }
    }
}
