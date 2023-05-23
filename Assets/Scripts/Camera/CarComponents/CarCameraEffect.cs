using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PostProcessing.Utilities;

public class CarCameraEffect : CarCameraComponent
{
    [SerializeField] [Range(0f, 1f)] private float m_normalizeSpeedShake;
    [SerializeField] private float m_shakeAmount;
    [SerializeField] private PostProcessingController m_postProcessing;
    [SerializeField] private ParticleSystem m_particleSystemWind;
    [SerializeField] private AudioMixerGroup m_audioWindGroup;

    private void Start()
    {
        m_audioWindGroup.audioMixer.SetFloat("Wind", -31f);
    }
    private void Update()
    {
        if (car.NormalizeLinerVelocity >= m_normalizeSpeedShake)
        {
            transform.localPosition += m_shakeAmount * Time.deltaTime * Random.insideUnitSphere;
            m_postProcessing.enableMotionBlur = true;
            m_particleSystemWind.Play();
            m_audioWindGroup.audioMixer.SetFloat("Wind", -31f);

        }
        else
        {
            m_postProcessing.enableMotionBlur = false;
            m_particleSystemWind.Stop();
            m_audioWindGroup.audioMixer.SetFloat("Wind", -80f);

        }
    }
}
