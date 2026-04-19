using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class DroneUI : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro;

        private void Update()
        {
            TextMeshPro.text = $"Drones: {Game.Instance.DronsManager.Drons.Count}/{Game.Instance.DronStats.MaxCount}";
        }
    }
}