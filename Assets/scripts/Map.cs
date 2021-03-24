using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Map : MonoBehaviour
{
    Moto moto;
    Order order = null;
    float time;
    public int popTime;
    public GameObject lunchRacePanel;
    // Start is called before the first frame update
    void Start()
    {
        moto = new Moto("motoTest");
        time = popTime;
    }

    void Update()
    {
        CountTen();
    }
    void CountTen()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = popTime;
            CreatePop();
        }

    }
    void CreatePop()
    {
        if (Random.Range(0, 10) < 10)
        {
            GameObject popup = Instantiate(Resources.Load("popup")) as GameObject;
            popup.GetComponent<Order>().attachMap(this);
        }
    }

    public void ShowRacePanel(Order order)
    {
        lunchRacePanel.SetActive(true);
        this.order = order;
    }

    public void CancelRace()
    {
        if (order)
        {
            lunchRacePanel.SetActive(false);
            order.hideText();
            order = null;
        }
    }

    public void RefuseRace()
    {
        lunchRacePanel.SetActive(false);
        order.refuseRace();
        order = null;
    }

    public void LunchRace()
    {
        lunchRacePanel.SetActive(false);
        StaticClass.moto = moto;
        
        (StaticClass.raceLonger, StaticClass.raceTime) = order.getRaceInfo();
        order = null;
        SceneManager.LoadScene("raceScene");
    }
}
