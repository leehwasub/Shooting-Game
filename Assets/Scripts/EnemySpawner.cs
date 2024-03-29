﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData; // 적 생성을 위한 스테이지 크기 정보
    [SerializeField]
    private GameObject enemyPrefab; // 복제해서 생성할 적 캐릭터 프리팸
    [SerializeField]
    private GameObject enemyHPSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹
    [SerializeField]
    private Transform canvasTransform; // UI를 표현하는 canvs 오브젝트의 Transform
    [SerializeField]
    private BGMController bgmController; // 배경음악 설정 (보스 등장시 변경)
    [SerializeField]
    private GameObject textBossWarning; // 보스 등장 텍스트 오브젝트
    [SerializeField]
    private GameObject panelBossHP; // 보스 체력 패널 오브젝트
    [SerializeField]
    private GameObject boss; // 보스 오브젝트
    [SerializeField]
    private float spawnTime; // 생성 주기
    [SerializeField]
    private int maxEnemyCount = 100; // 현재 스테이지의 최대 적 생성 숫자

    private void Awake()
    {
        textBossWarning.SetActive(false);
        panelBossHP.SetActive(false);
        boss.SetActive(false);

        StartCoroutine("SpawnEnemy");    
    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0; // 적 생성 숫자 카운트용 변수

        while (true)
        {
            // x 위치는 스테이지의 크기 범위 내에서 임의의 값을 선택
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            // 적 캐릭터 생성
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            // 적 체력을 나타내는 Slider UI 생성 및 설정
            SpawnEnemyHPSlider(enemyClone);

            currentEnemyCount++;

            if(currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            // spawnTime 만큼 대기
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // canvas의 자식으로 설정
        sliderClone.transform.SetParent(canvasTransform);
        // 계층 설정을 바뀐크기를 (1, 1, 1) 로 설정
        sliderClone.transform.localScale = Vector3.one;
        //Slider UI 가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Sllider 에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMType.Boss);
        
        textBossWarning.SetActive(true);
        
        yield return new WaitForSeconds(1.0f);
        
        textBossWarning.SetActive(false);

        panelBossHP.SetActive(true);

        boss.SetActive(true);

        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }

}
