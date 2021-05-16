using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecttile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 발사체에 부딪힌 오브젝트의 태그가 Enemy이면
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnDie();
            Destroy(gameObject);
        }
    }
}
