using System;

namespace Code.GameMap
{
    public class DronStats
    {
        public const float MoveTimeDefault = 2.0f;
        public const float MoveTimeMax = 0.1f;
        public const float ScanTimeDefault = 3.0f;
        public const float ScanTimeMax = 0.1f;
        public const int MaxEnergyDefault = 7;
        public const int MaxEnergyMax = 50;
        
        public static float GetMoveTime(int level, int maxLevel)
            => CalculateByLvl(MoveTimeDefault, MoveTimeMax, level, maxLevel);

        public static float GetScanTime(int level, int maxLevel)
            => CalculateByLvl(ScanTimeDefault, ScanTimeMax, level, maxLevel);

        public static int GetMaxEnergy(int level, int maxLevel)
            => (int)CalculateByLvl(MaxEnergyDefault, MaxEnergyMax, level, maxLevel);
        
        private static float CalculateByLvl(float start, float end, int level, int maxLevel)
        {
            if (maxLevel <= 0)
                throw new ArgumentException("maxLevel must be > 0");

            float t = (float)level / maxLevel;
            return start + (end - start) * t;
        }
        
        public event Action OnMaxCountChanged;
        public event Action OnMoveTimeChanged;
        public event Action OnScanTimeChanged;
        public event Action OnMaxEnergyChanged;

        private float _maxCount = 1;
        public float MaxCount
        {
            get => _maxCount;
            set
            {
                if (_maxCount == value) return;
                _maxCount = value;
                OnMaxCountChanged?.Invoke();
            }
        }

        private float _moveTime = MoveTimeDefault;
        public float MoveTime
        {
            get => _moveTime;
            set
            {
                if (_moveTime == value) return;
                _moveTime = value;
                OnMoveTimeChanged?.Invoke();
            }
        }

        private float _scanTime = ScanTimeDefault;
        public float ScanTime
        {
            get => _scanTime;
            set
            {
                if (_scanTime == value) return;
                _scanTime = value;
                OnScanTimeChanged?.Invoke();
            }
        }

        private int _maxEnergy = MaxEnergyDefault;
        public int MaxEnergy
        {
            get => _maxEnergy;
            set
            {
                if (_maxEnergy == value) return;
                _maxEnergy = value;
                OnMaxEnergyChanged?.Invoke();
            }
        }
    }
}