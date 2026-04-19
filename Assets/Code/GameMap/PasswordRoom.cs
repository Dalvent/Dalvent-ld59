using UnityEngine;

namespace Code.GameMap
{
    public class PasswordRoom : Room
    {
        public GameMapSelectable GameMapSelectable;

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

        private void OnUnselected()
        {
            Game.Instance.SendButtonsPanel.ClearAll();
        }

        private void OnWritingPassword()
        {
            Game.Instance.PasswordPopup.Open();
        }
    }
}