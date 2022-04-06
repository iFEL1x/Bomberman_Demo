/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Bomb> _bombPool;

    private Rigidbody _rigidDody;
    private Vector3 _direction;
    private Transform _bombSpawn;
    private GUIManager _guiManager;
    private bool _blockMovment = true;
    public bool _playerDead = false;

    [Tooltip("Put child gameObject 'Restart position'")]
    [SerializeField] private List<GameObject> respawnPointArr;
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private int countBomb;

    [Range(1, 2)]
    [SerializeField] private int playerNumber = 1;
    [SerializeField] private float speed;

    public int PlayerNumber => playerNumber;

    private void Awake()
    {
        _rigidDody = GetComponent<Rigidbody>();
        _bombSpawn = transform.Find("bombSpawn");

        _bombPool = new List<Bomb>();
        for (int i = 0; i < countBomb; i++)
        {
            var bombTemp = Instantiate(bombPrefab, _bombSpawn);
            _bombPool.Add(bombTemp);
            bombTemp.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        _guiManager = GUIManager.Instance;

        _guiManager.changeStateMenu += ChangeStateMenu; //Отслеживанием состояние меню и блокирование управления игрокам.
    }


    private void Update()
    {
        if (!_blockMovment)
        {
            if (playerNumber == 1)
                PlayerOneMovment();
            else
                PlayerTwoMovment();
        } 
    }

    private void ChangeStateMenu(bool stateMenu)
    {
        _blockMovment = stateMenu;
    }

    private void PlayerOneMovment()
    {
        _direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _direction = Vector3.forward;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _direction = Vector3.back;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _direction = Vector3.left;
            transform.rotation = Quaternion.Euler(0, 240, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _direction = Vector3.right;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        _direction *= speed;
        _rigidDody.velocity = _direction;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
    }

    private void PlayerTwoMovment()
    {
        _direction = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _direction = Vector3.forward;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction = Vector3.back;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _direction = Vector3.left;
            transform.rotation = Quaternion.Euler(0, 240, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _direction = Vector3.right;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        _direction *= speed;
        _rigidDody.velocity = _direction;

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            DropBomb();
        }
    }

    private void DropBomb()
    {
        if (_bombPool.Count > 0)
        {
            Bomb bombTemp = _bombPool[0];
            _bombPool.Remove(bombTemp);
            bombTemp.gameObject.SetActive(true);
            bombTemp.transform.parent = null;
            bombTemp.transform.position =
                new Vector3(Mathf.RoundToInt(transform.position.x),
                bombPrefab.transform.position.y,
                Mathf.RoundToInt(transform.position.z));
        }
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

    public void OnTriggerEnter(Collider collider) //Коллизия взрыва с игроком, где считаем касание как порожение игрока обнуляя его позицию и начисление баллов.
    {
        if (!_playerDead && collider.CompareTag("Explosion"))
        {
            _playerDead = true;
            ResetPositionPlayerDied(); //Сбрасываем позицию игрока.
        }
    }

    public void ResetPositionPlayerDied() //Респавним игрока попавшего под бомбу в случайном месте(из 4) и начисляем очки.
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
