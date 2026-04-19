using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class SendButtonsPanel : MonoBehaviour
    {
        private const int WarmUpCount = 5;
        
        public SendButton Prefab;

        private readonly List<SendButton> _buttons = new();

        private void Awake()
        {
            WarmUp();
        }

        private void WarmUp()
        {
            for (int i = 0; i < WarmUpCount; i++)
                CreateButton();
        }

        private SendButton CreateButton()
        {
            var btn = Instantiate(Prefab, transform);
            btn.gameObject.SetActive(false);
            _buttons.Add(btn);
            return btn;
        }

        public void SetActions(IReadOnlyList<SendAction> actions)
        {
            while (_buttons.Count < actions.Count)
                CreateButton();

            for (int i = 0; i < _buttons.Count; i++)
            {
                if (i < actions.Count)
                {
                    _buttons[i].Init(actions[i]);
                    _buttons[i].gameObject.SetActive(true);
                }
                else
                {
                    _buttons[i].gameObject.SetActive(false);
                }
            }
        }

        public void ClearAll()
        {
            foreach (var button in _buttons)
                button.gameObject.SetActive(false);
        }
    }
}