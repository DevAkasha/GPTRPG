using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// BaseModel은 여러 ReactiveField를 캡슐화한 데이터 컨테이너입니다.
    /// 상호작용이 아닌 순수 상태만 정의합니다.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// 초기 상태 구성 및 ReactiveExpression 등록 등
        /// </summary>
        public virtual void OnInitialize() { }

        /// <summary>
        /// 구독 해제 및 리소스 정리 등
        /// </summary>
        public virtual void OnTerminate() { }
    }
}
