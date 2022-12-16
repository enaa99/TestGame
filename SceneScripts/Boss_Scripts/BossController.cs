using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
using Cinemachine.PostFX;

public class BossController : MonoBehaviour
{

    public enum eState
    {
        Start,              // ������ ����
        CastSpell,          // ��ų���
        SkillElectric,      // ��ų���
        Chase,              // ����
        InviewCheck,        // �þ�üũ
        Wander,             // ��ȸ
        BiteAttack,         // ���ݸ��
        BreathFire,         // ���ݸ��
        TailAttack,         // ���ݸ��
        FootAttack,         // ���ݸ��
        FireAround,         // ���ݸ��
        FlyUp,              // ���� ���
        FlyAround,          // ���Ƽ� �ൿ
        LandingPoint,       // ���� ��ġ ã��
        Landing,            // ����
        Dead,               // ���
        AttackState,        // ���� ����
        GroundState,        // ���� ����
        SkyState            // ���� ����
    }


    public Transform ragDoll;
    public GameObject track;
    public Collider explosionCollider;
    public Collider legCollider;
    public Collider breathCollier;
    public Collider headCollider;
    public GameObject leftPos;
    public GameObject rightPos;
    public GameObject firePos;
    public PlayerController _player;
    public CinemachineVirtualCamera bossCam;
    public CinemachineVirtualCamera startCam;
    public CinemachineDollyCart dollyCart;
    public GameObject playObj;

    [HideInInspector]
    public CinemachineComponentBase componentBase;
    [HideInInspector]
    public CinemachinePostProcessing postProcessing;

    [HideInInspector]
    public GameObject[ , ] explosion;
    [HideInInspector]
    public int spawnCountExplosion = 5;
    [HideInInspector]
    public int explosionCount = 6;

   
    [HideInInspector]
    public GameObject[] electricPool_1;
    [HideInInspector]
    public GameObject[] electricPool_2;
    [HideInInspector]
    public int spawnCountElectric = 10;

    [HideInInspector]
    public StateMachine<BossController> m_sm;

    [HideInInspector]
    public Dictionary<eState, State<BossController>> 
        m_states = new Dictionary<eState, State<BossController>>();


    [HideInInspector]
    public string curAnimName;
    [HideInInspector]
    public bool AnimName => b_anim.GetCurrentAnimatorStateInfo(0).IsName(curAnimName);
    [HideInInspector]
    public float AnimTime => b_anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    [HideInInspector]
    public Rigidbody b_rigid;
    [HideInInspector]
    public Boss_Pattern pattern;
    [HideInInspector]
    public Animator b_anim;
    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public bool doPattern;
    [HideInInspector]
    public bool actNext;
    [HideInInspector]
    public Vector3 destination;
    [HideInInspector]
    public Vector3 landPos;
    [HideInInspector]
    public int choosePattern = 0;
    [HideInInspector]
    public float attackDist = 12f;
    [HideInInspector]
    public float landDist = 16f;


    private void Start()
    {
        
        pattern = transform.GetComponent<Boss_Pattern>();
        b_rigid = GetComponent<Rigidbody>();
        b_rigid.constraints = RigidbodyConstraints.FreezeRotationX
                               | RigidbodyConstraints.FreezeRotationY 
                               | RigidbodyConstraints.FreezeRotationZ;

        b_anim = GetComponent<Animator>();

        m_states.Add(eState.Start,           new BossStart(this, _player));
        m_states.Add(eState.Wander,          new BossWander(this, _player));
        m_states.Add(eState.SkillElectric,   new BossSKillElectric(this, _player));
        m_states.Add(eState.CastSpell,       new BossCastSpell(this, _player));
        m_states.Add(eState.Chase,           new BossChase(this, _player));
        m_states.Add(eState.BiteAttack,      new BossBite(this, _player));
        m_states.Add(eState.TailAttack,      new BossTaliAttack(this, _player));
        m_states.Add(eState.FlyAround,       new BossFlyAround(this, _player));
        m_states.Add(eState.FootAttack,      new BossFoot(this, _player));
        m_states.Add(eState.FireAround,      new BossFireAround(this, _player));
        m_states.Add(eState.BreathFire,      new BossBreathFire(this, _player));
        m_states.Add(eState.FlyUp,           new BossFlyUp(this, _player));
        m_states.Add(eState.LandingPoint,    new BossLandingPoint(this, _player));
        m_states.Add(eState.Landing,         new BossLanding(this, _player));
        m_states.Add(eState.GroundState,     new BossGroundState(this, _player));
        m_states.Add(eState.SkyState,        new BossSkyState(this, _player));
        m_states.Add(eState.AttackState,     new BossAttackState(this, _player));
        m_states.Add(eState.InviewCheck,     new BossInViewCheck(this, _player));
        m_states.Add(eState.Dead,            new BossDead(this, _player));

        electricPool_1 = new GameObject[spawnCountElectric];
        electricPool_2 = new GameObject[spawnCountElectric];

        explosion = new GameObject[explosionCount,spawnCountExplosion];

      

        m_sm = new StateMachine<BossController>(this, null);
        ChangeState(eState.Start);

    }


    private void FixedUpdate()
    {
        m_sm.OnFixedUpdate();

    }

    private void Update()
    {
        m_sm.OnUpdate();

    }
    // ������Ʈ�� ���¸� ������ �ش�
    public void ChangeState(eState state)
    {
        Debug.LogWarning(state);
        m_sm.SetState(m_states[state]);
    }
    
    // ���� ������Ʈ�� ������ �޴´�
    public bool IsState(eState state)
    {
        return m_sm.CurState == m_states[state];
    }

    // �ִϸ��̼��� ������ string���� ���� �� �ش�
    // �ִϸ��̼��� ���൵�� 90%�� �ѱ�� InviewCheck�� �ٲ��ش�
    public void CheckAnimation(string name)
    {
        curAnimName = name;

        if (AnimName && AnimTime > 0.9f)
        {
            ChangeState(eState.InviewCheck);
        }
    }

    // �Ÿ��� ���� üũ���ش�
    public void CheckDistanceGround()
    {
        //GC(GarbageCollection) ������ ���� SqrMagnitude�� ���
        float distance = Vector3.SqrMagnitude(this.transform.position - _player.transform.position);

        if(distance < attackDist*attackDist && IsState(eState.Wander))
        {
            ChangeState(eState.InviewCheck);
        }

        if (distance > attackDist * attackDist && !IsState(eState.Chase)
            && !IsState(eState.Wander))
        {
            ChangeState(eState.Chase);
        }

        else if (distance <= attackDist * attackDist && !IsState(eState.AttackState))
        {
            ChangeState(eState.AttackState);
        }

        else if (distance <= attackDist * attackDist && IsState(eState.AttackState))
        {
            actNext = true;
        }

    }

    // ���� ����Ʈ�� �����ߴ��� ���� üũ
    public void CheckDistanceLand()
    {
        this.destination.y = this.transform.position.y;
        float dist = Vector3.Distance(destination, this.transform.position);
        if (dist <= landDist)
        {
            ChangeState(eState.Landing);
        }
    }


    // ȸ�� ������ ������ �ش�
    public void TurnTo(float rotSpeed, Vector3 Pos)
    {
        Quaternion rot = Quaternion.LookRotation(Pos - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, rotSpeed * Time.deltaTime);
    }

}
