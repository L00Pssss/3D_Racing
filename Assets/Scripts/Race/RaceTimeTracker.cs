using UnityEngine;

public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker m_raceStateTracker;
    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;

    private float currentTime;

    public float CurrentTime => currentTime;

    private void Start()
    {
        m_raceStateTracker.Started += OnRaceStarted;
        m_raceStateTracker.Completed+= OnRaceCompleted;

        enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.Started -= OnRaceStarted;
        m_raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStarted()
    {
        enabled = true;
        currentTime = 0;
    }
    private void OnRaceCompleted()
    {
        enabled = false;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
