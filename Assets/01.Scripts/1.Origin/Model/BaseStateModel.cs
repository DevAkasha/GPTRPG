using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Akasha
{
    /// <summary>
    /// 상태와 트리거를 함께 다루는 통합형 Reactive 모델입니다.
    /// Presenter 또는 Actor에서 사용되며, Spirit 또는 Manager가 구독합니다.
    /// </summary>
    public abstract class ReactiveStateModel
    {
        private readonly ReactiveCommand command = new();

        public void Raise(object issuer) => command.Raise(issuer);
        public void Subscribe(Action callback, object subscriber, int priority = 0)
            => command.Subscribe(callback, subscriber, priority);
        public void Unsubscribe(Action callback) => command.Unsubscribe(callback);

        public virtual void OnInitialize() { }
        public virtual void OnTerminate() { }
    }
}