using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,0));
        rb.AddForce(new Vector2(0,Input.GetAxis("Vertical") * speed));
    }
}
