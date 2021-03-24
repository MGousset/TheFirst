using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CircleCollider2D frontWheelCollider, backWheelCollider;
    public Rigidbody2D carBody;
    public WheelJoint2D backWheelMotor;
    public FixedJoint2D bodyPosition;

    public Moto moto { get; set; }

    public bool IsInFloor(LayerMask floorMask)
    {
        return (frontWheelCollider.IsTouchingLayers(floorMask) || backWheelCollider.IsTouchingLayers(floorMask));
    }

    public void Wheeling()
    {
        carBody.AddTorque(Moto.WHEELINGMAXTORQUE);
    }

    public void StopWheeling()
    {
        carBody.AddTorque(0);
    }

    public void RotateBack(){
        if (carBody.angularVelocity < 0)
        {
            carBody.AddTorque(Moto.COUNTERTORQUE);
        }
        else if (carBody.angularVelocity > Moto.RATATIONSPEED)
        {
            carBody.AddTorque(0);
        }
        else
        {
            carBody.AddTorque(Moto.MOTOMAXTORQUE);
        }
    }

    public void RotateFront()
    {
        if (carBody.angularVelocity > 0)
        {
            carBody.AddTorque(-Moto.COUNTERTORQUE);
        }
        else if (carBody.angularVelocity < -Moto.RATATIONSPEED)
        {
            carBody.AddTorque(0);
        }
        else
        {
            carBody.AddTorque(-Moto.MOTOMAXTORQUE);
        }
    }
    public void StopRotate()
    {
        carBody.AddTorque(-carBody.angularVelocity/4);
    }

    public void Forward(float torqueMultiplier)
    {
        backWheelMotor.useMotor = true;
        backWheelMotor.motor = new JointMotor2D { motorSpeed = -moto.speed, maxMotorTorque = moto.maxMotorTorque * torqueMultiplier };
    }

    public void Backward(float torqueMultiplier)
    {
        backWheelMotor.motor = new JointMotor2D { motorSpeed = 500, maxMotorTorque = moto.maxMotorTorque * torqueMultiplier };
    }

    public void StopMotor()
    {
        backWheelMotor.useMotor = false;
    }
}
