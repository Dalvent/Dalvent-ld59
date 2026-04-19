using System.Collections;
using UnityEngine;

namespace Code
{
    public class DebikScream : MonoBehaviour
    {
        public AudioSource AudioSource;
        public DoorOpen DoorOpen;
        public Shake Shake;
        private Coroutine _audioCarutine;

        private void OnEnable()
        {
            DoorOpen.StartMoving += OnStartMoving;
            DoorOpen.StopMoving += OnStopMoving;
        }

        private void OnDisable()
        {
            DoorOpen.StartMoving -= OnStartMoving;
            DoorOpen.StopMoving -= OnStopMoving;
        }

        private void Update()
        {
            Shake.isShaking = AudioSource.isPlaying;
        }

        private void OnStartMoving()
        {
            if (!DoorOpen.IsOpen)
                return;
            
            _audioCarutine = StartCoroutine(PlayWithDelay());
        }

        private void OnStopMoving()
        {
            if (DoorOpen.IsOpen)
                return;

            AudioSource.Stop();
            StopCoroutine(_audioCarutine);
        }
        
        private IEnumerator PlayWithDelay()
        {
            yield return new WaitForSeconds(1f);
            while (true)
            {
                AudioSource.PlayWithRandomPitch();
                
                yield return new WaitForSeconds(1f);
            }
        }
    }
}