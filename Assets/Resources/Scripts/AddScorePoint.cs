using System.Collections;
using System.Collections.Generic;
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
                GlobalStateManager.Instance.PlayerDied(player.PlayerNumber); //Начисление баллов.
                gameObject.SetActive(false);
            }
        }
    }
}
