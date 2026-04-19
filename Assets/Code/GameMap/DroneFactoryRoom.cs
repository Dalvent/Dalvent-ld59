using System;
using System.Collections.Generic;
using System.Linq;
using Code;
using Code.GameMap;
using UnityEngine;

public class DroneFactoryRoom : Room
{
    private const int SpawnDronStartCost = 5;
    private const float SpawnDronMultCost = 1.4f;

    private int CurrentNextDronCost = SpawnDronStartCost;
    private int DronBought = 0;
    
    public GameMapSelectable GameMapSelectable;
    public DronRoomMover Dron;
    private Room _nextRoom;

    private void OnEnable()
    {
        GameMapSelectable.Selected += UpdateList;
        GameMapSelectable.Unselected += Game.Instance.SendButtonsPanel.ClearAll;
        Game.Instance.DronsManager.DronMoved += UpdateList;
        Game.Instance.MoneyStorage.MoneyAdded += OnMoneyAdded;
        Game.Instance.DronStats.OnMaxCountChanged += UpdateList;
        Game.Instance.DronStats.OnMaxEnergyChanged += UpdateList;
        Game.Instance.DronStats.OnMoveTimeChanged += UpdateList;
        Game.Instance.DronStats.OnScanTimeChanged += UpdateList;
    }

    private void OnDisable()
    {
        GameMapSelectable.Selected -= UpdateList;
        Game.Instance.DronsManager.DronMoved -= UpdateList;
        GameMapSelectable.Unselected -= Game.Instance.SendButtonsPanel.ClearAll;
        Game.Instance.MoneyStorage.MoneyAdded -= OnMoneyAdded;
        Game.Instance.DronStats.OnMaxCountChanged -= UpdateList;
    }

    private void OnMoneyAdded(int obj)
    {
        UpdateList();
    }

    private void Start()
    {
        _nextRoom = Game.Instance.GameField.GetDoors(this)
            .First()
            .SecondRoom(this);
    }

    private void UpdateList()
    {
        if (!GameMapSelectable.IsSelected)
            return;
        
        var actions = new List<SendAction>();

        var dronCost = Game.Instance.DronsManager.Drons.Count == 0
            ? Math.Min(CurrentNextDronCost, Game.Instance.MoneyStorage.CurrentMoney)
            : CurrentNextDronCost;
        
        bool canBoughtNewDron = Game.Instance.DronsManager.IsRoomWithoutDron(_nextRoom) && 
                                Game.Instance.DronsManager.Drons.Count < Game.Instance.DronStats.MaxCount
                                && Game.Instance.MoneyStorage.CurrentMoney >= dronCost;
        
        actions.Add(new($"{dronCost}$: build new Drone", OnBuildNewDrone, !canBoughtNewDron));
        actions.AddRange(Game.Instance.DronsManager.CreateUpgradeActions());
        Game.Instance.SendButtonsPanel.SetActions(actions);
    }


    private void OnBuildNewDrone()
    {
        var newDrone = Instantiate(Dron, Game.Instance.GameField.transform);
        Game.Instance.DronsManager.AddDron(newDrone);

        var cost = CurrentNextDronCost;
        DronBought++;
        CurrentNextDronCost = (int)(DronBought * SpawnDronMultCost * CurrentNextDronCost);
        
        newDrone.TeleportTo(this);
        newDrone.MoveTo(_nextRoom);
        Game.Instance.MoneyStorage.AddMoney(-cost);
    }

    private void OnUpDronesMaxCount()
    {
    }

    private void OnUpDronesHp()
    {
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