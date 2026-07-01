using System;
using UnityEngine;

namespace GrowthTown.Core
{
    public static class GamePolicy
    {
        // 최소 기록 인정 시간 (초 단위, 5분 = 300초)
        public const float MINIMUM_RECORD_TIME_SECONDS = 300f;

        // 나무 스폰 레벨 간격
        public const int TREE_SPAWN_INTERVAL = 5;

        /// <summary>
        /// 세션 기록이 유효한지 검증합니다.
        /// 5분 미만의 기록은 어뷰징 방지를 위해 무효 처리합니다.
        /// </summary>
        /// <param name="elapsedTimeSeconds">진행된 시간(초)</param>
        /// <returns>유효성 여부</returns>
        public static bool IsValidSessionRecord(float elapsedTimeSeconds)
        {
            if (elapsedTimeSeconds < MINIMUM_RECORD_TIME_SECONDS)
            {
                Debug.LogWarning($"[GamePolicy] 기록 무효: 진행 시간({elapsedTimeSeconds}초)이 최소 요구 시간인 {MINIMUM_RECORD_TIME_SECONDS}초(5분) 미만입니다.");
                return false;
            }
            return true;
        }

        // 여기에 추가적인 예외 처리 규칙 및 게임 정책을 명시할 수 있습니다.
    }
}
