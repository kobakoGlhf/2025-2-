﻿using System.Threading;
using UnityEngine;

namespace MFFrameWork
{
    public class EnemyManager : Character_B
    {

        [SerializeField] Transform _targetObj;
        [SerializeField] float _modeChangeDistanse;
        [SerializeField] float _time;//刈り
        [SerializeField] float _randomLeftMove;

        [SerializeField] EnemyState _testState;

        CancellationTokenSource _cancelTSource;
        EnemyState _state;


        [SerializeField, Tooltip("Enemyはこの距離を保とうとします。")] float _targetDistans;

        float _timer;
        Vector3 _randomDirection;
        public override void Start_S()
        {
            _state = _testState;
            TargetTransform = _targetObj;
            _characterMove.LockTarget = _targetObj;
            OnLookTarget();
            _characterMove.MoveSpeed *= 0.5f;
            ChangeRandomDirection();
        }
        void EnemyLogicChange()
        {
            if (!_targetObj) _state = EnemyState.Idol;
            else if ((_targetObj.position - transform.position).sqrMagnitude > _modeChangeDistanse * _modeChangeDistanse)
            {
                _state = EnemyState.Tracking;
                _characterMove.MoveSpeed *= 10f;
            }
            else
            {
                _characterMove.MoveSpeed *= 0.1f;
                _state = EnemyState.Attack;
            }
        }

        public void Update()
        {
            switch (_state)
            {
                case EnemyState.Tracking:
                    TrackMode();
                    break;
                case EnemyState.Attack:
                    AttackMode();
                    break;
                default:
                    break;
            }


            _timer += Time.deltaTime;
            if (_timer > 1.2f)
            {
                Debug.Log("------");
                _timer = 0;
                ChangeRandomDirection();
                OnAttack();
            }
        }
        void TrackMode()
        {
            var direction = new Vector2(_targetObj.position.x - transform.position.x, _targetObj.position.z - transform.position.z);
            if (direction.sqrMagnitude > 10 * 10) OnMove(direction);
            else CancelMove();

        }
        void AttackMode()
        {
            Vector3 toTarget = Vector3.zero;
            if (_targetObj)
                toTarget = _targetObj.position - transform.position;
            toTarget.y = 0;

            var moveVec = _randomDirection;

            moveVec.z = toTarget.magnitude - _targetDistans * 0.1f;
            var moveDic = Quaternion.LookRotation(toTarget) * moveVec ;

            OnMove(new Vector2(moveDic.x, moveDic.z));

        }
        void ChangeRandomDirection()
        {
            // ランダムな方向を設定 (-1〜1の範囲)
            _randomDirection = new Vector3(Random.Range(_randomLeftMove * -1, _randomLeftMove), 0, 0).normalized;
        }
    }
    enum EnemyState
    {
        Idol,
        Attack,
        Tracking,
        avoidance
    }
}
