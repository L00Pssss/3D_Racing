using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{    
    
    [SerializeField] private float m_maxSteerAngel;
    [SerializeField] private float m_maxBrakeTorque;


    [Header("Engine")]
    [SerializeField] private AnimationCurve m_engineTorqueCurve;

    [SerializeField] private float m_engineMaxTorque;

    [SerializeField] private float m_engineMinRpm;
    [SerializeField] private float m_engineMaxRpm;

    [SerializeField] private float m_maxSpeed;


    [Header("Gearbox")]
    [SerializeField] private float[] m_gears;
    [SerializeField] private float m_finalDriveRatio;
    //debug
    [SerializeField] private int m_selectedGearIndex;
    [SerializeField] private float m_upShiftEngineRpm;
    [SerializeField] private float m_downShiftEngineRpm;

    public event UnityAction<string> GearChanged;
    public event UnityAction GearChangedPlaySound;
    public float LinerVelocity => m_chassis.LinearVelocity;
    public float NormalizeLinerVelocity => m_chassis.LinearVelocity / m_maxSpeed;
    public float WheelSpeed => m_chassis.GetWheelSpeed();

    public float MaxSpeed => m_maxSpeed;

    public float SteerAngel => m_chassis.m_SteerAnngel;

    public float EngineRpm => m_engineRpm;
    public float EngineMaxRpm => m_engineMaxRpm;

    public Rigidbody Rigidbody => m_chassis ==null? GetComponent<CarChassis>().Rigidbody : m_chassis.Rigidbody;

    private CarChassis m_chassis;



    //debug
    [SerializeField] private float m_engineTorque;
    [SerializeField] private float m_engineRpm;
    [SerializeField] private float m_selectedGear;
    [SerializeField] private float m_rearGear;
    [SerializeField] private float linerVelocity;
    [SerializeField] private float steerAngel;
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeContorl;



    
    
    private void Start()
    {
        m_chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        steerAngel = SteerAngel;
        linerVelocity = LinerVelocity;

        UpdateEngineTorque();

        AutoGearShift();

        if (LinerVelocity >= m_maxSpeed)
        {
            m_engineTorque = 0;
        }

        m_chassis.m_EngineTorque = m_engineTorque * ThrottleControl;
        m_chassis.m_SteerAnngel = m_maxSteerAngel * SteerControl;
        m_chassis.m_BrakeTorque = m_maxBrakeTorque * BrakeContorl;
    }
    // Gearbox

    public string GetSelectedGearName()
    {
        if (m_selectedGear == m_rearGear) return "R";

        if (m_engineRpm == 800) return "N";

        return (m_selectedGearIndex + 1).ToString();
    }
    private void AutoGearShift()
    {
        if (m_selectedGear < 0) return;

        if (m_engineRpm >= m_upShiftEngineRpm)
        {
            UpGear();

            GearChangedPlaySound?.Invoke();
        }
        if (m_engineRpm < m_downShiftEngineRpm && m_engineRpm > 800 ) // проверить этот код (badwork)
        {
                m_selectedGear = 0;

                DownGear();

                GearChangedPlaySound?.Invoke();
        }
    }
    public void UpGear()
    {
        ShiftGear(m_selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(m_selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        m_selectedGear = m_rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
        GearChangedPlaySound?.Invoke();
    }

    public void ShifToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNetral()
    {
        m_selectedGear = 0;
        GearChangedPlaySound?.Invoke();
        GearChanged?.Invoke(GetSelectedGearName());
    }
    private void ShiftGear(int gearIndex)
    {
        
        gearIndex = Mathf.Clamp(gearIndex, 0, m_gears.Length - 1);
        m_selectedGear = m_gears[gearIndex];
        m_selectedGearIndex = gearIndex;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    private void UpdateEngineTorque()
    {
        m_engineRpm = m_engineMinRpm + Mathf.Abs(m_chassis.GetAverageRpm() * m_selectedGear * m_finalDriveRatio); // что бі не умножалось на холостые 
        m_engineRpm = Mathf.Clamp(m_engineRpm, m_engineMinRpm, m_engineMaxRpm);

        m_engineTorque = m_engineTorqueCurve.Evaluate(m_engineRpm / m_engineMaxRpm) * m_engineMaxTorque * m_finalDriveRatio * Mathf.Sign(m_selectedGear) * m_gears[0];
    }

    public void Reset()
    {
        m_chassis.Reset();

        m_chassis.m_EngineTorque = 0;
        m_chassis.m_BrakeTorque = 0;
        m_chassis.m_SteerAnngel = 0;

        ThrottleControl = 0;
        BrakeContorl = 0;
        SteerControl = 0;
    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();
        transform.position = position;

        transform.rotation = rotation;
    }
}
