using System;
using UnityEngine;

namespace GameCore
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody _rigidDody;

        private void Awake()
        {
            _rigidDody = GetComponent<Rigidbody>();
        }

        public void DropBomb()
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
                StartCoroutine(ExplosionBomb(bombTemp));
            }
        }

        public void Move(Vector3 direction)
        {            
            direction *= speed;
            _rigidDody.velocity = direction;
        }
    }
}
