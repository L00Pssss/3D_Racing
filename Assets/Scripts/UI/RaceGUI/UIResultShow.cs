using System;
using TMPro;
using UnityEngine;

public class UIResultShow : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    [SerializeField] private GameObject m_goldRecordObject;
    [SerializeField] private GameObject m_playerRecordObject;
    [SerializeField] private GameObject m_playerObject;
    [SerializeField] private TextMeshProUGUI m_goldRecordTime;
    [SerializeField] private TextMeshProUGUI m_playerRecordTime;
    [SerializeField] private TextMeshProUGUI m_playerTime;

    //  [SerializeField] private TextMeshProUGUI m_recordLable;

    private RaceResultTime m_raceResultTime;
    public void Construct(RaceResultTime obj) => m_raceResultTime = obj;

    private RaceStateTracker m_raceStateTracker;
    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;

    private RaceTimeTracker m_raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => m_raceTimeTracker = obj;

    private void Start()
    {
        m_raceResultTime.ResultsUpdated += OnRaceUpdate;


        m_goldRecordObject.SetActive(false);
        m_playerRecordObject.SetActive(false);
        m_playerObject.SetActive(false);
    }

    private void OnDestroy()
    {
        m_raceResultTime.ResultsUpdated -= OnRaceUpdate;
    }
    private void OnRaceUpdate()
    {
        m_goldRecordObject.SetActive(true);
        m_playerRecordObject.SetActive(true);
        m_playerObject.SetActive(true);
        m_playerTime.text = StringTime.SecondToTimeString(m_raceTimeTracker.CurrentTime);
        m_goldRecordTime.text = StringTime.SecondToTimeString(m_raceResultTime.GoldTime);
        m_playerRecordTime.text = StringTime.SecondToTimeString(m_raceResultTime.PlayerRecordTime);
    }
}