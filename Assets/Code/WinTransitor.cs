using UnityEngine;

namespace Code
{
    public class WinTransitor : MonoBehaviour
    {
        public GameObject[] DisableObjects;
        public Canvas YouWin;
        
        public void ShowEnd()
        {
            Game.Instance.MessageAudio.PlayWithRandomPitch();
            YouWin.gameObject.SetActive(true);

            foreach (var disableObject in DisableObjects)
            {
                disableObject.SetActive(false);
            }
        }
    }
}