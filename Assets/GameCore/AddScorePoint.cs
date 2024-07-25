using UnityEngine;

public class AddScorePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Player"))
        {
            Player player = Collider.gameObject.GetComponent<Player>();
            if (player._playerDead)
            {
                GlobalStateManager.Instance.PlayerDied(player.PlayerNumber);
                gameObject.SetActive(false);
            }
        }
    }
}
