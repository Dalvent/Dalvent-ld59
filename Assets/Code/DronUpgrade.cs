using System;
using System.Collections.Generic;
using Code.GameMap;
using Code.GameMap.Storage;

namespace Code
{
    public sealed class DronUpgrade
    {
        private int _level = 1;

        public string Name { get; }
        public IReadOnlyList<int> LevelCosts { get; }

        public int Level
        {
            get => _level;
            private set
            {
                if (_level == value)
                    return;

                _level = value;
            }
        }

        public bool IsMaxLevel => Level >= MaxLevel;
        public int MaxLevel => LevelCosts.Count + 1;

        public int CurrentCost => IsMaxLevel ? -1 : LevelCosts[Level - 1];

        private readonly Action<DronStats, int, int> _applyLevel;

        public DronUpgrade(string name, IReadOnlyList<int> levelCosts, Action<DronStats, int, int> applyLevel)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LevelCosts = levelCosts ?? throw new ArgumentNullException(nameof(levelCosts));
            _applyLevel = applyLevel ?? throw new ArgumentNullException(nameof(applyLevel));

            if (LevelCosts.Count == 0)
                throw new ArgumentException("Upgrade must contain at least one level cost.", nameof(levelCosts));
        }

        public bool TryBuy(DronStats stats, MoneyStorage moneyStorage)
        {
            if (stats == null)
                throw new ArgumentNullException(nameof(stats));

            if (moneyStorage == null)
                throw new ArgumentNullException(nameof(moneyStorage));

            if (IsMaxLevel)
                return false;

            int cost = CurrentCost;

            if (moneyStorage.CurrentMoney < cost)
                return false;

            moneyStorage.AddMoney(-cost);

            Level++;
            _applyLevel(stats, Level, MaxLevel);

            return true;
        }

        public override string ToString()
        {
            if (IsMaxLevel)
                return $"{Name} (MAX lvl)";

            return $"{CurrentCost}$: {Name} ({Level} lvl)";
        }
    }
}