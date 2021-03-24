using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceScript : MonoBehaviour
{
    public GameObject cam, endPanel, timerText;
    public FixedJoystick speedStick;
    public Slider progression;
    public Text expectedTime, realTimeText, moneyText;

    private int raceLonger, raceTime;
    private Moto moto;
    private GameObject player;
    PlayerController playerCtlr;
    private Vector3 offset;
    private Vector2 startPos;
    private int time = 0;
    private float cash;
    private bool finished = false;

    private LayerMask floorMask;

    // Start is called before the first frame update
    void Start()
    {
        floorMask = LayerMask.GetMask("floor");

        moto = StaticClass.moto;
        raceLonger = StaticClass.raceLonger;
        raceTime = StaticClass.raceTime;
        if (moto == null)
        {
            moto = new Moto("motoTest");
            raceLonger = 100;
            raceTime = 10;
        }
        player = Instantiate(Resources.Load(moto.name)) as GameObject;
        playerCtlr = player.GetComponent<PlayerController>();
        playerCtlr.moto = moto;
        startPos = player.transform.position;

        offset = cam.transform.position - player.transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            moveCame();

            if (playerCtlr.IsInFloor(floorMask))
            {
                floorControle();
            }
            else
            {
                airControle();
            }
            checkInput();

            isFinished();
        }
    }

    public void floorControle()
    {
        if (Input.GetKey("q"))
        {
            playerCtlr.Wheeling();
        }
        else
        {
            playerCtlr.StopWheeling();
        }
    }
    void airControle()
    {
        if (Input.GetKey("q"))
        {
            playerCtlr.RotateBack();
        }
        else if (Input.GetKey("d"))
        {
            playerCtlr.RotateFront();
        }
        else
        {
            playerCtlr.StopRotate();
        }
    }

    void moveCame()
    {
        Debug.Log(player.transform.position);
        cam.transform.position = player.transform.position + offset;
    }

    void checkInput()
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

    void isFinished()
    {
        time = (int)Time.realtimeSinceStartup;
        timerText.GetComponent<Text>().text ="Time :" + time + "s";
        float distance = (player.transform.position.x - startPos.x) / raceLonger;
        progression.value = distance;
        if (distance > 1)
        {
            finished = true;
            timerText.SetActive(false);
            expectedTime.text = raceTime.ToString();
            realTimeText.text = time.ToString();
            cash = (5 + (raceTime - time) / 2);
            moneyText.text = "Cash :" + cash;
            endPanel.SetActive(true);
        }
    }

    public void returnToMenu()
    {
        endPanel.SetActive(false);
        SceneManager.LoadScene("mapScene");
    }
}
