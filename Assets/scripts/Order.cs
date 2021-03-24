using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    private Map mapScript;
    private static int SHOWINGTIME = 15;
    private float time;

    public string comment =  "J'ai faim";
    public float raceLonger { get; set; }
    public float raceMoney { get; set; }
    public GameObject textGameObject;
    public Text text;

    public void Start()
    {
        raceLonger = Random.Range(2, 5);
        raceMoney = Random.Range(2, 5);
        transform.position = new Vector3(Random.Range(100, Screen.width-100), Random.Range(400, Screen.height-300));
        GetComponent<Image>().color = Random.ColorHSV();
        time = SHOWINGTIME;
        text.text = raceLonger.ToString() + "km, " + raceMoney + "$";
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(gameObject);
        }
    }

    public void attachMap(Map map)
    {
        mapScript = map;
        transform.SetParent(map.GetComponent<Transform>());
        transform.localScale = new Vector2(1, 1);
    }

    public (float, float) getRaceInfo()
    {
        return (raceLonger, raceMoney);
    }

    public void refuseRace()
    {
        Destroy(gameObject);
    }
    public void hideText()
    {
        textGameObject.SetActive(false);
    }
    public void ShowRacePanel()
    {
        //raceLonger, raceTime
        mapScript.ShowRacePanel(this);
    }
}
    
