using UnityEngine;

public class SuspensionArm : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_factor;

    private float baseOffset;
    private Vector3 startRotation;

    private void Start()
    {
        baseOffset = m_target.localPosition.y;
        startRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        if (m_target != null)
        {

            float angle = (m_target.localPosition.y - baseOffset) * m_factor;
            Vector3 newRotation = startRotation + new Vector3(0, 0, angle);
            transform.localEulerAngles = newRotation;
        }
    }
}
