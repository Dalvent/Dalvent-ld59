using System.Collections.Generic;
using Code.GameMap.FogRooms;
using UnityEngine;

namespace Code.GameMap
{
    public class BattleFogRoom : Room
    {
        public FogRoom Connected;
        public GameObject Unknown;
        
        public bool IsConnected => Connected.gameObject.activeSelf;
        
        public GameMapSelectable GameMapSelectable;

        public void MakeConnected()
        {
            Connected.gameObject.SetActive(true);
            Unknown.SetActive(false);

            if (GameMapSelectable.IsSelected)
                OnSelected();
        }
        
        private void Awake()
        {
            Connected.gameObject.SetActive(false);
            Unknown.SetActive(true);
        }

        private void OnEnable()
        {
            GameMapSelectable.Selected += OnSelected;
            GameMapSelectable.Unselected += OnUnselected;
        }
        
        private void OnDisable()
        {
            GameMapSelectable.Selected -= OnSelected;
            GameMapSelectable.Unselected -= OnUnselected;
        }

        private void OnSelected()
        {
            var actions = new List<SendAction>
            {
                new("Status", OnWritingPassword),
            };
            
            if (IsConnected)
                actions.AddRange(Connected.GetSpecialActions());
            
            Game.Instance.SendButtonsPanel.SetActions(actions);
        }

        private void OnUnselected()
        {
            Game.Instance.SendButtonsPanel.ClearAll();
        }

        private void OnWritingPassword()
        {
            Game.Instance.MessagePopup.Open(IsConnected ? Connected.GetStatus() : MessageType.UnknownRoom);
        }
    }
}