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
    private void Start()
    {
        Invoke("Explode", 3f); //Стандартный метод "Invoke" вызывает нами описанный метод "Explode" через 3 секунды после установки Бомбы
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

    private void OnTriggerEnter(Collider collider) //Детонация бомбы от взрывной волны.
    {
        if(!_exploded && collider.CompareTag("Explosion")) //Когда взрыв ксатается бомбы, активируем ее.
        {
            CancelInvoke("Explode"); //Деактивируем текущий таймер, что бы не создать двойной взрыв.
            Explode();  //Активируем без таймера, мгновенно.
        }

        if (collider.CompareTag("Bomb")) //Если две бомбы сталкиваются в одной точке, удаляем обу бомбы без детонации.
        {
            StartCoroutine(player.ReturnBombToPool(gameObject, 0f));
        }
    }
}
