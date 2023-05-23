using TMPro;
using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<Pauser>, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject m_panel;
    [SerializeField] private TextMeshProUGUI m_Text;
    private Pauser pauser;
    private RaceStateTracker m_stateTracker;

    public void Construct(Pauser obj) => pauser = obj;
    public void Construct(RaceStateTracker obj) => m_stateTracker = obj;

    public void UnPause()
    {
        pauser.UnPuase();
    }

    private void Start()
    {
        m_panel.SetActive(false);
        pauser.PauseStateChange += OnPauseStatChanged;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStatChanged;
    }
    private void OnPauseStatChanged(bool isPause)
    {
        m_panel.SetActive(isPause);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            pauser.ChangePauseState();
            if (m_stateTracker.State == RaceState.Preparation && pauser.IsPause == true)
            {
                m_Text.enabled = false;
            }
            if (m_stateTracker.State == RaceState.Preparation && pauser.IsPause == false)
            {
                m_Text.enabled = true;
            }
        }
    }
}
