namespace Code.Player
{
    public class PlayerSelector
    {
        private GameMapSelectable _currentSelected;
        
        public void Select(GameMapSelectable selectable)
        {
            if (_currentSelected != null)
                _currentSelected.IsSelected = false;

            _currentSelected = selectable;
            if (_currentSelected != null)
                _currentSelected.IsSelected = true;
        }
    }
}