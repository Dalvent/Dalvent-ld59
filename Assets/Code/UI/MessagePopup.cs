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
        }        
        
        public void Open(string message)
        {
            gameObject.SetActive(true);

            MessageText.text = message;
        }

        public void OnOkClicked()
        {
            gameObject.SetActive(false);
        }
    }
}