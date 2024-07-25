using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float Delay = 1f;

    private void Start()
    {
        Destroy(gameObject, Delay);
    }
}
