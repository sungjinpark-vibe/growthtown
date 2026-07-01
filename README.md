# GrowthTown

Unity C# 기반 앱 개발 프로젝트입니다.

## 기술 스택

| 항목 | 기술 |
|------|------|
| 엔진 | Unity 2022.3 LTS |
| 언어 | C# |
| UI | Unity UGUI + TextMeshPro |
| 백엔드 | Firebase (Firestore, Auth, Remote Config) |
| 애니메이션 | Unity Animator + DOTween |
| 버전 관리 | Git + Git LFS |

## 프로젝트 구조

```
Assets/
├── _Project/
│   ├── Scripts/       # C# 스크립트
│   ├── Prefabs/       # Unity Prefab
│   ├── Scenes/        # Unity Scene
│   ├── Art/           # 텍스처, 스프라이트, 3D 모델
│   └── Audio/         # 배경음악, 효과음
└── Plugins/           # 서드파티 SDK (Firebase 등)
```

## 시작하기

1. 이 저장소를 클론합니다.
   ```bash
   git clone https://github.com/sungjinpark-vibe/growthtown.git
   ```
2. Git LFS를 초기화합니다.
   ```bash
   git lfs install
   git lfs pull
   ```
3. Unity Hub에서 `D:\project\growthtown` 폴더를 프로젝트로 열기합니다.
4. Unity 패키지 임포트가 완료되면 개발을 시작합니다.

## 브랜치 전략

| 브랜치 | 용도 |
|--------|------|
| `main` | 프로덕션 릴리즈 |
| `develop` | 통합 개발 브랜치 |
| `feature/*` | 기능 개발 |
| `fix/*` | 버그 수정 |

## 커밋 메시지 컨벤션

```
feat: 새로운 기능 추가
fix: 버그 수정
refactor: 코드 리팩토링 (기능 변경 없음)
art: 에셋/그래픽 추가 또는 수정
test: 테스트 코드 추가
docs: 문서 수정
chore: 빌드 설정, 패키지 업데이트
```
