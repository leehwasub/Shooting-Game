using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType {Stage = 0, Boss }

public class BGMController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips; // 배경음악 파일목록
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeBGM(BGMType index)
    {
        //재생 중인 배경음악 정지
        audioSource.Stop();
        //배경음악 파일 목록에서 index번째 배경음악으로 파일 교체
        audioSource.clip = bgmClips[(int)index];
        // 바뀐 배경음악 재생
        audioSource.Play();
    }

}
