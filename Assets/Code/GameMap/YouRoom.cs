namespace Code.GameMap
{
    public class YouRoom : Room
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
            var actions = new []
            {
                new SendAction("Status", OnShowStatus),
                new SendAction("Use door", OnOpenDoor)
            };
            Game.Instance.SendButtonsPanel.SetActions(actions);
        }

        private void OnUnselected()
        {
            Game.Instance.SendButtonsPanel.ClearAll();
        }

        private void OnShowStatus()
        {
            Game.Instance.MessagePopup.Open(MessageType.ItsYou);
        }

        private void OnOpenDoor()
        {
            Game.Instance.DoorOpen.Interact();
        }
    }
}