using UnityEngine;
using UnityEngine.UI;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance { get; set; }

    private int _scrorePlayer1;
    private int _scrorePlayer2;

    public int ScoreP1 => _scrorePlayer1;
    public int ScoreP2 => _scrorePlayer2;

    [SerializeField] private Text scroreTxtPlayer1;
    [SerializeField] private Text scroreTxtPlayer2;


    private void Awake()
    {
        Instance = this;
    }

    public void PlayerDied(int playerNumber)
    {
        if(playerNumber == 1)
        {
            _scrorePlayer2++;
            scroreTxtPlayer2.text = _scrorePlayer2.ToString();
        }
        else if (playerNumber == 2)
        {
            _scrorePlayer1++;
            scroreTxtPlayer1.text = _scrorePlayer1.ToString();
        }
    }
}
