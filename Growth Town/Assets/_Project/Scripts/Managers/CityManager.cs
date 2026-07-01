using System.Collections.Generic;
using UnityEngine;

namespace LifeTown.Core
{
    public class CityManager : MonoBehaviour
    {
        public GameObject treePrefab;
        public TileMapManager tileMapManager; // Manages the grid/tiles
        
        private int previousTotalBuildingLevel = 0;
        private int currentTotalBuildingLevel = 0;

        /// <summary>
        /// 건물의 레벨이 변경될 때마다 호출되어야 하는 함수입니다.
        /// </summary>
        public void UpdateTotalBuildingLevel(int newTotalLevel)
        {
            previousTotalBuildingLevel = currentTotalBuildingLevel;
            currentTotalBuildingLevel = newTotalLevel;

            CheckTreeSpawnCondition();
        }

        private void CheckTreeSpawnCondition()
        {
            // 총 레벨 합이 5의 배수인지 확인하고, 이전에 5의 배수를 달성한 적이 없는지(또는 이전 레벨의 배수와 다른지) 체크
            if (currentTotalBuildingLevel > 0 && currentTotalBuildingLevel % 5 == 0 && previousTotalBuildingLevel < currentTotalBuildingLevel)
            {
                // 레벨이 증가하여 5의 배수가 되었을 때 스폰 (레벨 하락 후 재달성은 로직에 따라 조율 가능)
                if (currentTotalBuildingLevel / 5 > previousTotalBuildingLevel / 5)
                {
                    TreeSpawn();
                }
            }
        }

        private void TreeSpawn()
        {
            if (tileMapManager == null || treePrefab == null)
            {
                Debug.LogWarning("TreeSpawn 실패: TileMapManager 또는 treePrefab이 할당되지 않았습니다.");
                return;
            }

            // 건물/도로와 인접한 빈 타일 목록 가져오기
            List<Vector2Int> validTiles = GetValidTilesForTree();

            if (validTiles.Count == 0)
            {
                Debug.Log("조건에 맞는 빈 자리가 없어 나무 스폰을 생략합니다.");
                return;
            }

            // 무작위로 1자리 선택
            int randomIndex = Random.Range(0, validTiles.Count);
            Vector2Int spawnPosition = validTiles[randomIndex];

            // 타일에 나무 프리팹 인스턴스화
            Vector3 worldPos = tileMapManager.GetWorldPosition(spawnPosition);
            Instantiate(treePrefab, worldPos, Quaternion.identity, this.transform);

            // 해당 타일을 Occupied(영구 장애물) 상태로 고정
            tileMapManager.SetTileOccupied(spawnPosition, true);
            Debug.Log($"나무 스폰 완료. 위치: {spawnPosition}, 타일 상태 Occupied로 변경됨.");
        }

        private List<Vector2Int> GetValidTilesForTree()
        {
            List<Vector2Int> validTiles = new List<Vector2Int>();
            
            // 모든 빈 타일들을 순회하며 인접 여부 확인 (TileMapManager의 구현에 따라 최적화 필요)
            List<Vector2Int> emptyTiles = tileMapManager.GetAllEmptyTiles();

            foreach (var tilePos in emptyTiles)
            {
                if (IsAdjacentToBuildingOrRoad(tilePos))
                {
                    validTiles.Add(tilePos);
                }
            }

            return validTiles;
        }

        private bool IsAdjacentToBuildingOrRoad(Vector2Int tilePos)
        {
            // 상, 하, 좌, 우 4방향 인접 확인 (대각선 포함 여부에 따라 변경 가능)
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (var dir in directions)
            {
                Vector2Int neighborPos = tilePos + dir;
                
                // 해당 위치에 건물이나 도로가 있는지 확인
                if (tileMapManager.HasBuildingOrRoadAt(neighborPos))
                {
                    return true;
                }
            }

            return false;
        }
    }

    // 예시를 위한 TileMapManager 인터페이스/클래스 구조 (실제 프로젝트 구조에 맞게 수정 필요)
    public class TileMapManager : MonoBehaviour
    {
        public List<Vector2Int> GetAllEmptyTiles() { return new List<Vector2Int>(); }
        public bool HasBuildingOrRoadAt(Vector2Int pos) { return false; }
        public Vector3 GetWorldPosition(Vector2Int gridPos) { return Vector3.zero; }
        public void SetTileOccupied(Vector2Int pos, bool isOccupied) { }
    }
}
