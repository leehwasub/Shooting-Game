using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1; //적 공격력
    [SerializeField]
    private int scorePoint = 100; // 적 처치시 획득 점수
    private PlayerController playerController; // 플레이어의 점수(Score) 정보에 접근하기 위해

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적에게 부딪힌 오브젝트의 태그가 "Player" 이면
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            // 적 사망
            OnDie();
        }
    }

    public void OnDie()
    {
        playerController.Score += scorePoint;
        Destroy(gameObject);
    }

}
