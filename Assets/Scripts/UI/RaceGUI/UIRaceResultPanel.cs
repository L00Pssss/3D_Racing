using TMPro;
using UnityEngine;

public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
{
    [SerializeField] private GameObject m_resultPanel;
    [SerializeField] private TextMeshProUGUI m_recordTime;
    [SerializeField] private TextMeshProUGUI m_currentTime;

    private RaceResultTime raceResultTime;

    public void Construct(RaceResultTime obj) => raceResultTime = obj;


    private void Start()
    {
        raceResultTime.ResultsUpdated += OnUpdateResults;
        m_resultPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        raceResultTime.ResultsUpdated -= OnUpdateResults;
    }

    private void OnUpdateResults()
    {
        m_resultPanel.SetActive(true);

        m_recordTime.text = StringTime.SecondToTimeString(raceResultTime.GetAbsoluteRcord());
        m_currentTime.text = StringTime.SecondToTimeString(raceResultTime.CurrenTime);
    }
}
