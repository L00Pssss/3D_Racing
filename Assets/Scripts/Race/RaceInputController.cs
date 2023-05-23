using System;
using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<CarInputControl>
{
    private CarInputControl m_carControl;
    private RaceStateTracker m_raceStateTracker;

    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;
    public void Construct(CarInputControl obj) => m_carControl = obj;

    private void Start()
    {
        m_raceStateTracker.Started += OnRaceStarted;
        m_raceStateTracker.Completed += OnRaceCompletd;

        m_carControl.enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.Started -= OnRaceStarted;
        m_raceStateTracker.Completed -= OnRaceCompletd;
    }

    private void OnRaceStarted()
    {
        m_carControl.enabled = true;
    }

    private void OnRaceCompletd()
    {
        m_carControl.enabled = false;
        m_carControl.Stop();
    }


}
