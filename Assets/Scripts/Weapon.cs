using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // 공격할 때 생성되는 발사체 프리팹
    [SerializeField]
    private float attackRate = 0.1f; // 공격 속도
    [SerializeField]
    private int maxAttackLevel = 3; // 공격 최대 래벨
    private int attackLevel = 1; // 공격 래벨
    private AudioSource audioSource;

    [SerializeField]
    private GameObject boomPrefab; // 폭탄 프리팹
    private int boomCount = 3; // 생성 가능한 폭탄

    public int AttackLevel
    {
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);
        get => attackLevel;
    }

    public int BoomCount
    {
        set => boomCount = Mathf.Max(0, value);
        get => boomCount;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }

    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }

    private IEnumerator TryAttack()
    {
        while (true)
        {
            // 발사체 오브젝트 생성
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            AttackByLevel();

            audioSource.Play();

            // attackRate 시간 만큼 대기
            yield return new WaitForSeconds(attackRate);
        }
    }

    private void AttackByLevel()
    {
        GameObject clonePRojecttile = null;

        switch (attackLevel)
        {
            case 1:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(projectilePrefab, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clonePRojecttile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clonePRojecttile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));
                clonePRojecttile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clonePRojecttile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }

    public void StartBoom()
    {
        if(boomCount > 0)
        {
            boomCount--;
            Instantiate(boomPrefab, transform.position, Quaternion.identity);
        }
    }

}
