using UnityEngine;
using UnityEngine.Events;


public enum RaceState
{
   Preparation,
   CoundDown,
   Race,
   Passed
}

public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
{
    public event UnityAction PeparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapComplated;


    private TrackPointCircuit m_trackPointCircuit;

    [SerializeField] private Timer m_countdownTimer;
    [SerializeField] private int lapsToComplete;

    public Timer CountDownTimer => m_countdownTimer;

    private RaceState state;
    public RaceState State => state;
    public void Construct(TrackPointCircuit trackPointCircuit)
    {
        this.m_trackPointCircuit = trackPointCircuit;
    }
    private void Start()
    {
        StartState(RaceState.Preparation);

        m_countdownTimer.enabled = false;

        m_countdownTimer.Finished += OnCountdownTimerFinished;

        m_trackPointCircuit.TrackPointTriggered += OnTrackPointTriggerd;
        m_trackPointCircuit.LapCompleted += OnLapCompleted;
    }

    private void OnDestroy()
    {
        m_countdownTimer.Finished -= OnCountdownTimerFinished;

        m_trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggerd;
        m_trackPointCircuit.LapCompleted -= OnLapCompleted;
    }

    private void StartState(RaceState state)
    {
        this.state = state;
    }
    private void OnTrackPointTriggerd(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }
    private void OnCountdownTimerFinished()
    {
        StartRace();
    }

    private void OnLapCompleted(int lapAmount)
    {
        if (m_trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (m_trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
            {
                CompleteRace();
            }
            else
            {
                CompleteLap(lapAmount);
            }
        }
    }

    public void StartRaceTimer()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.CoundDown);

        m_countdownTimer.enabled = true;
        PeparationStarted?.Invoke();
    }
    private void StartRace()
    {
        if (state != RaceState.CoundDown) return;
        StartState(RaceState.Race);

        Started?.Invoke();
    }
    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);

        Completed?.Invoke();
    }


    private void CompleteLap(int lapAmount)
    {
        LapComplated?.Invoke(lapAmount);
    }


}
