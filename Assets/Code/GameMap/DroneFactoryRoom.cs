using System.Collections.Generic;
using System.Linq;
using Code;
using Code.GameMap;

public class DroneFactoryRoom : Room
{
    public GameMapSelectable GameMapSelectable;
    public DronRoomMover Dron;
    private Room _nextRoom;

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

    private void Start()
    {
        _nextRoom = Game.Instance.GameField.GetDoors(this)
            .First()
            .SecondRoom(this);
    }

    private void OnSelected()
    {
        var actions = new List<SendAction>();

        bool isDisableBuildNew = Game.Instance.GameField.IsRoomWithoutDron(_nextRoom);
        actions.Add(new("41$ - build new dron", OnBuildNewDrone, isDisableBuildNew));
        
        actions.Add(new("41$ - up drone speed", OnUpDronesSpeed));
        actions.Add(new("41$ - up drone hp", OnUpDronesHp));
        actions.Add(new("41$ - upgrade max drone count", OnUpDronesMaxCount));
        actions.Add(new("Status", OnStatus));
        Game.Instance.SendButtonsPanel.SetActions(actions);
    }

    private void OnStatus()
    {
    }

    private void OnUpDronesMaxCount()
    {
    }

    private void OnUpDronesHp()
    {
    }

    private void OnBuildNewDrone()
    {
        var newDrone = Instantiate(Dron, Game.Instance.GameField.transform);
        
        newDrone.TeleportTo(this);
        newDrone.MoveTo(_nextRoom);
        Game.Instance.GameField.AddDron(newDrone);
        
        OnSelected();
        
        Game.Instance.MoneyStorage.AddMoney(50);
    }

    private void OnUpDronesSpeed()
    {
        Game.Instance.MoneyStorage.AddMoney(-50);
    }

    private void OnUnselected()
    {
        Game.Instance.SendButtonsPanel.ClearAll();
    }

    private void OnWritingPassword()
    {
    }
    
    private void OnUpgradeMaxDrones()
    {
        throw new System.NotImplementedException();
    }
}