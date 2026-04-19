using Code.GameMap;
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
        private PasswordRoom _passwordRoom;

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
        
        public void Open(PasswordRoom passwordRoom)
        {
            _passwordRoom = passwordRoom;
            gameObject.SetActive(true);
            RefreshView();
            
            Game.Instance.MessageAudio.PlayWithRandomPitch();
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
            foreach (var door in Game.Instance.GameField.GetDoors(_passwordRoom))
                door.Unlock();
            _passwordRoom.FillList();
            IsSended = true;
        }

        public bool IsSended { get; set; }
    }
}
