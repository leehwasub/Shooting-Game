using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {CircleFire = 0, SingleFireToCenterPosition}

public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // 공격할 때 생성되는 발사체 프리팹

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f; // 공격 주기
        int count = 30; // 발사체 생성 개수
        float intervalAngle = 360 / count; // 발사체 사이의 각도
        float weightAngle = 0; // 가중되는 각도

        while (true)
        {
            for(int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                float angle = weightAngle + intervalAngle * i;
                
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            weightAngle += 1;

            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attackRate = 0.1f;
        while (true)
        {
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector3 direction = (targetPosition - clone.transform.position).normalized;
            clone.GetComponent<Movement2D>().MoveTo(direction);
            yield return new WaitForSeconds(attackRate);
        }
    }

}
