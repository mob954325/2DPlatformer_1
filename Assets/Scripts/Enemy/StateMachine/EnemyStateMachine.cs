using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    /// <summary>
    /// State가 실행되었는지 확인하는 변수 (false : 실행 x , true : 실행됨)
    /// </summary> 
    bool isEnter = false;

    /// <summary>
    /// 상태머신에서 죽었는지 확인하는 변수
    /// </summary>
    bool isDead = false; 

    /// <summary>
    /// 현재 상태 스크립트 ( Null이면 행동을 안함 )
    /// </summary>
    public EnemyStateBase currentState;

    void Start()
    {
        // init
        if(currentState != null) // currentState가 있으면 첫 실행문 실행
        {
            isEnter = true;
            currentState.enemy = GetComponent<EnemyBase>(); // state의 enemy 컴포넌트 받기
        }
    }

    void FixedUpdate()
    {
        RunStateMachine();
        OnDieState(); // 죽으면 사망 상태로 이동
    }


    /// <summary>
    /// 해당 State에서 실행할 내용 (상태를 변경할려면 이 함수에서 리턴해야함)
    /// </summary>
    private void RunStateMachine()
    {
        if (isEnter)
        {
            isEnter = false;
            currentState?.EnterCurrentState();
        }

        EnemyStateBase nextState = currentState?.RunCurrentState();

        if(nextState != null)
        {
            ChangeStateMachine(nextState);
        }
    }

    /// <summary>
    /// CurrentState를 nextState로 바꾸는 함수
    /// </summary>
    /// <param name="nextState">바뀔 state이름</param>
    private void ChangeStateMachine(EnemyStateBase nextState)
    {
        if (currentState != nextState) // 매개변수와 현재 상태가 같지 않다 -> state 변환 전
        {
            isEnter = true;
            currentState?.ExitCurrentState();
        }

        currentState = nextState; // 다음 state로 변경
        currentState.enemy = GetComponent<EnemyBase>(); // state의 enemy 컴포넌트 받기
    }

    private void OnDieState()
    {
        if(currentState.enemy.IsDie && !isDead)
        {
            isEnter = true; // 진입여부 활성화
            isDead = true;

            // 상태 변경
            currentState = currentState.enemy.SetEnemyState(EnemyBase.State.Death);
            currentState.enemy = GetComponent<EnemyBase>(); // state의 enemy 컴포넌트 받기
        }
    }
}
