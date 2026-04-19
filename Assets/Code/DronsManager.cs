using System;
using System.Collections.Generic;
using System.Linq;
using Code.GameMap;
using Code.GameMap.Storage;

namespace Code
{
    public sealed class DronsManager
    {
        private readonly List<DronRoomMover> _drons = new();
        private readonly DronStats _dronStats;
        private readonly MoneyStorage _moneyStorage;
        private IReadOnlyCollection<DronUpgrade> _upgrades;

        public event Action DronMoved;

        public IReadOnlyList<DronRoomMover> Drons => _drons;
        
        public DronsManager(DronStats dronStats, MoneyStorage moneyStorage)
        {
            _dronStats = dronStats ?? throw new ArgumentNullException(nameof(dronStats));
            _moneyStorage = moneyStorage ?? throw new ArgumentNullException(nameof(moneyStorage));

            var upgrades = new List<DronUpgrade>
            {
                new DronUpgrade(
                    "Max Drones",
                    new[] { 100, 200, 500, 1000 },
                    (stats, level, maxLevel) => stats.MaxCount = level),

                new DronUpgrade(
                    "Scan Time",
                    new[] { 15, 50, 100, 200, 300 },
                    (stats, level, maxLevel) => { stats.ScanTime = DronStats.GetScanTime(level, maxLevel); }),

                new DronUpgrade(
                    "Max Energy",
                    new[] { 50, 150, 350, 500 },
                    (stats, level, maxLevel) => { stats.MaxEnergy = DronStats.GetMaxEnergy(level, maxLevel); }),

                new DronUpgrade(
                    "Move Time",
                    new[] { 15, 100, 200, 300, 500 },
                    (stats, level, maxLevel) => { stats.MoveTime = DronStats.GetMoveTime(level, maxLevel); })
            };

            _upgrades = upgrades;
        }

        public void AddDron(DronRoomMover dron)
        {
            if (dron == null)
                throw new ArgumentNullException(nameof(dron));

            if (_drons.Contains(dron))
                return;

            _drons.Add(dron);
            dron.Moved += OnDronMoved;
        }

        public void RemoveDrone(DronRoomMover dron)
        {
            if (dron == null)
                return;

            _drons.Remove(dron);
            dron.Moved -= OnDronMoved;
        }

        private void OnDronMoved(DronRoomMover dronRoomMover)
        {
            DronMoved?.Invoke();
        }

        public bool IsRoomWithoutDron(Room room) => _drons.All(d => d.CurrentRoom != room);

        public DronUpgrade GetUpgrade(string name)
        {
            return _upgrades.FirstOrDefault(x => x.Name == name);
        }

        public SendAction[] CreateUpgradeActions()
        {
            var actions = new SendAction[_upgrades.Count];
            int index = 0;
            
            foreach (var upgrade in _upgrades)
            {
                if (upgrade == null)
                    throw new ArgumentNullException(nameof(upgrade));

                bool isDisabled = upgrade.IsMaxLevel || _moneyStorage.CurrentMoney < upgrade.CurrentCost;
                var action = new SendAction(
                    upgrade.ToString(),
                    () => _ = upgrade.TryBuy(_dronStats, _moneyStorage),
                    isDisabled);
                actions[index++] = action;
            }

            return actions;
        }
    }
}