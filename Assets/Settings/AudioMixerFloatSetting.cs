using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerFloatSetting : Setting
{
    [SerializeField] private AudioMixer m_audioMixer;
    [SerializeField] private string m_nameParametry;


    [SerializeField] private float m_minRealValue;
    [SerializeField] private float m_maxRealValue;

    [SerializeField] private float m_virtualStep;
    [SerializeField] private float m_minVirutalValue;
    [SerializeField] private float m_maxVirutalValue;

    private float currentValue = 0;

    public override bool isMinValue { get => currentValue == m_minRealValue; }
    public override bool isMaxValue { get => currentValue == m_maxRealValue; }

    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(m_maxRealValue - m_minRealValue) / m_virtualStep);
    }

    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(m_maxRealValue - m_minRealValue) / m_virtualStep);
    }

    // -40 0...-80 0...100
    public override string GetStringValue()
    {
        return Mathf.Lerp(m_minVirutalValue, m_maxVirutalValue, (currentValue - m_minRealValue) / (m_maxRealValue - m_minRealValue)).ToString();
    }
    public override object GetVelue()
    {
        return currentValue;
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, m_minRealValue, m_maxRealValue);
    }

    public override void Apply()
    {
        m_audioMixer.SetFloat(m_nameParametry, currentValue);

        Save();
    }

    public override void Load()
    {
        currentValue = PlayerPrefs.GetFloat(m_title, 0);
    }
    private void Save()
    {
        PlayerPrefs.SetFloat(m_title, currentValue);
    }
}
