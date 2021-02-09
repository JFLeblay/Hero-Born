using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    // Constants

    // Public properties
    public string labelTxt = "Collect all 4 items and win your freedom!";
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public int maxItems = 4;
    public Stack<string> lootStack = new Stack<string>();
    public delegate void DebugDelegate(string logMessage);
    public DebugDelegate debug = Print;

    // Private properties

    // Getters/Setters
    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems)
            {
                labelTxt = "You've found all 4 items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelTxt = "Item found, only " + (maxItems - _itemsCollected) + " to go!";
            }
        }
    }

    private int _playerHP = 5;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                labelTxt = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelTxt = "Ouch... that hurts!";
            }
        }
    }

    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        InventoryList<string> innventoryList = new InventoryList<string>();

        innventoryList.SetItem("Potion");
        Debug.Log(innventoryList.item);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelTxt);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU LOSE!"))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                catch (System.ArgumentException e)
                {

                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }

            }
        }
    }

    private static void Print(string logMessage)
    {
        Debug.Log(logMessage);
    }

    public void Initialize()
    {
        _state = "Manager initialized...";
        _state.FancyDebug();
        debug(_state);

        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlerPlayerJump;

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
    }

    public void HandlerPlayerJump()
    {
        debug("Player has jumped...");
    }

    private void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task");
    }

    public void PrintLootReport()
    {
        var currentIem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentIem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }

}
