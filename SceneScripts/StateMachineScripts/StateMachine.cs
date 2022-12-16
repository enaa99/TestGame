using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public State<T> CurState { get; protected set; }

    protected PlayerController player;

    private T m_sender;

    //»ý¼ºÀÚ
    public StateMachine(T sender, State<T> state)
    {
        m_sender = sender;
    }

    public void SetState(State<T> state)
    {
        if (m_sender == null)
        {
            Debug.LogError("invalid m_sender");
            return;
        }

        if (CurState == state)
        {
            Debug.LogWarningFormat("Already Define State - {0}", state);
            return;
        }

        if (CurState != null)
            CurState.OnExit(m_sender);

        CurState = state;

        if (CurState != null)
            CurState.OnEnter(m_sender);
    }

    public void OnFixedUpdate()
    {
        if (m_sender == null)
        {
            Debug.LogError("invalid m_sener");
            return;
        }
        CurState.OnFixedUpdate(m_sender);
    }

    public void OnUpdate()
    {
        if (m_sender == null)
        {
            Debug.LogError("invalid m_sener");
            return;
        }
        CurState.OnUpdate(m_sender);
    }



}