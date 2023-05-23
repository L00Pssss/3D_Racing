using System;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car m_car;
    [SerializeField] private AnimationCurve m_brakeCurve;
    [SerializeField] private AnimationCurve m_steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float m_autoBrakeStrength = 0.5f;


    private float wheelSpeed;
    private float maxSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handbrakeAxis;
    private void Update()
    {
        wheelSpeed = m_car.WheelSpeed;
        maxSpeed = m_car.MaxSpeed;

        UpdateAxis();

        UpdateThrottleAndBrake();
        UpdateSteer();
        UpdateAutoBreake();


        //TODO : DEBUG
        if (Input.GetKeyDown(KeyCode.E))
        {
           m_car.UpGear();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_car.DownGear();
        }
        // на хуй я не знаю. Безполезный код. 
        if (Input.GetKeyDown(KeyCode.G))
        {
            m_car.ShiftToNetral();
        }
    }

    private void UpdateThrottleAndBrake()
    {
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            m_car.ThrottleControl = Mathf.Abs(verticalAxis);
            m_car.BrakeContorl = 0;
        }
        else
        {
            m_car.ThrottleControl = 0;
            m_car.BrakeContorl = m_brakeCurve.Evaluate(wheelSpeed / maxSpeed);
        }

        // Gears
        if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            m_car.ShiftToReverseGear();
        }
        if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed < 0.5f)
        {
            m_car.ShifToFirstGear();
        }

    }
    
    private void UpdateSteer()
    {
        m_car.SteerControl = m_steerCurve.Evaluate(wheelSpeed / maxSpeed) * horizontalAxis;
    }


    private void UpdateAutoBreake()
    {
        if (verticalAxis == 0)
        {
            m_car.BrakeContorl = m_brakeCurve.Evaluate(wheelSpeed / m_car.MaxSpeed) * m_autoBrakeStrength;
        }
    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handbrakeAxis = Input.GetAxis("Jump");
    }

    public void Stop()
    {
        Reset();

        m_car.BrakeContorl = 1;
    }

    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handbrakeAxis = 0;

        m_car.ThrottleControl = 0;
        m_car.SteerControl = 0;
        m_car.BrakeContorl = 0;
    }
}
