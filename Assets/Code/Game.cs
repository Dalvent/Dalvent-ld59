using Code;
using Code.Core;
using Code.GameMap;
using Code.GameMap.Storage;
using Code.Player;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public SendButtonsPanel SendButtonsPanel;
    public PasswordPopup PasswordPopup;
    public MessagePopup MessagePopup;
    public GameField GameField;
    public DoorOpen DoorOpen;
    public WinTransitor WinTransitor;
    public PlayerSelector PlayerSelector { get; }
    public MoneyStorage MoneyStorage { get; }
    public DronStats DronStats { get; }
    public PasswordStorage PasswordStorage { get; }
    public DronsManager DronsManager { get; }

    public Game()
    {
        PlayerSelector = new PlayerSelector();
        MoneyStorage = new MoneyStorage();
        DronStats = new DronStats();
        PasswordStorage = new PasswordStorage(new PasswordGenerator());

        DronsManager = new DronsManager(DronStats, MoneyStorage);
    }
    
    public void Awake()
    {
        Instance = this;
    }
}
