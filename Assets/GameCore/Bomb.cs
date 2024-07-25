using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private LayerMask levelMask;
    [SerializeField] private string hasPlayerNameThisBomb;

    private bool _exploded = false;


    private void Awake()
    {
        player = GameObject.Find(hasPlayerNameThisBomb).GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(!_exploded && collider.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }

        if (collider.CompareTag("Bomb"))
        {
            StartCoroutine(player.ReturnBombToPool(gameObject, 0f));
        }
    }
    
    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(CreatExplosions(Vector3.forward));
        StartCoroutine(CreatExplosions(Vector3.right));
        StartCoroutine(CreatExplosions(Vector3.back));
        StartCoroutine(CreatExplosions(Vector3.left));

        GetComponent<MeshRenderer>().enabled = false;
        _exploded = true;
        transform.Find("Collider").gameObject.SetActive(false);

        StartCoroutine(player.ReturnBombToPool(gameObject, 0.3f));
    }

    private IEnumerator CreatExplosions(Vector3 direction)
    {
        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit;

            Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }

            else
                break;

            yield return new WaitForSeconds(0.25f);
        }
    }
}
