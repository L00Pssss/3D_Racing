using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    public const string SaveMark = "_player_best_time";

    public event UnityAction ResultsUpdated;

    [SerializeField] private float goldTime;

    private float playerRecordTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float PlayerRecordTime => playerRecordTime;
    public float CurrenTime => currentTime;
    public bool RecordWasSet => playerRecordTime != 0;


    private RaceTimeTracker m_raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => m_raceTimeTracker = obj;

    private RaceStateTracker m_raceStateTracker;
    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;

    private void Awake()
    {
        Load();
    }
    private void Start()
    {
        m_raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        float absouteRcord = GetAbsoluteRcord();

        if (m_raceTimeTracker.CurrentTime < absouteRcord || playerRecordTime == 0)
        {
            playerRecordTime = m_raceTimeTracker.CurrentTime;

            Save();
        }

        currentTime = m_raceTimeTracker.CurrentTime;

        ResultsUpdated?.Invoke();
    }

    public float GetAbsoluteRcord()
    {
        if (playerRecordTime < goldTime && playerRecordTime != 0)
        {
            return playerRecordTime;
        }
        else return goldTime;
    }

    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, PlayerRecordTime);
    }
}
