using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Akasha
{
    /// <summary>
    /// ReactiveEvent의 래퍼 역할을 하며, Actor 또는 Presenter에서 발생시키고
    /// Spirit 또는 Manager가 구독하여 반응하는 트리거 모델입니다.
    /// </summary>
    public abstract class BaseReactiveFlag
    {
        private readonly RxEvent command = new();

        /// <summary>
        /// 이벤트 발생
        /// </summary>
        public void Raise(object issuer)
        {
            command.Raise(issuer);
        }

        /// <summary>
        /// 이벤트 구독
        /// </summary>
        public void Subscribe(Action callback, object subscriber, int priority = 0)
        {
            command.Subscribe(callback, subscriber, priority);
        }

        public void Unsubscribe(Action callback)
        {
            command.Unsubscribe(callback);
        }
    }
}