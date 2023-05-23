using System;
using Unity.VisualScripting;
using UnityEngine;

public class CarRespawner : MonoBehaviour, IDependency<CarInputControl>, IDependency<Car>, IDependency<RaceStateTracker>
{
    [SerializeField] private float m_respawnHeight;

    private TrackPoint respawnTrackPoint;

    private RaceStateTracker m_raceStateTracker;

    //מבחמנ. 
    public void Construct(RaceStateTracker obj)
    {
        m_raceStateTracker = obj;
    }
            
        

    private Car m_car;
    public void Construct(Car obj) => m_car = obj;

    private CarInputControl m_carControl;
    public void Construct(CarInputControl obj) => m_carControl = obj;

    private void Start()
    {
        m_raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        respawnTrackPoint = point;
    }
    public void Respawn()
    {
        if (respawnTrackPoint == null) return;

        if (m_raceStateTracker.State != RaceState.Race) return;

        m_car.Respawn(respawnTrackPoint.transform.position + respawnTrackPoint.transform.up * m_respawnHeight, respawnTrackPoint.transform.rotation);

        m_carControl.Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) == true)
        {
            Respawn();
        }
    }
}
