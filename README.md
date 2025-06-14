## 📌 캐릭터 랜덤 디펜스(CRD)

캐릭터를 소환해 방어선을 구축하고 몰려오는 적을 방어하는 3D 디펜스 게임입니다.

---

### 🕒 개발 기간
- **2024.11.27 ~ 2024.12.05 (9일)**

---

### 👥 개발 인원
- **1명 (개인 프로젝트)**

---

### 🙋‍♂️ 담당한 부분
- 미니맵 및 부대 지정 시스템
  - RenderTexture를 활용한 미니맵 구성
  - 마우스 드래그로 다수 유닛 선택 및 시각적 표시
- 유닛 명령 시스템
  - 인터페이스와 상태머신을 활용한 명령 처리(이동, 공격, 멈춤)
  - 커맨드 패턴으로 구조화된 명령 처리 설계
- 아이템 조합 시스템
  - 지정된 레시피에 따라 아이템을 생성하는 기능 구현
  - 전략적 자원 활용을 유도하는 조합 로직 설계
- 오브젝트 소환 및 재사용
  - 풀링 시스템을 활용해 오브젝트 관리 최적화 및 연출

---

### ⚙️ 사용 기술
- **Unity**: 게임 엔진 및 전체 시스템 구성
- **C#**: 게임 로직 및 UI 처리
- **RenderTexture**: 미니맵 구현
- **State Machine**: 유닛 상태 관리
- **Command 패턴**: 유닛 명령 처리
- **Object Pooling**: 오브젝트 재사용 최적화

---

### 🎯 프로젝트 목표
- 전략 디펜스 게임의 기본 구조와 시스템 설계
- 미니맵과 유닛 명령 시스템의 연동 구현
- 오브젝트 풀링과 상태머신을 통한 퍼포먼스 최적화

---

### 📚 개발하며 느낀 점
- RenderTexture를 사용해 미니맵을 구현하며 Unity 렌더링 파이프라인에 대해 더 깊이 이해할 수 있었습니다.
- 상태머신과 커맨드 패턴을 결합해 유닛 명령 시스템을 설계하며 유지보수성과 확장성을 고려한 설계 방법을 배웠습니다.
- 오브젝트 풀링을 적용해 성능과 효율성을 동시에 높이는 방법을 경험했습니다.
