using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        _guiManager.changeStateMenu += ChangeStateMenu;
    }


    private void Update()
    {
        if (!_blockMovment)
        {
            if (playerNumber == 1)
                PlayerOneMovment();
            else
                PlayerTwoMovement();
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

    private void PlayerTwoMovement()
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
            Bomb bombTemp = _bombPool.First();
            _bombPool.Remove(bombTemp);
            
            bombTemp.transform.parent = null;
            bombTemp.transform.position =
                new Vector3(
                    Mathf.RoundToInt(transform.position.x), 
                    bombPrefab.transform.position.y, 
                    Mathf.RoundToInt(transform.position.z));
            
            bombTemp.gameObject.SetActive(true);
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

    public void OnTriggerEnter(Collider collider) //�������� ������ � �������, ��� ������� ������� ��� ��������� ������ ������� ��� ������� � ���������� ������.
    {
        if (!_playerDead && collider.CompareTag("Explosion"))
        {
            _playerDead = true;
            ResetPositionPlayerDied(); //���������� ������� ������.
        }
    }

    public void ResetPositionPlayerDied() //��������� ������ ��������� ��� ����� � ��������� �����(�� 4) � ��������� ����.
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
