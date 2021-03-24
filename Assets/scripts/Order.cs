using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    private Map mapScript;
    private static int SHOWINGTIME = 15;
    public string comment =  "J'ai faim";

    public int raceTime { get; set; }
    public int raceLonger { get; set; }
    private float time;
    public GameObject textGameObject;
    public Text text;

    public void Start()
    {
        raceLonger = Random.Range(2, 10);
        raceTime = raceLonger * 3600 / 50;
        transform.position = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        GetComponent<Image>().color = Random.ColorHSV();
        time = SHOWINGTIME;
        text.text = comment;
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
    }

    public (int, int) getRaceInfo()
    {
        return (raceLonger, raceTime);
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
    
