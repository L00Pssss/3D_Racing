using System;
using UnityEngine;

public class PauseAudioSource : MonoBehaviour, IDependency<Pauser>
{
    private AudioSource audioSource;
    private Pauser pauser;

    public void Construct(Pauser obj) => pauser = obj;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        pauser.PauseStateChange += OnPauseStateChange;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChange;
    }

    private void OnPauseStateChange(bool pause)
    {
        if (pause == true)  audioSource.Pause();

        if (pause == false) audioSource.Play();
    }
}
