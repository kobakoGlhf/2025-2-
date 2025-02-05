//using MFFrameWork.Utilities;
//using UnityEngine;

//namespace MFFrameWork
//{
//    public class PlayerMove_Dev : MonoBehaviour, IMove, IJump, IDush
//    {
//        //�S�ẴC���^�[�t�F�[�X�ŋ��ʂ̕���
//        Rigidbody _rb;
//        bool _isGround;
//        bool _isDisableMove = true;

//        bool isGround
//        {
//            get => _isGround; set
//            {
//                if (_isGround != value) Debug.Log($"isGround is {_isGround}");
//                _isGround = value;
//            }
//        }

//        [SerializeField] float _slopelimit = 45;
//        [SerializeField] float _groundDistans = 0.5f;
//        //�p�����[�^�[
//        [Space(10), Header("Movement parameter")]
//        [SerializeField] float _moveSpeed = 10;
//        [SerializeField] float _jumpPower = 10;
//        [Space]
//        [SerializeField] float _dushSpeed = 10;
//        [SerializeField] float _dushCoolTime = 10;

//        public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
//        public float JumpPower { get => _jumpPower; set => _jumpPower = value; }
//        public float DushSpeed { get => _dushSpeed; set => _dushSpeed = value; }


//        private void Start()
//        {
//            _rb = GetComponent<Rigidbody>();
//        }
//        private void Update()
//        {
//            Debug.DrawRay(transform.position, Vector3.down * _groundDistans, Color.blue);
//        }

//        public void Init(Rigidbody rb, float moveSpeed = 10, float distanse = 1,
//            float jumpPower = 100, float dushPower = 100)
//        {
//            _rb = rb;
//            _moveSpeed = moveSpeed;
//            _groundDistans = distanse;
//            _jumpPower = jumpPower;
//            _dushSpeed = dushPower;
//        }

//        void IMove.Move(Vector3 moveDirection)//ToDo:HERE�@Raycast��SphereChast�ɕύX������
//        {
//            if (!_isDisableMove) return;
//            moveDirection = moveDirection.normalized;
//            Debug.DrawRay(transform.position, moveDirection, Color.green);
//            //Ray moveRay = new Ray(transform.position, moveDirection);
//            var groundHit = GroundCheck();

//            if (_isGround)//�ڒn���̈ړ�����
//            {
//                var groundParallel = Vector3.Cross(groundHit.normal, Vector3.Cross(moveDirection, Vector3.up));

//                //�ړ������ɑ΂��Ẵf�o�b�O
//                Debug.DrawLine(transform.position + groundParallel, groundParallel * 100, Color.red);

//                var moveVector = groundParallel * _moveSpeed;
//                _rb.AddForce(moveVector);
//                //�ړ����x�̐���
//                if (_rb.linearVelocity.sqrMagnitude > _moveSpeed * _moveSpeed)
//                {
//                    _rb.linearVelocity = _rb.linearVelocity.normalized * _moveSpeed;
//                }
//                //_rb.linearVelocity = moveVector;
//            }
//            else//�󒆂ł̈ړ�����
//            {
//                _rb.AddForce(moveDirection * _moveSpeed * 100);
//                //�ړ����x�̐���
//                if (_rb.linearVelocity.sqrMagnitude > _moveSpeed * _moveSpeed)
//                {
//                    _rb.linearVelocity = _rb.linearVelocity.normalized * _moveSpeed;
//                }
//                //if (moveDirection.sqrMagnitude > 0)
//                //{
//                //    moveDirection *= _moveSpeed;
//                //    moveDirection.y = _rb.linearVelocity.y;
//                //    _rb.linearVelocity = moveDirection;
//                //}
//            }
//        }
//        void IJump.Jump()
//        {
//            if (!_isDisableMove||!_isGround) return;
//            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
//        }
//        async void IDush.Dush(Vector3 moveDirection)
//        {
//            if (!_isDisableMove) return;
//            else if (moveDirection.sqrMagnitude == 0) return;

//            _rb.linearVelocity = Vector3.zero;
//            _rb.AddForce(moveDirection.normalized * DushSpeed, ForceMode.Impulse);

//            _isDisableMove = false;
//            await Pausable.PausableWaitForSeconds(_dushCoolTime);
//            _isDisableMove = true;
//        }
//        /// <summary>
//        /// world��-y������ray���΂��B
//        /// </summary>
//        /// <returns></returns>
//        RaycastHit GroundCheck()//ToDo:HERE �ڒn������X�t�B�A�L���X�g�Ȃǂł��ꂢ�ɂƂ肽���B
//        {
//            Debug.DrawRay(transform.position, Vector3.down * _groundDistans, Color.blue);
//            Ray underRay = new Ray(transform.position, transform.up * -1);
//            _isGround = Physics.Raycast(underRay, out RaycastHit groundHit, _groundDistans);
//            if (Vector3.Dot(groundHit.normal, Vector3.up) < _slopelimit * Mathf.Deg2Rad)
//            {
//                _isGround = false;
//            }
//            return groundHit;
//        }
//    }
//}
