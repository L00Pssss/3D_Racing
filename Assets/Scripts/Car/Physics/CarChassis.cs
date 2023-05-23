using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] m_wheelAxles;
    [SerializeField] private float m_weelBaseLength;

    [SerializeField] private Transform centerOfMass;


    [Header("Down Force")]
    [SerializeField] private float m_downForceMin;
    [SerializeField] private float m_downForceMax;
    [SerializeField] private float m_downForceFactor;



    [Header("AngularDrag")]
    [SerializeField] private float m_angularDragMin;
    [SerializeField] private float m_angularDragMax;
    [SerializeField] private float m_angularDragFactor;
    //debug
    public float m_EngineTorque;
    public float m_BrakeTorque;
    public float m_SteerAnngel;

    public float GetAverageRpm()
    {
        float sum = 0;

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            sum += m_wheelAxles[i].GetAvargaeRpm();
        }

        return sum / m_wheelAxles.Length;
    }
    public float GetWheelSpeed()
    {
        return GetAverageRpm() * m_wheelAxles[0].GetRaduis() * 2f * 0.1885f; // 2pr
    }

    public float LinearVelocity => m_rigidbody.velocity.magnitude * 3.6f;

    private Rigidbody m_rigidbody;
    // Таким образом, данный код позволяет получить компонент Rigidbody на объекте, если он не был инициализирован заранее, иначе получить сохраненную ранее ссылку на компонент.
    public Rigidbody Rigidbody => m_rigidbody == null? GetComponent<Rigidbody>(): Rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        if (centerOfMass != null)
        {
            m_rigidbody.centerOfMass = centerOfMass.localPosition;
        }

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            m_wheelAxles[i].ConfigureVehicleSubsteps(50, 50, 50);
        }
    }
    private void FixedUpdate()
    {
        UpdateAngularDrag();

        UpdateDownForce();
        UpdateWheelAxles();
    }



    private void UpdateAngularDrag()
    {
        m_rigidbody.angularDrag = Mathf.Clamp(m_angularDragFactor * LinearVelocity, m_angularDragMin, m_angularDragMax);
    }

    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(m_downForceFactor * LinearVelocity, m_downForceMin, m_downForceMax);
        m_rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxles()
    {
        int amountMotorWheel = 0;

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            if (m_wheelAxles[i].IsMotor == true)
                amountMotorWheel += 2;
        }

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            m_wheelAxles[i].Update();

            m_wheelAxles[i].ApplyMotorTorque(m_EngineTorque / amountMotorWheel);
            m_wheelAxles[i].ApplySteerAngel(m_SteerAnngel, m_weelBaseLength);
            m_wheelAxles[i].ApplyBreakTorque(m_BrakeTorque);
        }
    }

    public void Reset()
    {
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = Vector3.zero;
    }
}
