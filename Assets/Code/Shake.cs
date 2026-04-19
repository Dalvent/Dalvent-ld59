namespace Code
{
    using UnityEngine;

    public class Shake : MonoBehaviour
    {
        [Header("Settings")]
        public bool isShaking;

        public float amplitude = 0.1f;
        public float frequency = 25f;

        private Vector3 _startPos;

        private void Awake()
        {
            _startPos = transform.localPosition;
        }

        private void Update()
        {
            if (!isShaking)
            {
                transform.localPosition = _startPos;
                return;
            }

            float x = (Mathf.PerlinNoise(Time.time * frequency, 0f) - 0.5f) * 2f;
            float y = (Mathf.PerlinNoise(0f, Time.time * frequency) - 0.5f) * 2f;
            float z = (Mathf.PerlinNoise(Time.time * frequency, Time.time * frequency) - 0.5f) * 2f;

            Vector3 offset = new Vector3(x, y, z) * amplitude;

            transform.localPosition = _startPos + offset;
        }
    }
}