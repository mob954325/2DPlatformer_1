using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// UI���� ��ũ��Ʈ
/// </summary>
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    [Header("Gauge UI")]
    PlayerBar playerBar;
    EnemyBar enemyBar;

    [Header("Panels")]
    ResultPanel resultPanel;
    [HideInInspector]public bool isPlayerInteraction = false;

    void Awake()
    {
        Instance = this;
        OnInitialize();
    }

    protected void OnInitialize()
    {
        playerBar = FindAnyObjectByType<PlayerBar>();
        enemyBar = FindAnyObjectByType<EnemyBar>();

        resultPanel = FindAnyObjectByType<ResultPanel>();

        Cursor.lockState = CursorLockMode.None; // Ŀ�� ��������
        Cursor.visible = true; // Ŀ�� �����ֱ�
    }

    /// <summary>
    /// ��� �г��� �����ִ� �Լ�
    /// </summary>
    public void ShowResult(bool isPlayer = false)
    {
        Cursor.lockState = CursorLockMode.None; // Ŀ�� ��������
        Cursor.visible = true; // Ŀ�� �����ֱ�
        resultPanel.SetResultText(isPlayer); // ��������
        resultPanel.Show(); // �����ֱ�

        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
