#  2D 로그라이크 프로젝트

## ■ Code Conventions

- private(지역) 변수는 카멜 표기법으로 작성하도록 한다. (ex. private myName)
- public(전역) 변수는 파스칼 표기법으로 작성하도록 한다. (ex. public MyName)

- 매서드(함수)의 이름은 파스칼 표기법으로 작성하도록 한다.

## ■ Commit Conventions

|유형|내용|
|-|-|
|Feat|새로운 기능 추가를 한 경우|
|Fix|버그를 수정을 한 경우|
|Build|빌드 관련내용을 수정한 경우 (Project Setting)|
|Test|테스트 Scene또는 코드를 추가한 경우|
|Refactor|코드를 리펙토링한 경우|
|Docs|주석이나 문서를 수정한 경우 (README 등)|
|Release|버전을 릴리즈한 경우|

### 규칙

1. Pull request 충돌 시 반드시 develop을 merge한 후, 충돌을 해결하고 요청하도록 합니다.
2. branch는 기능 단위로 생성하고 명명규칙은 다음과 같게 합니다. ex) feat/Chest, fix/playerMovement
3. 기능 개발 시 반드시 관계 내용을 작성하도록 합니다. ex) 일정 범위 내 플레이어가 들어오면 상자가 반응해서 지정 키를 누르면 상자가 열림.
4. 빌드 관련 변경을 한 경우에는 설명을 상세히 적도록 합니다.
