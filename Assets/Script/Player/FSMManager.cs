using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK
}

public class FSMManager : MonoBehaviour {

    public PlayerState currentState;
    public PlayerState startState;
    public Transform mark;

    Dictionary<PlayerState, PlayerFSMState> states
        = new Dictionary<PlayerState, PlayerFSMState>();

    private void Awake()
    {
        mark = GameObject.FindGameObjectWithTag("Marker").transform;

        states.Add(PlayerState.IDLE, GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());
    }

    public void SetState(PlayerState newState)
    {
        foreach (PlayerFSMState fsm in states.Values)
        {//foreach 안에 들어있는 것들을 전부 실행. 코드 추가시 유연한 대처 가능.
            fsm.enabled = false;
        }
   
        states[newState].enabled = true;
    }

    void Start () {
        SetState(startState);
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {//마우스로 클릭한곳에 광선을 쏘아 마우스로 클릭한 좌표 위치값 구하기.
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // 메인카메라에서 가져옴
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 1000f))
            {
                mark.position = hit.point;
                Debug.Log("Click " + hit.point);

                SetState(PlayerState.RUN);
            }
        }


    }
}