using TMPro;
using UnityEngine;

public class UIRaceTextStart : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker m_raceStateTracker;
    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;
    [SerializeField] private TextMeshProUGUI m_textMeshProUGUI;
    [SerializeField] private Animator m_animator;

    private void Start()
    {
        m_raceStateTracker.PeparationStarted += OnPeparationStarted;

        m_textMeshProUGUI.enabled = true;
        m_animator.enabled = true;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.PeparationStarted -= OnPeparationStarted;
    }

    private void OnPeparationStarted()
    {
        m_textMeshProUGUI.enabled = false;
        m_animator.enabled = false;
    }

}
