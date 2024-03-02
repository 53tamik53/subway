using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardrive : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (moveSpeed * Time.deltaTime));

    }

}
