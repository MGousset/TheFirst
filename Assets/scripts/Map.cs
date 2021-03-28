using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Map : MonoBehaviour
{
    public Text test;
    Moto moto;
    Order order = null;
    float time;
    public int popTime;
    float dist, newDist;
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
        getTouch();
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
        if (this.order)
        {
            this.order.textGameObject.SetActive(false);
        }
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
        
        (StaticClass.raceLonger, StaticClass.raceMoney) = order.getRaceInfo();
        order = null;
        SceneManager.LoadScene("raceScene");
    }

    void getTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (Input.touchCount == 2)
            {
                Touch touch2 = Input.GetTouch(1);
                if (touch2.phase == TouchPhase.Began)
                {
                    dist = (touch2.position - touch.position).magnitude;
                    test.text = dist.ToString();
                }
                
                else if (touch2.phase == TouchPhase.Moved || touch.phase == TouchPhase.Moved)
                {
                    newDist = (touch2.position - touch.position).magnitude;
                    float ratio = newDist / dist;
                    test.text = ratio.ToString();
                    Zoom(ratio);
                    dist = newDist;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Move(touch.deltaPosition);
            }
        }
    }

    public void Zoom(float zoom)
    {
        
        Debug.Log("zoom");
        if (transform.localScale.x < 10 || transform.localScale.x > 0.5)
        {
            Vector3 pos = transform.localPosition;
            if (transform.localScale.x * zoom > 10)
            {
                zoom = 10 / transform.localScale.x;
            }
            else if (transform.localScale.x * zoom < 0.5)
            {
                zoom = 0.5f / transform.localScale.x;
            }
            else
            {
                transform.localScale *= zoom;
                transform.localPosition = pos * zoom;
            }
        }
    }
    public void Move()
    {
        Debug.Log("move");
        Move(new Vector3(5, 5, 0));
    }
    public void Move(Vector3 move)
    {
        Debug.Log("move");
        transform.Translate(move);
    }
}
