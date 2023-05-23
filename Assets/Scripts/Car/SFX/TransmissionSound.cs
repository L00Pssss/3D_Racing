using UnityEngine;

public class TransmissionSound : MonoBehaviour
{
    [SerializeField] private Car m_car;
    [SerializeField] private AudioSource m_audioSource;

    private void Start()
    {
        m_car.GearChangedPlaySound += OnGearChanged;
    }

    private void OnDestroy()
    {
        m_car.GearChangedPlaySound -= OnGearChanged;
    }


    private void OnGearChanged()
    {
        m_audioSource.Play();
    }



}
