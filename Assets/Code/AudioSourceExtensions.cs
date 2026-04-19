using UnityEngine;

namespace Code
{
    public static class AudioSourceExtensions
    {
        public static void PlayWithRandomPitch(this AudioSource source, float minPitch = 0.85f, float maxPitch = 1.15f)
        {
            if (source == null)
                return;

            source.pitch = Random.Range(minPitch, maxPitch);
            source.Play();
        }
    }
}