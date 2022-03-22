using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSound : MonoBehaviour
{

        public AudioClip[] clips;

        public bool randomizePitch = true;
        public float pitchRange = 0.2f;

        protected AudioSource m_Source;

        private void Awake()
        {
            m_Source = GetComponent<AudioSource>();
        }

        public void PlayRandomSound()
        {
            AudioClip[] source = clips;

            int choice = Random.Range(0, source.Length);

            if (randomizePitch)
                m_Source.pitch = Random.Range(1.0f - pitchRange, 1.0f + pitchRange);

            m_Source.PlayOneShot(source[choice]);
        }

        public void Stop()
        {
            m_Source.Stop();
        }
}