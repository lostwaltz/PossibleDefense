### 슬라임 타워 디펜스 - README

---

### **프로젝트 개요**

**슬라임 타워 디펜스**는 3D 타워 디펜스 게임 프로젝트로,조건에 따른 업적 클리어와 업그레이드 및 기능이 다른 타워,몬스터 종류와 스킬, 데이터에 기반한 동적 스테이지등, 필요한 부분을 연습하고자 한 프로젝트입니다.

- **장르**: 타워 디펜스
- **플랫폼**: 유니티 엔진
- **팀원**:
    - 김민철 (팀장) (lostwaltz)
    - 송명성 (SONGMYEONGSEONG)
    - 손형민 (Zzondy-Unity)
    - 정준영 (pracUmj)

- **BGM**
    **Made With Suno**
---

### **주요 기능**

1. **업적 시스템**  
   ![image](https://github.com/user-attachments/assets/290eaa06-cf49-4d22-b72d-6d25144ced5c)  
    ▲ 인게임 업적 시스템 화면  
    - 다양한 게임 내 행동에 대해 업적을 추적 및 달성 가능.
    - Action(행동)과 Target(목표) 기준으로 업적 분류, 고유 ID를 통해 특정 트리거를 정의.
2. **몬스터**  
   ![1](https://github.com/user-attachments/assets/9a7e2225-5512-408c-ae67-a788b43f84d2)  
    ▲ 스폰지역에서 적이 주기적으로 소환  
    - 각 웨이브마다 주기적으로 스폰.
    - 보스를 포함한 특정 몬스터는 스킬 보유. 스킬은 Scriptable Object로 구현하여 확장 및 제거 용이.
3. **타워**  
   ![image](https://github.com/user-attachments/assets/8a7ca35b-1538-48ee-bc21-b2cd5c3e4e19)  
   ▲ Scene에서 따로 적용이 가능한 업그레이드 화면
    - 영구 업그레이드 및 스테이지 전용 업그레이드 제공.
    - 타워마다 고유한 공격 효과 보유.
    - 유지보수성과 확장성을 고려해 전략 패턴(Strategy Pattern)과 상태 패턴(State Pattern) 사용.
4. **스테이지**  
    ![image](https://github.com/user-attachments/assets/1f20512f-e77a-442f-b522-b7504235c84f)  
    ▲ 외부데이터(CSV)를 사용하여 스테이지 데이터 관리
    - 외부 데이터를 통해 맵을 설정하며, 확장 가능한 구조로 제작.

---

### **코드 주요 내용**

1. **업적 시스템**  
    ![image](https://github.com/user-attachments/assets/b91ecdc2-ebd3-41a8-b751-dad8b7fa2ddc)  
    ▲ AchievmentManager 클래스 코드
    - 특정 조건을 충족하는 행동과 목표를 필터링하여 업적을 트리거.
3. **몬스터 및 스킬**  
   ![image](https://github.com/user-attachments/assets/74f343df-dada-4cf3-93be-1aa8d21f78a7)  
    ▲ 몬스터 스킬 클래스 코드  
    ![image](https://github.com/user-attachments/assets/818b2066-c79a-4202-b287-ba1ed06fabc0)  
    ▲ Inspector로 몬스터의 패턴을 쉽게 관리 할수 있게 구현  
    - 보스 스킬은 Scriptable Object로 구현되어 필요 시 쉽게 추가 및 제거 가능.
5. **타워**  
   ![image](https://github.com/user-attachments/assets/3c756210-d7ac-4dc1-bee3-998cdb7b47c5)  
   ▲ 전략패턴을 이용하여 공격패턴의 다양하게
    - 전략 패턴 및 상태 패턴을 활용해 타워 업그레이드와 고유 공격 구현.
    - 타워 스포너(Tower Spawner)를 통해 동적으로 타워 생성.
7. **스테이지**  
   ![image](https://github.com/user-attachments/assets/a4e5ed1c-570e-48a9-80f1-7524f3b72628)  
   ▲ CSVReader 클래스 코드
    - 외부 데이터를 기반으로 맵을 생성하고 설정.

---

### **트러블슈팅**

1. **오브젝트 풀 초기화 문제**
    - **문제**: 초기화되지 않은 상태로 재사용되어 물리 속성(중력 등)이 누적되는 문제 발생.
    - **해결**: 오브젝트 풀링 시 필요한 초기화 로직 추가 및 물리 속성 초기화 적용.
2. **업적 시스템 초기화 문제**
    - **문제**: 싱글톤 초기화 순서 문제로 인해 업적 데이터 로드 오류 발생.
    - **해결**: 씬 로더 매니저(Scene Loader Manager)를 사용해 초기화 순서 관리.
3. **씬 전환 시 데이터 초기화 문제**
    - **문제**: 씬 전환 시 데이터가 초기화되는 이슈.
    - **해결**: JSON을 활용해 데이터를 저장 및 로드하며, `SceneEnter` 함수로 데이터 세팅.
4. **스테이지 맵 생성 문제**
    - **문제**: 맵을 동적으로 생성하고 데이터를 유지하는 과정에서 발생한 어려움.
    - **해결**: 외부 데이터를 기반으로 맵 설정 구조 개발.
