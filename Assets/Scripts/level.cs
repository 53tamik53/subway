using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class level : MonoBehaviour
{

    public GameObject[] prefab;
    public Transform player;

    public float[] spawncooldown;
    private float cd = 0;

    private List<GameObject> objectPool;
    private List<GameObject> activeObjects;
    public float despawnDistance = 30f;
    public float[] spawndistance;

    void Start()
    {
        
        objectPool = new List<GameObject>();
        activeObjects = new List<GameObject>();

        for (int i = 0; i < prefab.Length; i++)
        {
            if (!prefab[i].activeSelf)
            {
                GameObject obj = prefab[i];
                obj.SetActive(false);
                objectPool.Add(obj);
            }
          
           
        }
        objectPool = objectPool.OrderBy(x => Random.value).ToList();
    }
    private void Update()
    {
        
        cd -= Time.deltaTime; 
        if(cd<=0)
        {
            GetObjectFromPool();
            cd = Random.Range(spawncooldown[0], spawncooldown[1]);
        }
        if (activeObjects.Count > 8)
        {
            for (int i = 0; i < activeObjects.Count; i++)
            {
                GameObject obj = activeObjects[i];
               if(obj==null)
                {
                    return;
                }
                if (!obj.activeSelf || Mathf.Abs(obj.transform.position.z - player.position.z) > despawnDistance)
                {
                    Debug.Log("Silindi: " + obj.name);
                    activeObjects.RemoveAt(i);
                    ReturnObjectToPool(obj);
                }
            }
        }
    }

    public void GetObjectFromPool()
    {
        Debug.Log(objectPool.Count);
        int i = Random.Range(0, objectPool.Count);
        GameObject obj = objectPool[i];
           
                obj.SetActive(true);
                obj.transform.position = new Vector3(obj.transform.position.x ,obj.transform.position.y, player.transform.position.z + Random.Range(spawndistance[0], spawndistance[1]));
        objectPool.RemoveAt(i);  
        activeObjects.Add(obj);



    }

    void ReturnObjectToPool(GameObject obj)
    {
      
        obj.SetActive(false);
        objectPool.Add(obj);
    }
}
