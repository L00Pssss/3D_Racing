using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip m_click;
    [SerializeField] private AudioClip m_hower;
    private AudioSource m_audio;

    private UIButton[] m_UIButtons;
    private void Start()
    {
        m_audio = GetComponent<AudioSource> ();

        m_UIButtons = GetComponentsInChildren<UIButton>(true); // даже по неактивным объектам. 

        for (int i = 0; i < m_UIButtons.Length; i++)
        {
            m_UIButtons[i].PointerEnter += OnPointerEnter;
            m_UIButtons[i].PointerClick += OnPointerClick;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < m_UIButtons.Length; i++)
        {
            m_UIButtons[i].PointerEnter -= OnPointerEnter;
            m_UIButtons[i].PointerClick -= OnPointerClick;
        }
    }
    private void OnPointerClick(UIButton arg0)
    {
        m_audio.PlayOneShot(m_click);
    }

    private void OnPointerEnter(UIButton arg0)
    {
        m_audio.PlayOneShot(m_hower); 
    }
}
