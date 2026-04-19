using System;

namespace Code.GameMap.Storage
{
    public class MoneyStorage
    {
        public int CurrentMoney { get; private set; } = 500;
        
        public event Action<int> MoneyAdded;
        
        public void AddMoney(int value)
        {
            CurrentMoney += value;
            MoneyAdded?.Invoke(value);
        }
    }
}