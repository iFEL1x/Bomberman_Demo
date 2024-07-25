using UnityEngine;

namespace GameCore
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private PlayerMoveController _playerMoveController;
        
        private bool _blockMovment = true;
        private Vector3 _direction;
        private GUIManager _guiManager;
        public bool _playerDead = false;
        
        private void Start()
        {
            _guiManager = GUIManager.Instance;

            _guiManager.changeStateMenu += ChangeStateMenu;
        }
        
        private void Update()
        {
            if (!_blockMovment)
            {
                PlayerMovement();
            } 
        }
        
        private void ChangeStateMenu(bool stateMenu)
        {
            _blockMovment = stateMenu;
        } 
        
        private void PlayerMovement()
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

            _playerMoveController.Move(_direction);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerMoveController.DropBomb();
            }
        }
    }
}
