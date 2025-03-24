using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akasha;

// ✅ Model
// 게임의 데이터 상태를 보관하는 순수 클래스입니다.
// ReactiveProperty<T>를 사용해 상태가 바뀔 때마다 반응하도록 설계됩니다.
public class CounterModel
{
    // 카운트 상태를 저장하는 Reactive 변수입니다.
    public ReactiveProperty<int> Count = new(0);
}

