using System.Collections.Generic;
using Code.GameMap.FogRooms;
using UnityEngine;

namespace Code.GameMap
{
    public class BattleFogRoom : Room
    {
        public FogRoom Connected { get; private set; }
        public GameObject Unknown;
        
        public bool IsConnected => Connected.gameObject.activeSelf;
        
        public GameMapSelectable GameMapSelectable;

        public void MakeConnected()
        {
            Connected.gameObject.SetActive(true);
            Unknown.SetActive(false);
            Connected.OnConnected(this);

            if (GameMapSelectable.IsSelected)
                FillList();
        }
        
        private void Awake()
        {
            Connected = GetComponentInChildren<FogRoom>();
            Connected.gameObject.SetActive(false);
            Unknown.SetActive(true);
        }

        private void OnEnable()
        {
            GameMapSelectable.Selected += FillList;
            GameMapSelectable.Unselected += OnUnselected;
        }
        
        private void OnDisable()
        {
            GameMapSelectable.Selected -= FillList;
            GameMapSelectable.Unselected -= OnUnselected;
        }

        public void FillList()
        {
            if (!GameMapSelectable.IsSelected)
                return;
            
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