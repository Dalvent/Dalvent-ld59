using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code
{
    public struct SendAction
    {
        private readonly Action _action;
        public string Name { get; }
        public bool IsDisabled { get; }

        public SendAction(string name, Action action, bool isDisabled = false)
        {
            _action = action;
            Name = name;
            IsDisabled = isDisabled;
        }

        public void Use() => _action.Invoke();
    }

    public class SendButton : MonoBehaviour
    {
        public GameObject DisableOverlay;
        public TextMeshProUGUI Text;
        public Button Button;

        private UnityAction _currentAction;
        
        public void Init(SendAction sendAction)
        {
            if (_currentAction != null)
                Button.onClick.RemoveListener(_currentAction);

            DisableOverlay.gameObject.SetActive(sendAction.IsDisabled);
            
            Text.text = sendAction.Name;
            _currentAction = sendAction.Use;
            Button.onClick.AddListener(_currentAction);
            Button.interactable = !sendAction.IsDisabled;
        }
    }
}