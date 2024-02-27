using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 종료처리에 들어갔는지 확인하는 변수
    /// </summary>
    private static bool isShutDown = false;

    /// <summary>
    /// 싱글톤의 객체
    /// </summary>
    private static T instance;

    /// <summary>
    /// 싱글톤 객체를 읽기 위한 프로퍼티
    /// </summary>
    public static T Instance
    {
        get
        {
            if(isShutDown)
            {
                Debug.LogWarning("싱글톤은 삭제 중입니다");
                return null;
            }
            if(instance == null)
            {
                T singleton = FindAnyObjectByType<T>(); // 싱글톤 오브젝트 찾기

                if(singleton == null) // 없으면 임시 오브젝트 생성
                {
                    GameObject obj = new GameObject();
                    obj.name = "Singleton";
                    singleton = obj.AddComponent<T>();
                }
                instance = singleton;
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    // flag

    /// <summary>
    /// 싱글톤이 초기화 되어있는지 확인하는 bool
    /// </summary>
    bool isInitionalize = false;

    void Awake()
    {
        if(instance == null) // 씬에 배치된 다른 싱글톤이 없을 때
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else if(instance != null) // 씬에 싱글톤이 배치되 있으면
        {
            Destroy(this.gameObject); // 자신 파괴
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 씬 로드될때 호출되는 함수
    /// </summary>
    /// <param name="scene">씬 정보</param>
    /// <param name="mode">로딩 모드</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!isInitionalize)
        {
            OnPreInitialize();
        }
        if(mode != LoadSceneMode.Additive)
        {
            OnInitialize();
        }
    }

    /// <summary>
    /// 씬이 로드되고 한번 실행될 함수
    /// </summary>
    protected virtual void OnPreInitialize()
    {
        isInitionalize = true;
    }

    /// <summary>
    /// 싱글톤이 만들어지고 씬이 변경될 때 마다 실행될 함수
    /// </summary>
    protected virtual void OnInitialize()
    {
    }

    /// <summary>
    /// 프로그램 종료될 때 실행될 함수
    /// </summary>
    private void OnApplicationQuit()
    {
        isShutDown = true;
    }
}
