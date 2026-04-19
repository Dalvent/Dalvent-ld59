using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code
{
    public class PasswordPopup : MonoBehaviour
    {
        public TextMeshProUGUI PasswordText;
        public Button SendButton;
        public Image SendDisableImage;
        public Button CancelButton;

        public char[] _written;

        private void Awake()
        {
            SendButton.onClick.AddListener(OnSendClicked);
            CancelButton.onClick.AddListener(Close);

            RefreshView();
        }

        // TODO:
        // void Update()
        // {
        //     if (Input.anyKeyDown)
        //     {
        //         foreach (char c in Input.inputString)
        //             Debug.Log("Char: " + c);
        //     }
        // }
        
        public void Open()
        {
            gameObject.SetActive(true);
            RefreshView();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void RefreshView()
        {
            PasswordText.text = FormatPassword(Game.Instance.PasswordStorage.GetMasked());
            
            bool sendButtonInteractable = Game.Instance.PasswordStorage.IsComplete();
            SendButton.interactable = sendButtonInteractable;
            SendDisableImage.gameObject.SetActive(!sendButtonInteractable);

            // TODO: make write password.
            //_written = new char[Game.Instance.PasswordStorage.GetMasked().Length];
        }
        
        public string FormatPassword(char[] userPassword)
        {
            return string.Join(' ', userPassword);
        }

        private void OnSendClicked()
        {
        }
    }
}
