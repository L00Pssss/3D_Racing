using System;
using UnityEngine;

[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider m_leftWheelCollider;
    [SerializeField] private WheelCollider m_rightWheelCollider;

    [SerializeField] private Transform m_leftWheelMesh;
    [SerializeField] private Transform m_rightWheelMesh;

    [SerializeField] private bool m_IsMotor;
    [SerializeField] private bool m_IsSteer;

    [SerializeField] private float m_wheelWidth;

    [SerializeField] private float antiRollForce;

    [SerializeField] private float m_additionalWheelDownForce;

    [SerializeField] private float m_baseForwardStiffnes = 1.5f;
    [SerializeField] private float m_stabilityForwardFactor = 1.0f;

    [SerializeField] private float m_baseSidewaysStiffnes = 2.0f;
    [SerializeField] private float m_stabilitySidewaysFactor = 1.0f;

    public bool IsMotor => m_IsMotor;
    public bool IsSteer => m_IsSteer;


    private WheelHit leftWheelHit;
    private WheelHit rightWeelHit;

    // Public API
    public void Update()
    {
        UpdateWheelHits();

        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStiffeness();
         
        SyncMeshTransform();
    }

    public void ConfigureVehicleSubsteps(float speedThreshold, int speedBelowThreshold, int stepsAboveThreshold)
    {
        m_leftWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
        m_rightWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
    }

    private void UpdateWheelHits()
    {
        m_leftWheelCollider.GetGroundHit(out leftWheelHit);
        m_rightWheelCollider.GetGroundHit(out rightWeelHit);
    }

    private void CorrectStiffeness()
    {
        WheelFrictionCurve leftForward = m_leftWheelCollider.forwardFriction;
        WheelFrictionCurve rightForward = m_rightWheelCollider.forwardFriction; 
        
        WheelFrictionCurve leftSideways = m_leftWheelCollider.sidewaysFriction;
        WheelFrictionCurve rightSideways = m_rightWheelCollider.sidewaysFriction;

        leftForward.stiffness = m_baseForwardStiffnes + Mathf.Abs(leftWheelHit.forwardSlip) * m_stabilityForwardFactor;
        rightForward.stiffness = m_baseForwardStiffnes + Mathf.Abs(rightWeelHit.forwardSlip) * m_stabilityForwardFactor;


        leftSideways.stiffness =  m_baseSidewaysStiffnes + Mathf.Abs(leftWheelHit.sidewaysSlip) * m_stabilitySidewaysFactor;
        rightSideways.stiffness = m_baseSidewaysStiffnes + Mathf.Abs(rightWeelHit.sidewaysSlip) * m_stabilitySidewaysFactor;

        m_leftWheelCollider.forwardFriction = leftForward;
        m_rightWheelCollider.forwardFriction = rightForward;

        m_leftWheelCollider.sidewaysFriction = leftSideways;
        m_rightWheelCollider.sidewaysFriction = rightSideways;
    }

    private void ApplyDownForce()
    {
        if (m_leftWheelCollider.isGrounded == true)
        {
            m_leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -m_additionalWheelDownForce *
                m_leftWheelCollider.attachedRigidbody.velocity.magnitude, m_leftWheelCollider.transform.position);
        }

        if (m_rightWheelCollider.isGrounded == true)
        {
            m_rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWeelHit.normal * -m_additionalWheelDownForce *
                m_rightWheelCollider.attachedRigidbody.velocity.magnitude, m_rightWheelCollider.transform.position);
        }
    }

    private void ApplyAntiRoll()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if (m_leftWheelCollider.isGrounded == true)
        {
            travelL = (-m_leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y - m_leftWheelCollider.radius) 
                / m_leftWheelCollider.suspensionDistance;
        }  
        
        if (m_rightWheelCollider.isGrounded == true)
        {
            travelR = (-m_rightWheelCollider.transform.InverseTransformPoint(rightWeelHit.point).y - m_rightWheelCollider.radius) 
                / m_rightWheelCollider.suspensionDistance;
        }

        float forceDirection = (travelL - travelR);

        if (m_leftWheelCollider.isGrounded == true)
        {
            m_leftWheelCollider.attachedRigidbody.AddForceAtPosition(m_leftWheelCollider.transform.up * 
                -forceDirection * antiRollForce, m_leftWheelCollider.transform.position);
        } 

        if (m_rightWheelCollider.isGrounded == true)
        {
            m_rightWheelCollider.attachedRigidbody.AddForceAtPosition(m_rightWheelCollider.transform.up * 
                forceDirection * antiRollForce, m_rightWheelCollider.transform.position);
        }
    }

    public void ApplySteerAngel(float steerAngel, float whellBaseLenght)
    {
        if (m_IsSteer == false) return;

        float radius = Math.Abs(whellBaseLenght * Mathf.Tan(Mathf.Deg2Rad * (90 - Math.Abs(steerAngel))));
        float angelSing = Mathf.Sign(steerAngel);

        if (steerAngel > 0)
        {
            m_leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLenght / (radius + (m_wheelWidth * 0.5f))) * angelSing;
            m_rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLenght / (radius - (m_wheelWidth * 0.5f))) * angelSing;
        }
        else if (steerAngel < 0)
        {
            m_leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLenght / (radius - (m_wheelWidth * 0.5f))) * angelSing;
            m_rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLenght / (radius + (m_wheelWidth * 0.5f))) * angelSing;
        }
        else
        {
            m_leftWheelCollider.steerAngle = 0;
            m_rightWheelCollider.steerAngle = 0;
        }
    }

    public void ApplyMotorTorque(float motortorque) 
    {
        if (m_IsMotor == false) return;

        m_leftWheelCollider.motorTorque = motortorque;
        m_rightWheelCollider.motorTorque = motortorque;
    }

    public void ApplyBreakTorque(float brakeTorque)
    {
        m_leftWheelCollider.brakeTorque = brakeTorque;
        m_rightWheelCollider.brakeTorque = brakeTorque;
    }

    public float GetAvargaeRpm()
    {
        return (m_leftWheelCollider.rpm + m_rightWheelCollider.rpm) * 0.5f;
    }

    public float GetRaduis()
    {
        return m_leftWheelCollider.radius;
    }

    //private
    private void SyncMeshTransform()
    {
        UpdateWhellTransform(m_leftWheelCollider, m_leftWheelMesh);
        UpdateWhellTransform(m_rightWheelCollider, m_rightWheelMesh);
    }

    private void UpdateWhellTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out Vector3 positon, out Quaternion rotation);
        wheelTransform.position = positon;
        wheelTransform.rotation = rotation;
    }
}
