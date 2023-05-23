using System;
using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private Car m_car;
    [SerializeField] private new Camera m_camera;
    [SerializeField] private CarCameraFollow m_follower;
    [SerializeField] private CarCameraEffect m_effect;
    [SerializeField] private CarCameraFovCorrector m_fovCorrector;
    [SerializeField] private CameraPathFollower m_pathFollower;

    private RaceStateTracker m_stateTracker;
    public void Construct(RaceStateTracker obj) => m_stateTracker = obj;

    private void Awake()
    {
        m_follower.SetProperties(m_car, m_camera);
        m_effect.SetProperties(m_car, m_camera);
        m_follower.SetProperties(m_car, m_camera);
    }

    private void Start()
    {
        m_stateTracker.PeparationStarted += OnPeparationStarted;
        m_stateTracker.Completed += OnCompleted;

        m_follower.enabled = false;
        m_pathFollower.enabled = true;
    }
    private void OnDestroy()
    {
        m_stateTracker.PeparationStarted -= OnPeparationStarted;
        m_stateTracker.Completed -= OnCompleted;
    }

    private void OnPeparationStarted()
    {
        m_follower.enabled = true;
        m_pathFollower.enabled = false;
    }
    private void OnCompleted()
    {
        m_pathFollower.enabled = true;
        m_pathFollower.StartMoveToNearestPoint();
        m_pathFollower.SetLookTarget(m_car.transform);

        m_follower.enabled = false;
    }


}
