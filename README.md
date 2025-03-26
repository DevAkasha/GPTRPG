# 🌀 Akasha 기반 인벤토리 UI 시스템

본 프로젝트는 Unity 환경에서 Akasha Framework(자체제작 반응형 프레임워크)를 기반으로 구축된  
**Reactive & 구조화된 캐릭터 인벤토리 UI 시스템**입니다.
순전히 프로그래밍 기술 위주의 프로젝트입니다.

---

## ✅ 프로젝트 개요

- Unity 기능의 UGUI 인벤토리 / 캐릭터 상태 UI
- Akasha Framework의 Reactive / 구조 / 로직 분리 구조 적용
- RxVar / RxExpr / RxList 기반의 상태 추적 및 표현
- Presenter / Spirit 중심의 상호작용 해석 구조

---

## 🛠️ 현재 구성된 기능 요약

- 캐릭터 정보 (이름, 레벨, HP 등) RxVar 기반
- 인벤토리 RxList<Item> 기반, 슬롯 동적생성
- Slot 클릭 시 장착/해제 토글 **(나무검 장착시 공격 20 해제시 공격 10)**
- StatWidget은 RxExpr 기반으로 자동 반영
- GoldWidget / CharacterWidget은 RxBind 기반으로 상태 반영
- ScrollView + GridLayoutGroup 구성 UI

---
## 🤠 Akasha Framework 소개

> Akasha는 명확한 구조 계층과 선언적 관계를 통해  
> **UI/World 기능과 제어 로직, 표현 계층을 완전하게 분리하는 구조 중심 Reactive Framework**입니다.

### 📀 주요 철학: **패러다임 대칭 구조**

#### 1. 기능 계층
| 분류 | 역할 |
|------|------|
| `Entity`, `Part` | 상태 기반의 World 구성 |
| `View`, `Screen` | 상태 표현 기반의 UI 구성 |
| `Widget`, `Geer` | 상호작용 포함된 복합 기능 담당 |

#### 2. 제어 계층
| 분류 | 역할 |
|------|------|
| `Spirit` | Entity + Actor 연결. Control Logic 중심 허브 |
| `Manager` | 전역 제어자. GameEvent 및 전체 흐름 담당 |

#### 3. 표현/반응 계층
| 분류 | 역할 |
|------|------|
| `Actor` | Entity 상태 기반 View 갱신 |
| `Presenter` | UI 해석 및 Spirit 전달 |
| `Indicator` | 이펙트, 사운드 등 단일 표현 |

#### 4. 선언적 관계 구독
| 구독자 타입 | RxType | 사용 예시 |
|-------------|--------|------------|
| 기능 중심 (View, Part) | `Functional` | 상태만 구독 |
| 사용자 상호작용 (Widget, Actor) | `InteractLogical` | 사용자 반응 수신 |
| 제어 중심 (Spirit, Manager) | `ControlLogical` | GameEvent, 명령 수신 |

---

## 🔄 Reactive 구조 원칙

- **RxVar<T>**: 단일 값 상태 (체력, 이름, 골드 등)
- **RxExpr<T>**: 계산식 기반의 상태 (총 공격력 등)
- **RxList<T>**: 아이템, 슬롯 등 연결 개체 관리
- **RxBind**: 선언적 구독 (초기값 포함)
- **RxEvent, GameEvent**: 명령형 이벤트 흐름

---

## 📆 프로젝트 구조

```
Assets/
├── Scripts/
│   ├── 1.Origin/
│   │   ├── Function/
│   │   │   ├── UI/
│   │   │   │   ├── BaseView.cs
│   │   │   │   ├── BaseScreen.cs
│   │   │   │   ├── BaseWidget.cs
│   │   │   └── World/
│   │   │       ├── BaseEntity.cs
│   │   │       ├── BasePart.cs
│   │   │       ├── BaseGeer.cs
│   │   └── Logic/
│   │       ├── Control/ → Manager, Spirit
│   │       ├── Interact/ → Actor, Presenter, Indicator
│   ├── 2.Basic/ ← 사용자 정의 Entity/Spirit 등
│   ├── 3.Extended/ ← 구체 UI, GameManager, Character 등
```

---

## 🎮 UI 흐름 예시

```
[버튼 클릭]
 → Presenter 이벤트
   → Spirit 명령 해석
     → Entity 상태 변경 (장착 등)
       → RxExpr 변화
         → StatWidget 자동 갱신
```

---


## 개발환경
- **Unity** : 2022.3.17f
- **IDE**  : Visual Studio 2022
- **OS** : Window10

## 개발자
- 허민영
- [티스토리](https://devakasha.tistory.com/)

