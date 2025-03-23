using System;
using System.Collections.Generic;

namespace Akasha
{
    public enum RxRelationType
    {
        Functional,         // 상태 계산 목적 → Entity, Part, Screen
        InteractLogical,    // 상향식 반응 목적 → Actor, Presenter, Indicator
        ControlLogical      // 하향식 제어 이벤트 수신 → Spirit, Manager
    }

    public enum ListChangeType { Add, Remove, Insert, Replace, Clear }

    public interface IReactiveReader
    {
        void SubscribeRaw(Action onChanged, object subscriber, RxRelationType type);
        void UnsubscribeRaw(Action onChanged);
    }

    public interface IReactiveReader<T> : IReactiveReader
    {
        T Value { get; }
        void Subscribe(Action<T> callback, object subscriber, RxRelationType type, int priority = 0);
        void Unsubscribe(Action<T> callback);
    }

    public interface IReactiveWriter<T>
    {
        T Value { set; }
    }
    public interface IReactiveEventIssuer { }
    public interface IReactiveValue<T> : IReactiveReader<T>, IReactiveWriter<T> { }

    public interface IGameEventManager { }
  
    public interface IBindableMono { }
    public interface IIndicator 
    { 
        void Refresh(); 
    }

    public interface IControlLogicalSubscriber { }
    public interface IInteractLogicalSubscriber { }
    public interface IFunctionalSubscriber { }

    public static class ReactiveContext
    {
        public static bool IsPaused { get; private set; } = false;

        public static void Pause() => IsPaused = true;
        public static void Resume() => IsPaused = false;
    }

    public static class ReactiveLogger
    {
        private static readonly List<string> _logs = new();

        public static void Log(string message)
        {
            _logs.Add($"[{DateTime.Now:HH:mm:ss.fff}] {message}");
        }

        public static void Dump()
        {
            Console.WriteLine("==== Reactive Event Log Dump ====");
            foreach (var log in _logs)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine("=================================");
        }

        public static void Clear()
        {
            _logs.Clear();
        }
    }

    public static class ReactiveScheduler
    {
        private static readonly SortedDictionary<int, Queue<Action>> logicalQueue = new();

        public static void EnqueueLogical(Action action, int priority)
        {
            if (ReactiveContext.IsPaused)
            {
                ReactiveLogger.Log($"[Scheduler] Skipped due to pause. Priority: {priority}");
                return;
            }

            if (!logicalQueue.TryGetValue(priority, out var queue))
            {
                queue = new Queue<Action>();
                logicalQueue[priority] = queue;
            }
            queue.Enqueue(action);
            ReactiveLogger.Log($"[Scheduler] Enqueued action at priority {priority}");
        }

        public static void Flush()
        {      
            if (ReactiveContext.IsPaused)
            {
                ReactiveLogger.Log("[Scheduler] Flush skipped due to pause.");
                return;
            }

            foreach (var (priority, queue) in logicalQueue)
            {
                while (queue.Count > 0)
                {
                    var action = queue.Dequeue();
                    ReactiveLogger.Log($"[Scheduler] Executing action at priority {priority}");
                    action.Invoke();
                }
            }
            logicalQueue.Clear();
            ReactiveLogger.Log("[Scheduler] Flush complete.");
        }
    }
    public static class ReactiveBinder
    {
        private static readonly Dictionary<object, List<IDisposable>> _bindings = new();

        public static void Bind<T>(IReactiveReader<T> source, Action<T> onChanged, object owner, RxRelationType type = RxRelationType.InteractLogical, int priority = 0)
        {
            ValidateSubscriber(owner, type);

            source.Subscribe(onChanged, owner, type, priority);

            if (!_bindings.TryGetValue(owner, out var list))
            {
                list = new List<IDisposable>();
                _bindings[owner] = list;
            }

            list.Add(new BindingHandle(() => source.Unsubscribe(onChanged)));
        }

        public static void UnbindAll(object owner)
        {
            if (_bindings.TryGetValue(owner, out var list))
            {
                foreach (var binding in list)
                    binding.Dispose();

                _bindings.Remove(owner);
            }
        }

        private static void ValidateSubscriber(object subscriber, RxRelationType type)
        {
            switch (type)
            {
                case RxRelationType.Functional:
                    if (subscriber is not IFunctionalSubscriber)
                        throw new InvalidOperationException("Functional 구독자는 IFunctionalSubscriber를 구현해야 합니다.");
                    break;
                case RxRelationType.InteractLogical:
                    if (subscriber is not IInteractLogicalSubscriber)
                        throw new InvalidOperationException("InteractLogical 구독자는 IInteractLogicalSubscriber를 구현해야 합니다.");
                    break;
                case RxRelationType.ControlLogical:
                    if (subscriber is not IControlLogicalSubscriber)
                        throw new InvalidOperationException("ControlLogical 구독자는 IControlLogicalSubscriber를 구현해야 합니다.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), $"알 수 없는 RxRelationType: {type}");
            }
        }

        private class BindingHandle : IDisposable
        {
            private readonly Action _unsubscribe;
            public BindingHandle(Action unsubscribe) => _unsubscribe = unsubscribe;
            public void Dispose() => _unsubscribe?.Invoke();
        }
    }
}

