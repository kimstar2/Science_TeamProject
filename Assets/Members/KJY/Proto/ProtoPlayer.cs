using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.KJY.Proto
{
    public class ProtoPlayer : MonoBehaviour
    {
        //TODO 물리 작용
        private Rigidbody2D _rigidCompo;
        private float _xDir;
        
        
        private void OnMove(InputValue value)
        {
            _xDir = value.Get<Vector2>().x;
        }

        private void FixedUpdate()
        {
            //TODO 속도 적용시켜서 움직임 구현 해보세요
        }


        //TODO 움직임
        //TODO 애니메이션
    }
}
