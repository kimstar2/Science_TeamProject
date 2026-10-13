using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.KJY.Proto.Scripts
{
    public class ProtoPlayer : MonoBehaviour
    {
        private readonly int _xDirHash = Animator.StringToHash("xDir");

        //TODO 물리 작용
        [SerializeField] private float speed;
        private Rigidbody2D _rigidCompo;
        private float _xDir;


        private void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void OnMove(InputValue value)
        {
            _xDir = value.Get<Vector2>().x;
        }

        private void FixedUpdate()
        {
            _rigidCompo.linearVelocityX = _xDir * speed;
        }

        private Animator _animator;
        
        // TODO 애니메이션 구현
        private void LateUpdate()
        {
            _animator.SetFloat(_xDirHash,_xDir);
        }
    }
}
