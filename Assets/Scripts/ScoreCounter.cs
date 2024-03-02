using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    public static float point;
    public static float pointplus;
    public static int totalcoincollected;

    [SerializeField]private float maxrecord;
    public TextMeshProUGUI pointtext;

    public TextMeshProUGUI lifetext;
    public static float Life = 3;

    public GameObject panel;
    
    void Start()
    {
        panel.SetActive(true);
       maxrecord= PlayerPrefs.GetFloat("maxrecord");
        totalcoincollected = 0;
        point = 0;
        pointplus = 6f;
        
    }

   
    void Update()
    {
        lifetext.text = Life.ToString("0") + " LIFE\n REMAINING";
        point += Time.deltaTime * pointplus;
        pointplus += Time.deltaTime * 0.025f;
        
        if(point<maxrecord)
        {
            pointtext.text = "SCORE\n" + point.ToString("0");
            pointtext.color = Color.white;
        }
        else if (point>maxrecord)
        {
            pointtext.text = "NEW RECORD\n" + point.ToString("0");
            pointtext.color = Color.blue;

        }

        

        if(Life<=0)
        {
            if(maxrecord<point)
            {
                maxrecord = point;
                PlayerPrefs.SetFloat("maxrecord", maxrecord);
            }
            SceneManager.LoadScene(0);
            Life = 3;

        }
        
        
        
    }
    public void START()
    {
        panel.SetActive(false);
    }
}
