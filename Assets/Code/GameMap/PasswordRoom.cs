using UnityEngine;

namespace Code.GameMap
{
    public class PasswordRoom : Room
    {
        public GameMapSelectable GameMapSelectable;

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
            Game.Instance.SendButtonsPanel.ClearAll();
            
            if (Game.Instance.PasswordPopup.IsSended)
            {
                Game.Instance.SendButtonsPanel.SetActions(new []
                {
                    new SendAction("Status", OnShowStatus2)
                });
                return;
            }
            
            Game.Instance.SendButtonsPanel.SetActions(new []
            {
                new SendAction("Status", OnShowStatus),
                new SendAction("Password", OnWritingPassword)
            });
        }

        private void OnShowStatus()
        {
            Game.Instance.MessagePopup.Open(MessageType.ItsYourTarget);
        }

        private void OnShowStatus2()
        {
            Game.Instance.MessagePopup.Open("Status - Ready to win!");
        }
        
        private void OnUnselected()
        {
            Game.Instance.SendButtonsPanel.ClearAll();
        }

        private void OnWritingPassword()
        {
            Game.Instance.PasswordPopup.Open(this);
        }
    }
}