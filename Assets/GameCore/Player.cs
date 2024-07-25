using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Bomb> _bombPool;

    private Transform _bombSpawn;
    public bool _playerDead = false;

    [Tooltip("Put child gameObject 'Restart position'")]
    [SerializeField] private List<GameObject> respawnPointArr;
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private int countBomb;

    [Range(1, 2)]
    [SerializeField] private int playerNumber = 1;
    [SerializeField] private float countdown;

    public int PlayerNumber => playerNumber;

    private void Awake()
    {
        _bombSpawn = transform.Find("bombSpawn");

        _bombPool = new List<Bomb>();
        for (int i = 0; i < countBomb; i++)
        {
            var bombTemp = Instantiate(bombPrefab, _bombSpawn);
            _bombPool.Add(bombTemp);
            bombTemp.gameObject.SetActive(false);
        }
    }

    private IEnumerator ExplosionBomb(Bomb bomb)
    {
        yield return new WaitForSeconds(countdown);
        if(!bomb.Exploded)
            bomb.Explode();
    }

    public IEnumerator ReturnBombToPool(GameObject bomb, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bomb);
        if (_bombPool.Count < countBomb)
        {
            var bombTemp = Instantiate(bombPrefab, _bombSpawn);
            _bombPool.Add(bombTemp);
            bombTemp.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!_playerDead && collider.CompareTag("Explosion"))
        {
            _playerDead = true;
            ResetPositionPlayerDied();
        }
    }

    private void ResetPositionPlayerDied()
    {
        GameObject respawnRandom;
        if (_playerDead)
        {
            respawnRandom = respawnPointArr[Random.Range(0, 3)];
            respawnRandom.gameObject.SetActive(true);
            gameObject.transform.position = respawnRandom.transform.position;
            StartCoroutine(PlayerReset());
        }
    }

    private IEnumerator PlayerReset()
    {
        yield return new WaitForSeconds(0.1f);
        _playerDead = false;
    }
}
