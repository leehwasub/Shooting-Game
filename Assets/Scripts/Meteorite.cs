using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private GameObject explosionPrefab; // 폭발 효과
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 운석에 부딪힌 오브젝트의 태그가 Player 이면
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //운석 사망
            //Destroy(gameObject);
            OnDie();
        }
    }

    public void OnDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
