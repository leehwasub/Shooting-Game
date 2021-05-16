using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 10; // 최대 체력
    private float currentHP; // 현재 체력
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP; // maxHP 변수에 접근할 수 있는 프로퍼티 (Get만 가능)
    public float CurrentHP => currentHP; // currentHP 변수에 접근할 수 있는 프로퍼티 (Get만 가능)

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage 만큼 감소
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)
        {
            Debug.Log("플레이어 사망");
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

}
