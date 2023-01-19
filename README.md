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

## 📋 개발 과정
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
<div align="center">
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/213256126-ed85cae0-7240-44f8-8912-6414b19a9230.png"/>
  <img width="60%" height="60%" src="https://user-images.githubusercontent.com/60832219/213256114-e411e547-787d-4ca4-80bd-f7e34a6d54ab.png"/>
</div>

+ 조이스틱 범위에 입력이 발생하면 **중점을 기준으로 벡터를 반환**한다.
+ **왼쪽 조이스틱**은 **이동 목적**으로 사용되고, **오른쪽 조이스틱**은 **공격 목적**으로 사용된다.
+ 주 스크립트는 `MoveJoystick.cs`와 `AttackJoystick.cs`가 있다.
+ 모든 UI 요소와 함께 **해상도 대응**이 일어난다.

### 무기 구현
> 크게 이펙트, 공격 판정, 강화로 나눌 수 있다.

👉🏻 이펙트
<div align="center">
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

👉🏻 공격 판정
