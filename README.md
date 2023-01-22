# 스틱맨 데자뷰
// 동영상 링크

## 🔗 링크
+ https://play.google.com/store/apps/details?id=com.coust.stickmandejavu

## 🖥 프로젝트 소개
+ **Play 스토어** 출시를 목적으로 한 게임을 개발한다.
+ 이를 통해 **Google Play Console** 및 **Google AdMob**을 활용해본다.

## ⏲ 개발 기간
+ 2022.03 ~ 2022.06
+ 1인 개발

## ⚙ 개발 환경
+ 개발툴 : `Unity 2021.1.15f1`
+ 스틱맨 애니메이션 제작 : `Pivot Animator`
+ 이미지 제작 및 편집 : `Adobe Photoshop`
+ 오디오 제작 및 편집 : `GoldWave`
+ 데이터베이스 : `Maria DB`
+ 서버 사이드 언어 : `PHP`
+ 로그인 및 IAP API : `Google Play Console`
+ 광고 API : `Google AdMob`

## ✏ 프로젝트 기획
> 횡 스크롤 형식의 생존 게임을 개발한다.

+ 사용자 정보를 불러오기 위한 **로그인** 기능을 구현한다.
+ 6개의 **무기**를 사용할 수 있고, 각각의 **궁극기**를 구현한다.
+ 여러 특징을 지닌 **적들을 구현**한다.
+ 플레이어 이동을 위한 **조이스틱**을 구현한다.
+ 스테이지 별 생성되는 **적들을 정의**하고, 스테이지가 종료되면 **광고 보상**을 생성한다.
+ 화폐를 통해 상점에서 **무기를 구매**하거나, **능력치를 강화**할 수 있게 한다.
+ **인앱 결제**를 하면 화폐를 추가 구입할 수 있도록 구현한다.
+ 현재까지의 진행 사항을 **저장**하는 기능을 구현한다.
+ **업적** 기능을 구현한다.

## 📋 주요 개발 과정
### 로그인 기능 구현
> 각 사용자의 정보를 불러오기 위한 로그인 기능이다.
<img width="50%" height="50%" src="https://user-images.githubusercontent.com/60832219/213242685-51c07a74-acf8-4ba5-bf2b-8aa86f20a30f.png"/>

+ **Google Play Console** API를 사용하였고, 구글 로그인에 성공하면 사용자의 **고유 ID**를 처리할 수 있다.
+ **게스트 로그인**은 저장 및 구매가 제한된다.
+ 로그인 기능을 구현하는 과정은 본 게시글에 정리했다.
  + https://giseung.tistory.com/37

### 스틱맨 애니메이션 제작
> 아래 이미지들은 궁극기 애니메이션 중 일부이다.
<div align="center">
<img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213253190-3743ae0d-0a28-46ef-9b97-3668a2476e67.gif"/>
<img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213253201-16a8cdb4-0cee-4e7d-bb75-eaf78700c378.gif"/>
<img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213253203-9c76e1cf-042a-4a2d-85c0-c0d2bd2261f1.gif"/>
</div>
  
+ 주인공 역인 스틱맨의 애니메이션은 **Pivot Animator** 프로그램을 활용해서 제작했다.
  + https://pivotanimator.net/
+ 가만히 있거나 뛰는 중에도 공격 모션은 실행되어야 하기 때문에 **상하체**를 구분했다.
+ **6개의 무기** 모션을 모두 제작했으며, **궁극기** 모션은 상체에 맞추었다.
+ 좌우 방향에 맞추어 상하체가 따로 회전한다.

### 조이스틱 구현
> 스틱맨을 조작하기 위한 조이스틱 기능이다.
<div align="left">
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213256126-ed85cae0-7240-44f8-8912-6414b19a9230.png"/>
  <img width="60%" height="60%" src="https://user-images.githubusercontent.com/60832219/213256114-e411e547-787d-4ca4-80bd-f7e34a6d54ab.png"/>
</div>

+ 조이스틱 범위에 입력이 발생하면 **중점을 기준으로 벡터를 반환**한다.
+ **왼쪽 조이스틱**은 **이동 목적**으로 사용되고, **오른쪽 조이스틱**은 **공격 목적**으로 사용된다.
+ 주 스크립트는 `MoveJoystick.cs`와 `AttackJoystick.cs`가 있다.
+ 모든 UI 요소와 함께 **해상도 대응**이 일어난다.

### 무기 구현
> 크게 이펙트, 공격 판정, 강화로 나눌 수 있다.

#### 👉🏻 이펙트
<div align="left">
  <img width="50%" height="50%" src="https://user-images.githubusercontent.com/60832219/213341675-ffb68d00-9567-4e56-b1b2-5655f5dd68c0.gif"/>
  <img width="55%" height="55%" src="https://user-images.githubusercontent.com/60832219/213341684-0e9868a9-0fa8-4da9-94e3-c49698d4892a.gif"/>
  <img width="65%" height="65%" src="https://user-images.githubusercontent.com/60832219/213341685-3d38d7f6-b35f-42ac-84ca-5e9394ec13a3.gif"/>
  <img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/213342931-a3fdad6e-e243-4fb8-b243-eb7392637e76.gif"/>
</div>

+ 대부분의 무기 이펙트는 **Particle System**을 활용하여 제작했다.
+ 포토샵으로 편집한 여러장의 이미지를 한 장의 **Sprite Sheet**로 생성하여 입자를 구현했다.
+ 이렇게 구현된 입자는 시간 흐름에 따라 위치, 크기, 속도, 방향, 색상 등이 변경된다.
+ 각 이펙트는 무기 모션의 **키 프레임 함수**에서 실행된다.
+ Sprite Packer로 **드로우콜 최적화**를 적용했다.

#### 👉🏻 공격 판정
<img width="25%" height="25%" src="https://user-images.githubusercontent.com/60832219/213673140-ccd22ba7-fe65-4bee-aa98-782a2f571bb7.gif"/>

+ 공격 판정 생성은 무기 모션의 **키 프레임 함수**에서 실행된다.
+ 공격 판정은 물리 충돌이 발생하는 **FixedUpdate**문 실행 전까지 활성화된 후, 비활성화된다.
+ 적 충돌이 발생하면 적 부모 클래스의 **GetDamage** 함수를 실행한다.
+ **피해량, 이동 속도 감소량, 타격 시 효과음, 궁극기 증가 여부** 등을 설정할 수 있다.
+ 주요 스크립트는 `PlayerAttackBoundManager.cs`이다.

#### 👉🏻 강화
<div align="left">
  <img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/213690253-971d28d4-ff53-44b7-a35c-290f06250c8d.png"/>
  <table border="0">
    <tr>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213688519-16b06696-7808-4d0b-be74-b100ba7baace.gif"/>
      </td>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213688527-950fe434-a9e1-47f5-8257-8a0cfe7317b7.gif"/>
      </td>
    </tr>
    <tr>
      <td align="center">
        Sniper 기본 공격 : 강화 0
      </td>
      <td align="center">
        Sniper 기본 공격 : 강화 12
      </td>
    </tr>
    <tr>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213688524-42c89a9d-5ff9-46f8-8991-10ffe11f323e.gif"/>
      </td>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213688528-c79d827f-f532-4f7a-82c0-61cb5d991513.gif"/>
      </td>
    </tr>
    <tr>
      <td align="center">
        Sniper 궁극기 : 강화 0
      </td>
      <td align="center">
        Sniper 궁극기 : 강화 12
      </td>
    </tr>
  </table>
</div>

+ 강화는 각 공격당 **12번**까지 가능하고, 강화 단계가 올라갈수록 **강화 비용이 증가**한다.
+ 일반적으로 **공격력**이 증가하고, 세 번째 강화 단계에서는 **공격 속도** 및 **궁극기 충전량**이 증가한다.
+ 예외로 **Wizard** 무기는 강화 세 번째 강화 단계마다 이동 속도 감소량이 증가한다.
+ `Definition.cs`에서 정의한 강화 수치 증가량을 `Player.cs`에서 초기 적용하는 방식으로 구성했다.

### 능력치 구현
<img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/213694658-db5e1d3f-37ae-4990-b117-7d61b35ba314.png"/>

+ **능력치 종류**
```
  최대 체력> 최대 체력이 증가한다.
  체력 회복률> 시간에 따라 회복하는 체력량이 증가한다.
  방어력> 각 무기당 피해를 입었을 때 감소하는 피해량이 증가한다.
  이동 속도> 각 무기당 이동 속도가 증가한다.
  다이아 획득량> 적을 처치했을 때 얻는 다이아량이 증가한다.
  다이아 획득 확률> 적을 처치했을 때 다이아를 얻을 확률이 증가한다.
  치명타 피해량> 적을 공격했을 때 치명타 피해량이 증가한다.
  치명타 확률> 적을 공격했을 때 치명타를 입힐 확률이 증가한다.
```
+ 강화는 각 능력당 **30번**까지 가능하고, 강화 단계가 올라갈수록 **강화 비용이 증가**한다.
+ 게임의 난이도가 급격히 쉬워지지 않도록 덧셈 연산을 사용했다.
+ `Definition.cs`에 정의한 강화 수치가 적용된 능력치를 `Player.cs`내의 프로퍼티로써 사용할 수 있다.

### 무기 교체 구현
> 두 개의 무기를 자유자재로 교체할 수 있게끔 UI를 구현했다.
<div align="left">
<img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213700923-76ca5e6c-a943-4858-a5cf-a27edf409fa7.gif"/>
<img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213702506-1e14e4fb-bdf2-460b-8182-34969a0435d8.gif"/>
</div>

+ 초기에 선택한 두 개의 **무기 아이콘**을 지정한다.
+ 버튼을 클릭하면 **애니메이션**이 실행되고, 두 아이콘의 **레이어 순서**를 변경한다.
+ 애니메이션이 종료되면, 최종적으로 무기 교체가 일어난다.
+ 무기 교체 도중에 공격할 수 없다.
+ 만약, 버튼에 의해 적이 가려지면 **불투명도**를 낮춘다.
+ 주요 스크립트는 `WeaponSwitchingButton.cs`이다.

### 적 구현
> 종류에 따라 다양한 특성을 가진다.
<div align="center">
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706409-7a82567c-6eed-4d21-b7f6-67035e8a59c7.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706411-5c854ade-a40e-46a5-b640-93e8162da7fd.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706412-8f61e98f-75de-4c06-81fa-bbad3e9b02bb.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706039-289ed2b0-201d-446d-bb5d-3ee53cb2f943.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706041-2f005b7d-54d9-46c5-8c51-dbe3e5afcc6e.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706049-ae327809-3212-4cc1-a749-15787111ae10.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706051-5b56f126-2b7a-4426-bde7-b702a4cea360.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706053-d97485fd-1ad8-434f-926a-d2ac2f485f90.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706055-e1a1cef3-f254-42ad-af56-5c3dd7e72380.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706058-38aa8dde-e59b-48fc-bcab-7b509b590087.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706060-39163938-ed20-4f16-a399-2c180d1a510d.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706062-3e802d7a-c829-41b1-802b-07fa9a9debd3.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706067-e2d88b94-319a-4f28-9374-e90669fad644.png"/>
  <img width="13%" height="13%" src="https://user-images.githubusercontent.com/60832219/213706071-0069ab30-35d2-452f-8e87-c75f41f66f03.png"/>
</div>

#### 👉🏻 공통
+ 능력치는 **체력, 이동 속도, 공격력, 공격 범위, 슬로우량** 등이 있다.
+ 애니메이션은 **정지, 공격, 피해, 이동, 죽음**이 있다.
+ **무기를 들고 있는 적**은 무기에 따라 근거리 또는 원거리로 구분된다.
+ 근접 무기는 오른쪽으로 갈수록 **공격력**과 **공격 범위**가 커진다.
+ 스틱맨의 **공격 판정**과 동일한 방식을 사용한다.
+ 적마다 스틱맨을 **탐색**하는 속도가 다르다.
+ 피해를 입으면 **Sprite**의 색상이 붉어졌다가 점차 돌아온다.
+ 부모 클래스는 `Enemy.cs`이다.
+ **Sprite Packer**로 드로우콜을 최적화하였다.

#### 💀 Slime
<div align="left">
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213721173-e28791ec-57ed-4425-8669-e405dba2405e.gif"/>
</div>

+ 전체적으로 **가장 약한** 능력치를 지닌다.
+ 이동하면서 내려찍을 때 공격 판정을 발생한다.

#### 💀 Rat, Spider, Worm
<div align="left">
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213724118-5b2620a0-4195-406c-8c28-30cf2064dced.gif"/>
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213724127-143a8f01-7e6b-4bc3-9e52-9117fe968d94.gif"/>
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213724130-b07a0f6d-aae5-47b4-abce-21c80149bc0a.gif"/>
</div>

+ 걸어다니는 적으로 오른쪽으로 갈수록 **높은 체력**과 **강한 공격력**이 특징이다.
+ Worm은 스틱맨을 탐색하는 시간이 없다.

#### 💀 Crow, Bat, Beholder
<div align="left">
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213726192-231a133d-3f26-408f-b6fc-9c7d735c1383.gif"/>
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213726200-647a67d4-2cb1-411a-9042-6381df05bd57.gif"/>
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213726207-ea191578-231c-43f1-ad68-ef28186cddb8.gif"/>
</div>

+ 날아다니는 적으로 오른쪽으로 갈수록 **높은 이동 속도**와 **넓은 공격 판정**이 특징이다.
+ Beholder는 원거리다.

#### 💀 Orc
<div align="left">
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213727859-d023bbf1-7798-4d32-aff9-94c23ba311c9.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213727866-f6130441-ccd0-4527-800f-aa100b89cd88.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213727873-c61344dd-0155-4242-819b-529b51ef519d.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213727875-a42b30c4-6b0b-4324-970e-73d55d2a0743.gif"/>
</div>

+ 별다른 특징은 없다.

#### 💀 Cyclope
<div align="left">
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213730675-e2be0661-8f65-4f9d-a464-fb379993c9af.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213730683-622558ae-a311-43e7-afb6-af0ce5ffb724.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213730686-b8e4e083-7db3-42c2-9333-53f9b45583e1.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213730696-efc7979f-2178-415c-a553-e07e3d9e82ae.gif"/>
</div>

+ 이동 속도가 매우 느리지만, **매우 높은 체력**과 **넓은 공격 판정**을 지닌다.

#### 💀 Demon
<div align="left">
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213732883-6e5bcf1a-4136-411e-9607-374771f9fd2e.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213732888-8fc2cf15-9a0e-4863-b189-9191a498021c.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213732894-797427af-a27d-45c0-9adb-8885c11a2baa.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213732899-aa955f01-9d5e-490a-9cac-22757b71b8c1.gif"/>
</div>

+ 피해 애니메이션이 없어서 **경직**을 받지 않는다.

#### 💀 Goblin
<div align="left">
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213791763-0ca3d727-cc3f-4f76-94bf-f38d5eb39a2f.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213791771-8baf06ef-88a6-4855-b7ec-fbc0cd424aa3.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213791773-8910d993-1316-4113-aea2-5c98a23466df.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213791775-de0f5a66-2aaa-4d25-b791-334607ee403b.gif"/>
</div>

+ **빠른 이동 속도**를 지닌다.

#### 💀 Zombie
<div align="left">
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213793581-9d35dcbf-dc01-41a2-8260-b1814b38a2df.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213793586-e8822608-b8eb-4054-8461-4d488498e8e4.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213793589-958cc407-59c6-4ac1-b9ec-076b350e1411.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213793593-42d7402d-f23a-4af2-a5ec-a93c69eed88a.gif"/>
</div>

+ 이동 중, 체력을 점차 **회복**한다.

#### 💀 Ghost
<div align="left">
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213795373-2d531cf1-6c92-4ff5-ad95-0a331c5e5f48.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213795394-ffed5dbf-8b51-4181-87d9-06bf959d9e47.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213795405-8a9cb243-80ee-4350-b195-34336deb9bff.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213795416-2bc1b101-0e7c-4dcf-96af-3ab0d3bb537c.gif"/>
</div>

+ **원거리 공격에 면역**이 된다.

#### 💀 Skeleton
<div align="left">
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213797195-f3b526e7-2a68-47d4-aa52-bc6e753f33e5.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213797209-b43c6963-065a-437c-9020-a7109c0f0d9b.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213797215-8f7abf10-d518-47e1-937f-57fe57b47a46.gif"/>
  <img width="23%" height="23%" src="https://user-images.githubusercontent.com/60832219/213797229-5f7eedaf-134d-4731-b396-4dc8badb9bb9.gif"/>
</div>

+ 죽고 나면 일정 시간 후 **부활**한다.
+ 부활 후에는 **능력치가 대폭 상승**하고, 일정 시간이 지나야만 다시 죽는다.

#### 👉🏻 티어
<div align="left">
  <table border="0">
    <tr>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213798574-af9996d1-d306-4531-82f5-99b38be2efe9.gif"/>
      </td>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213798580-fdaaaec0-da02-4892-97a2-41cced32b84f.gif"/>
      </td>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213798583-8d6baf20-c42d-42b4-9675-290c345df54b.gif"/>
      </td>
    </tr>
    <tr>
      <td align="center">
        티어 1
      </td>
      <td align="center">
        티어 2
      </td>
      <td align="center">
        티어 3
      </td>
    </tr>
  </table>
</div>
  
+ **체력바 색상**에 따라 티어를 3단계로 구분한다.
+ 티어가 상승함에 따라 모든 **능력치**가 큰 폭으로 상승한다.

### 스테이지 정의
<div align="left">
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213801138-a1baf807-dec7-4ba1-ad74-b19edeb79c60.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213801140-089bad90-4823-46d7-ac56-8eb06235008c.png"/>
</div>

+ **Assets/Resources/Stage** 디렉터리에 각 스테이지 정보를 저장한다.
+ 파일에는 **동시 생성 수, 생성 지연 시간, 생성하는 적 순서**가 정의되어있다.
+ 게임을 시작하면, 파일 입출력을 통해 생성하는 적 순서를 **큐**에 삽입한다.
+ 주요 스크립트는 `EnemySpawnManager.cs`이다.
+ 모든 적이 생성되어 사라지면 스테이지를 종료한다.

### 애드몹 추가
<div align="left">
  <table border="0">
    <tr>
      <td colspan="2" align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213897970-7fecadb3-5361-4909-808d-ad6b64170707.png"/>
      </td>
    </tr>
    <tr>
      <td colspan="2" align="center">
        광고 화면
      </td>
    </tr>
    <tr>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213897969-f434d188-71aa-4469-8f14-300ba235e241.png"/>
      </td>
      <td align="center">
        <img width="100%" height="100%" src="https://user-images.githubusercontent.com/60832219/213897971-022e6afe-1dc2-409b-8e5e-da9e62db5d82.png"/>
      </td>
    </tr>
    <tr>
      <td align="center">
        광고 시청 전 결과 화면
      </td>
      <td align="center">
        광고 시청 후 결과 화면
      </td>
    </tr>
  </table>
</div>

+ 스테이지를 클리어한 뒤에, 결과 화면에서 **보상형 광고**를 시청할 수 있다
+ 광고를 시청하면 번 수익과 클리어 보상을 **1.5배**로 획득할 수 있게끔 하였다.
+ 보상형 광고를 추가하는 과정은 본 게시글에 정리했다.
  + https://giseung.tistory.com/39
  
### 일시정지 기능 구현
<div align="left">
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213929365-b7330ecd-dccb-4166-bc69-67c3ec7a4fc2.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213929367-52233368-4f52-4517-838e-e6d34eb2dddf.png"/>
</div>

+ 일시정지 버튼을 클릭하면, **Time.timeScale** 값을 0으로 지정한다.
+ 위와 같은 과정을 거치면 시간에 영향을 받는 모든 게임 오브젝트가 멈추게 된다.
+ 일시정지와 동시에 배경 Sprite의 **레이어 순서를 최상위**로 변경하여, 모든 Sprite가 가려지도록 했다.
+ 주요 스크립트는 `CanvasManager.cs`이다.

### 인앱결제(IAP) 구현
<div align="left">
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213930094-ca014b54-ef21-4376-9e4e-249b90b385fd.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213930096-5a4b0eb3-09a5-433a-a5c0-b30fefe7fbfe.png"/>
</div>

+ 상점에서 게임 내 화폐를 추가 구입할 수 있도록 **인앱결제** 시스템을 구현했다.
+ 로그인을 했을 경우에만 결제가 가능하도록 했다.
+ 주요 스크립트는 `IAPManager.cs`이다.
+ 인앱결제를 구현하는 과정은 본 게시글에 정리했다.
  + https://giseung.tistory.com/38

### DB 통신 구현
<div align="left">
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213931404-1f548b31-c830-4df9-91c2-792c194a227c.PNG"/>
</div>

+ **서버 호스팅**을 통해 데이터베이스를 구축했다.
+ 서버사이드 언어인 **PHP**를 사용하여 클라이언트와의 통신을 구현했다.
+ 데이터베이스의 여러 컬럼을 불러오는 경우에는 **Json** 파싱을 활용했다.
+ 네트워크 미연결 시 **예외 처리**했다.
+ 주요 통신 정보는 다음과 같다.
  + `저장`: 메인 화면에서 불러오기 버튼을 클릭하면 사용자의 저장 정보를 불러오고, 저장 버튼을 클릭하거나 스테이지를 클리어하면 저장이 된다.
  + `스테이지 결과`: 스테이지가 종료되면 각 사용자의 결과 정보를 저장한다.
  + `업적`: 로그인을 하면 사용자의 업적 정보를 불러오고, 업적을 달성하면 자동으로 저장된다.
  + `화폐 구매 내역`: 사용자가 화폐를 구매하면 결제 내역을 저장한다.

## 📽 GIF
<div align="left">
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938845-51c01533-7f52-440d-a5f8-a546975fb11f.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938846-0222eb6e-2048-463b-b6c5-213faacabb8c.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938847-9059f0a6-1d69-4585-8fe5-b593c99e6bd6.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938850-80afc451-c7be-4147-81d0-93427937ff95.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938855-185a5739-3dc6-4287-9712-9f35519aa510.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938858-a390bcf7-feab-4ca3-9abf-292d7328add1.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938859-ec4b281c-e6e3-4def-bebc-b0d00cc20e7d.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938862-e59f342b-97d0-430a-afbc-306506722f64.gif"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938863-59ee96c2-d6e0-4468-b040-146d518abf85.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938864-9ff05d5b-fde3-4fb5-a424-b017c8c0358b.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938866-490f2eec-7658-4654-bcb3-f9e178e71c6d.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938867-b572cf06-a106-43d8-9b9a-06c24cce2ff3.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938868-e9289bf9-50ab-4367-862b-8b600e3862a5.png"/>
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/213938869-a394012b-1081-4bdc-9790-db5f3751335a.png"/>
</div>
