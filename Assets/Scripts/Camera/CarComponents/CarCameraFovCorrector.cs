using UnityEngine;

public class CarCameraFovCorrector : CarCameraComponent
{
    [SerializeField] private Car m_car;
    [SerializeField] private Camera m_camera;

    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldOfView;

    private float defaultFov;

    private void Start()
    {
        m_camera.fieldOfView = defaultFov;
    }

    private void Update()
    {
        m_camera.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, m_car.NormalizeLinerVelocity);
    }
}
