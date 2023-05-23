using TMPro;
using UnityEngine;

public class UITrackTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    [SerializeField] private TextMeshProUGUI text;
    private RaceTimeTracker m_raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => m_raceTimeTracker = obj;

    private RaceStateTracker m_raceStateTracker;
    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;

    private void Start()
    {
        m_raceStateTracker.Started += OnRaceStarted;
        m_raceStateTracker.Completed += OnRaceCompleted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.Started -= OnRaceStarted;
        m_raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStarted()
    {
        text.enabled = true;
        enabled = true;
    }
    private void OnRaceCompleted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = StringTime.SecondToTimeString(m_raceTimeTracker.CurrentTime);
    }
}
