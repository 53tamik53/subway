using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    public GameObject WhoiSPlayer;
    private void Update()
    {
        this.transform.position = WhoiSPlayer.transform.position - new Vector3(0, 0, +75f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="ground")
        {
            Destroy(other.gameObject);
        }
        
    }
}
