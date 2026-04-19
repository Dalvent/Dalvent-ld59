using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code
{
    public class GameMapSelectable : MonoBehaviour, IPointerClickHandler
    {
        public event Action Selected;
        public event Action Unselected;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;

                if (_isSelected)
                    Selected?.Invoke();
                else
                    Unselected?.Invoke();
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Game.Instance.PlayerSelector.Select(this);
            Game.Instance.SelectNodeAudio.PlayWithRandomPitch();
        }
    }
}