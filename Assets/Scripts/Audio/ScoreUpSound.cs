using System;
using Events;
using Game;
using UnityEngine;

namespace Audio
{
    public class ScoreUpSound : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        
        private AudioSource audioSource;
        private ScoreEventChannel scoreEventChannel;

        private void Awake()
        {
            scoreEventChannel = Finder.ScoreEventChannel;
            audioSource = gameObject.GetComponent<AudioSource>();
            
        }

        private void OnEnable()
        {
            scoreEventChannel.OnScoreChanged += PlaySound;
        }

        private void OnDisable()
        {
            scoreEventChannel.OnScoreChanged -= PlaySound;
        }

        private void PlaySound()
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}