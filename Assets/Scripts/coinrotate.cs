using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinrotate : MonoBehaviour
{
    public float speed=175f;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0.3f,1f,0.3f)* Time.deltaTime*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            ScoreCounter.totalcoincollected += 1;
            ScoreCounter.point += 2.5f * ((ScoreCounter.pointplus+ScoreCounter.totalcoincollected)*1.25f);
            this.gameObject.SetActive(false);
        }
    }
}
