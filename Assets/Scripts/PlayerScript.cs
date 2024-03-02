using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator anim;

    public GameObject Road;

    private bool playeruntouchable = false;

    private CapsuleCollider capsuleCollider;
    private float originalHeight;
    private float reducedHeight;

    public Renderer Playerrenderer;
    public Color playerColour;
    public Color newcol;

    int side;
    float slidecd = 0.3f;
    float jumpingcd = 0f;

    public bool ground = true;

    private Rigidbody rb;

    void Start()
    {

        capsuleCollider = GetComponent<CapsuleCollider>();
        originalHeight = capsuleCollider.height;
        reducedHeight = originalHeight / 2;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        side = 1;
        slidecd = 0;
        this.gameObject.transform.position = new Vector3(7.23f, transform.position.y, transform.position.z);
        
    }

    void Update()
    {
        if (side == 1 && (this.gameObject.transform.position != new Vector3(7.23f, transform.position.y, transform.position.z)))
        {
            this.gameObject.transform.position = new Vector3(7.23f, transform.position.y, transform.position.z);
        }

        moveSpeed += 0.05f * Time.deltaTime;
        jumpingcd -= 0.5f * Time.deltaTime;

        slidecd -= 0.7f * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (moveSpeed * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.A))
        {
            if ((side > 0) && (slidecd < 0))
            {
                side--;
                this.gameObject.transform.position = new Vector3(this.transform.position.x - 4.98f, transform.position.y, transform.position.z);
                slidecd = 0.3f;
            }

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if ((side < 2) && (slidecd < 0))
            {
                side++;
                this.gameObject.transform.position = new Vector3(this.transform.position.x + 4.98f, transform.position.y, transform.position.z);
                slidecd = 0.3f;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (ground)
            {
                if (jumpingcd < 0)
                {
                    rb.AddForce(345 * Vector3.up);
                    anim.SetTrigger("jumping");
                    ground = !ground;
                   
                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (ground)
            {
                if (jumpingcd < 0)
                {
                    anim.SetTrigger("sliding");
                    StartCoroutine(SlideCoroutine());
                  
                }
            }
        }

    }

    IEnumerator SlideCoroutine()
    {
        capsuleCollider.height = reducedHeight;
        yield return new WaitForSeconds(0.5f); 
        capsuleCollider.height = originalHeight;
        ground = true;
        jumpingcd = 0.15f;
    }
    IEnumerator LifeBlink()
    {
        ground = true;
        for (int i = 0; i < 5; i++)
        {
           Playerrenderer.material.color = newcol;
            yield return new WaitForSeconds(0.3f);
            Playerrenderer.material.color = playerColour; 
            yield return new WaitForSeconds(0.3f);
        }
        playeruntouchable = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            ground = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            ground = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            if (playeruntouchable == true)
            {
                ground = true;
                return;
                
            }
            
            else
            {

                Debug.Log(other.gameObject.name);
                ScoreCounter.Life--;
                playeruntouchable = true;
                StartCoroutine(LifeBlink());
                
            }
        }
        else if (other.gameObject.tag=="trigger")
        {

            Instantiate(Road, new Vector3(0, 0, this.transform.position.z + 100f),Quaternion.identity);
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            ground = false;
        }
    }
}
