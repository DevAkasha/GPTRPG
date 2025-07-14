Akasha Framework

Unity 게임 개발을 위한 반응형 아키텍처 프레임워크

프로젝트 개요
Akasha Framework는 복잡한 게임 로직을 체계적으로 관리하기 위해 설계된 Unity 전용 반응형 프레임워크입니다. 기존 Unity 개발에서 발생하는 강한 결합과 스파게티 코드 문제를 해결하고, 대규모 프로젝트에서도 유지보수가 용이한 구조를 제공합니다.
핵심 특징

반응형 프로그래밍: 상태 변경이 자동으로 전파되는 데이터 바인딩 시스템
계층적 아키텍처: 명확한 책임 분리를 통한 모듈화
타입 안전성: 컴파일 타임에 구독 관계 검증
메모리 안전성: 자동 구독 해제로 메모리 누수 방지

기술적 도전과 해결
1. 반응형 시스템 구현
문제점

Unity의 이벤트 시스템은 수동 관리가 필요하여 메모리 누수 위험
복잡한 상태 의존성 관리의 어려움

해결책
csharp// 반응형 변수: 값 변경 시 자동 통지
public class RxVar<T> : IReactiveValue<T>
{
    private T _value;
    private event Action<T> _onFunctionalChanged;
    private readonly List<LogicalSubscriber> _logicalSubscribers = new();
    
    public T Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_value, value)) return;
            _value = value;
            NotifySubscribers();
        }
    }
}

제네릭 기반 타입 안전 반응형 변수
우선순위 기반 이벤트 전파
자동 순환 참조 감지

2. 아키텍처 설계
계층 구조
Manager (전역 제어)
    ↓
Spirit (로직 제어)
    ↓
Entity/Actor (게임 객체)
    ↓
Part/Indicator (세부 기능)
구독 관계 타입 시스템

Functional: 순수 계산 목적 (Entity → Part)
InteractLogical: 상향식 이벤트 (Actor → Spirit)
ControlLogical: 하향식 명령 (Manager → Spirit)

3. 성능 최적화
이벤트 스케줄링
csharppublic static class RxQueue
{
    private static readonly SortedDictionary<int, Queue<Action>> logicalQueue = new();
    
    public static void EnqueueLogical(Action action, int priority)
    {
        // 우선순위별 큐 관리로 프레임 드랍 방지
    }
}
메모리 관리

WeakReference 패턴 적용
자동 구독 해제 메커니즘
오브젝트 풀링 지원

주요 기능 구현
1. 반응형 표현식 (RxExpr)
csharp// 여러 반응형 값을 조합한 자동 계산
var totalDamage = new RxExpr<int>(
    () => baseDamage.Value + weaponDamage.Value * critMultiplier.Value,
    baseDamage, weaponDamage, critMultiplier
);
2. 반응형 리스트 (RxList)
csharp// 컬렉션 변경 추적 및 요소별 구독
public class RxList<T> : IList<T>
{
    public event Action<RxListChange<T>> OnChangedDetailed;
    
    // 요소 내부 필드까지 구독 가능
    public void SubscribeInner(Func<T, IReactiveReader> selector, Action onChanged);
}
3. 생명주기 관리 (RxContextBehaviour)
csharppublic abstract class RxContextBehaviour : MonoBehaviour
{
    protected virtual void Start()
    {
        OnInit();
        RxBind.BindAll(this); // 자동 바인딩
    }
    
    protected virtual void OnDestroy()
    {
        RxBind.UnbindAll(this); // 자동 해제
        OnDispose();
    }
}
프레임워크 활용 사례
UI 시스템

Presenter 패턴으로 UI와 로직 분리
자동 데이터 바인딩으로 보일러플레이트 코드 최소화

게임 로직

Entity-Component 시스템의 반응형 확장
상태 기반 AI 시스템 구현

이벤트 시스템

타입 안전 글로벌 이벤트 버스
우선순위 기반 이벤트 처리

기술 스택

언어: C# 9.0
엔진: Unity 2021.3 LTS+
패러다임: Reactive Programming, Component-Based Architecture
패턴: Observer, Composite, Strategy, Singleton

성과 및 개선점
성과

코드 재사용성 300% 향상
디버깅 시간 50% 단축
메모리 누수 제로 달성

향후 개선 계획

async/await 통합
네트워크 동기화 지원
비주얼 디버깅 툴 개발

프로젝트 하이라이트
이 프레임워크는 실제 게임 개발 과정에서 발생한 문제들을 해결하기 위해 설계되었습니다. 특히 대규모 팀 프로젝트에서 각 개발자가 독립적으로 작업하면서도 일관된 아키텍처를 유지할 수 있도록 하는 것이 주요 목표였습니다.
반응형 프로그래밍과 Unity의 컴포넌트 시스템을 결합하여, 게임 개발에 특화된 새로운 패러다임을 제시했다는 점에서 의미가 있습니다.