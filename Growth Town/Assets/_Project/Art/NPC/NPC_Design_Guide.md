# 3등신 고양이 수인형 NPC 디자인 가이드 (포춘시티 스타일)

## 외형 특징 (Geometry)
- **비율**: 3등신 (머리 1, 상체 1, 하체 1)
- **머리**: 귀엽고 둥근 얼굴 베이스에 직관적인 고양이 귀(Cat Ears) 메쉬 포인트
- **팔/다리**: 관절이 꺾이지 않는 원통형(Cylinder) 구조로 단순화하여 귀여움 강조

## 머티리얼 세팅 (URP)
- **Shader**: Universal Render Pipeline/Unlit 또는 Lit(Receive Shadows on, Smoothness 0)
- **Color Palette**: 포춘시티 스타일의 쨍한 파스텔 톤 (Mint, Soft Yellow, Peach)
- **Outline**: 얇은 외곽선(Outline) 효과를 위해 렌더 피쳐(Render Feature) 기반 외곽선 셰이더 적용 권장

## 애니메이션 (Animation)
- 통통 튀는 듯한 Idle 애니메이션 (Y축 바운스)
- 걸을 때 좌우로 뒤뚱거리는 Walk 틱 애니메이션

## 강아지 (Dog) 추가 가이드
- **외형**: 쳐진 귀, 둥근 코, 통통 튀고 활발한 3등신 비율
- **애니메이션**: 꼬리를 흔들며 통통 튀는 걸음걸이

## 앵무새 (Parrot) 추가 가이드
- **외형**: 둥근 부리, 화려한 깃털 색상(빨강/노랑 등), 3등신 비율
- **이동 방식**: 공중을 날지 않고 바닥을 뒤뚱뒤뚱 걷도록 처리
