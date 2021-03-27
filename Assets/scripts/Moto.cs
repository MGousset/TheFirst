using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moto
{
    public string name { get; set; }
    public int speed { get; set; }
    public int maxMotorTorque { get; set; }
    public static int RATATIONSPEED { get; }  = 500;
    public static int MOTOMAXTORQUE { get; }  = 1000;
    public static int COUNTERTORQUE { get; }  = 4000;
    public static int WHEELINGMAXTORQUE { get; }  = 1500;

    public static int STOPWHEELINGMAXTORQUE { get; } = 750;

    public Moto(string motoName)
    {
        this.name = motoName;
        if (motoName == "motoTest")
        {
            speed = 3000;
            maxMotorTorque = 300;
        }
    }
}
