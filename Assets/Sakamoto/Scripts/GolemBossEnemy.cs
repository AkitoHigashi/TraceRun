using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GolemBossEnemy : BossEnemyBase
{
    [SerializeField] Animator _bossAnimator;
    [SerializeField] private float _coolTime = 3f;
    [SerializeField] private float _moveSpeed = 5f;
    /// <summary>
    /// �v���C���[�ɋ߂Â��ő勗��
    /// </summary>
    [SerializeField] private float _approachDistance = 3f;
    [SerializeField] private Transform _player;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private BossState _bossState = BossState.Idle;

    private void Start()
    {
        _initialPosition = transform.position;
        StartCoroutine(Attack());
    }
    public IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime);

            int rand = Random.Range(0, 2);
            _bossAnimator.SetInteger("attackRand", rand);
            //_bossAnimator.SetTrigger("StartAttack");

            // �U���A�j���[�V�������I���̂�҂iAnimator������AnimationEnd���Ăԁj
            yield return new WaitUntil(() => _bossState == BossState.Idle);
        }
    }

    //Animation Event ����ĂԊ֐�
    public void OnAttackAnimationEnd()
    {
        _bossState = BossState.Idle;
    }
    // �v���C���[�Ɍ������Ē����ړ�����i�A�j���[�V�����C�x���g�ŌĂԁj
    public void Walk()
    {
        if (_player == null) return;

        _targetPosition = _player.position;
        Vector3 direction = (_targetPosition - transform.position).normalized;

        StartCoroutine(MoveTowardDirection(direction));
    }

    private IEnumerator MoveTowardDirection(Vector3 direction)
    {
        _isMoving = true;
        _bossState = BossState.Attack;

        float distanceMoved = 0f;

        while (distanceMoved < _approachDistance)
        {
            float step = _moveSpeed * Time.deltaTime;
            transform.position += direction * step;
            distanceMoved += step;

            yield return null;
        }
        _isMoving = false;

        _bossAnimator.SetTrigger("DoShortAttack");
    }

    // �U����Ɍ��̈ʒu�֖߂�i�A�j���[�V�����C�x���g�ŌĂԁj
    public void ReturnPosition()
    {
        StartCoroutine(ReturnToStart());
    }

    private IEnumerator ReturnToStart()
    {
        _bossAnimator.SetTrigger("Return");
        _bossState = BossState.Attack;

        while (Vector3.Distance(transform.position, _initialPosition) > 0.1f)
        {
            Vector3 dir = (_initialPosition - transform.position).normalized;
            transform.position += dir * _moveSpeed * Time.deltaTime;
            yield return null;
        }

        transform.position = _initialPosition;

        // Idle�ɖ߂邽�߂�Trigger
        _bossAnimator.SetTrigger("AnimationEnd");

        _bossState = BossState.Idle;
    }
}
