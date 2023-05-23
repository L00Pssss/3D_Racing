using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour,IDependency<RaceStateTracker>
{
    private RaceStateTracker m_raceStateTracker;
    public void Construct(RaceStateTracker obj) => m_raceStateTracker = obj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            m_raceStateTracker.StartRaceTimer();
        }
    }
}
