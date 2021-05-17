using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1; //적 공격력
    [SerializeField]
    private int scorePoint = 100; // 적 처치시 획득 점수
    [SerializeField]
    private GameObject explosionPrefabs; // 폭발 효과
    [SerializeField]
    private GameObject[] itemPrefabs; // 적을 죽였을 때 획득 가능한 아이템

    private PlayerController playerController; // 플레이어의 점수(Score) 정보에 접근하기 위해

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

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
        Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
        //일정 확률로 아이템 생성
        SpawnItem();
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        int spawnItem = UnityEngine.Random.Range(0, 100);
        if(spawnItem < 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
        else if(spawnItem < 15)
        {
            Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 30)
        {
            Instantiate(itemPrefabs[2], transform.position, Quaternion.identity);
        }
    }

}
