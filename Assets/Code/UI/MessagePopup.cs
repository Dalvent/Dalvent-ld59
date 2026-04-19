using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class MessagePopup : MonoBehaviour
    {
        public TextMeshProUGUI MessageText;
        public Button OkButton;

        private void Awake()
        {
            OkButton.onClick.AddListener(OnOkClicked);
        }

        public void Open(MessageType message)
        {
            gameObject.SetActive(true);
            
            MessageText.text = message.GetMessage();
            Game.Instance.MessageAudio.PlayWithRandomPitch();
        }        
        
        public void Open(string message)
        {
            gameObject.SetActive(true);

            MessageText.text = message;
            Game.Instance.MessageAudio.PlayWithRandomPitch();
        }

        public void OnOkClicked()
        {
            gameObject.SetActive(false);
        }
    }
}