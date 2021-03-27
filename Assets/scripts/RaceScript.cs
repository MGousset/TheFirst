using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceScript : MonoBehaviour
{
    public GameObject cam, endPanel, timerText;
    public FixedJoystick speedStick, dirStick;
    public Slider progression;
    public Text expectedTime, realTimeText, moneyText;

    private float raceLonger, raceTime, raceMoney;
    private Moto moto;
    private GameObject player;
    PlayerController playerCtlr;
    private Vector3 offset;
    private Vector2 startPos;
    private float time;
    private float cash;
    private bool finished = false;
    private int limiteY = 2;

    private LayerMask floorMask;

    // Start is called before the first frame update
    void Start()
    {
        floorMask = LayerMask.GetMask("floor");
        time = 0;
        moto = StaticClass.moto;
        raceLonger = StaticClass.raceLonger;
        raceMoney = StaticClass.raceMoney;

        if (moto == null)
        {
            moto = new Moto("motoTest");
            raceLonger = 1;
            raceTime = 10;
            raceMoney = 5;
        }
        raceTime = raceLonger *20;

        player = Instantiate(Resources.Load(moto.name)) as GameObject;
        playerCtlr = player.GetComponent<PlayerController>();
        playerCtlr.moto = moto;
        startPos = player.transform.position;

        offset = cam.transform.position - player.transform.position;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!finished)
        {
            CheckDistance();
            if (!finished)
            {
                MoveCame();

                if (playerCtlr.IsInFloor(floorMask))
                {
                    FloorControle();
                }
                else
                {
                    AirControle();
                }
                CheckInput();
            }
        }
    }

    public void FloorControle()
    {
        if (Input.GetKey("q"))
        {
            playerCtlr.Wheeling(1);
        }
        else if (dirStick.Horizontal <= 0)
        {
            playerCtlr.Wheeling(- dirStick.Horizontal);
        }
        else
        {
            playerCtlr.StopWheeling();
        }
    }
    void AirControle()
    {
        if (Input.GetKey("d"))
        {
            playerCtlr.RotateFront(1);
        }
        else if (Input.GetKey("q"))
        {
            playerCtlr.RotateBack(1);
        }
        else if (dirStick.Horizontal > 0)
        {
            playerCtlr.RotateFront(dirStick.Horizontal);
        }        else if (dirStick.Horizontal > 0)
        {
            playerCtlr.RotateFront(dirStick.Horizontal);
        }
        else if (dirStick.Horizontal < 0)
        {
            playerCtlr.RotateBack(-dirStick.Horizontal);
        }
        else
        {
            playerCtlr.StopRotate();
        }
    }

    void MoveCame()
    {
        float posX = player.transform.position.x + offset.x;
        float posY = cam.transform.position.y;
        if (cam.transform.position.y + limiteY < player.transform.position.y)
        {
            posY = player.transform.position.y - limiteY;
        }
        
        else if(cam.transform.position.y - offset.y > player.transform.position.y)
        {
            posY = player.transform.position.y + offset.y;
        }

        cam.transform.position = new Vector3(posX, posY, -10);
    }

    void CheckInput()
    {
        float pression = Input.GetAxis("Horizontal");
        if (pression > 0)
        {
            playerCtlr.Forward(pression);
        }
        else if (pression > 0)
        {
            playerCtlr.Backward(pression);
        }
        else if (speedStick.Horizontal > 0)
        {
            playerCtlr.Forward(speedStick.Horizontal);
        }
        else if (speedStick.Horizontal < 0)
        {
            playerCtlr.Backward(-speedStick.Horizontal);
        }

        else
        {
            playerCtlr.StopMotor();
        }
    }

    void CheckDistance()
    {
        time += Time.deltaTime;
        timerText.GetComponent<Text>().text ="Time :" + (int)time + "s";
        float distance = (player.transform.position.x - startPos.x) / (raceLonger*100);
        progression.value = distance;
        if (distance > 1)
        {
            finished = true;
            timerText.SetActive(false);
            expectedTime.text = raceTime.ToString();
            realTimeText.text = time.ToString();
            cash = raceMoney;
            if (time < 0.80 * raceTime)
            {
                cash *= 1.5f;
            }
            moneyText.text = "Cash :" + cash;
            endPanel.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        endPanel.SetActive(false);
        SceneManager.LoadScene("mapScene");
    }
}
