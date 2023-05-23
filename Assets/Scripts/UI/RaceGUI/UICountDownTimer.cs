using TMPro;
using UnityEngine;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker m_raceStateTracker;
    [SerializeField] private TextMeshProUGUI m_textMeshProUGUI;
    [SerializeField] Timer m_countDownTimer;
    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;
    private void Start()
    {
        m_raceStateTracker.PeparationStarted += OnPeparationStarted;
        m_raceStateTracker.Started += OnRaceStarted;

        m_textMeshProUGUI.enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.PeparationStarted -= OnPeparationStarted;
        m_raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnPeparationStarted()
    {
        m_textMeshProUGUI.enabled = true;
        enabled = true;
    }

    private void OnRaceStarted()
    {
        m_textMeshProUGUI.enabled = false;
        enabled = false;
    }


    private void Update()
    {
        m_textMeshProUGUI.text = m_raceStateTracker.CountDownTimer.Value.ToString("F0"); // что бы не было симв после запятой. 

        if (m_textMeshProUGUI.text == "0")
            m_textMeshProUGUI.text = "GO!";
    }


}
