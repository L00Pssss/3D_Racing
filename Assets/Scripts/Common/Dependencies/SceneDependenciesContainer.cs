using UnityEngine;
public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private RaceStateTracker m_raceStateTracker;
    [SerializeField] private CarInputControl m_carInputContorl;
    [SerializeField] private Car m_car;
    [SerializeField] private CarCameraController m_carCameraController;
    [SerializeField] private TrackPointCircuit m_trackPointCircuit;
    [SerializeField] private RaceTimeTracker m_raceTimeTracker;
    [SerializeField] private RaceResultTime m_raceResultTime;

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<RaceStateTracker>(m_raceStateTracker, monoBehaviourInScene);
        Bind<CarInputControl>(m_carInputContorl, monoBehaviourInScene);
        Bind<Car>(m_car, monoBehaviourInScene);
        Bind<CarCameraController>(m_carCameraController, monoBehaviourInScene);
        Bind<TrackPointCircuit>(m_trackPointCircuit, monoBehaviourInScene);
        Bind<RaceTimeTracker>(m_raceTimeTracker, monoBehaviourInScene);
        Bind<RaceResultTime>(m_raceResultTime, monoBehaviourInScene);
    }
    private void Awake()
    {
        FindAllObjectToBind();
    }
}
