using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoSceneSetup : EditorWindow
{
    [MenuItem("Life Town/원클릭 씬 자동 세팅 (Auto Setup)")]
    [InitializeOnLoadMethod]
    public static void SetupScene()
    {
        if (Application.isPlaying) 
        {
            Debug.LogWarning("플레이 모드 중에는 씬을 세팅할 수 없습니다. 상단의 재생(▶) 버튼을 눌러 정지한 후 다시 시도해주세요.");
            return;
        }

        // Prevent running multiple times
        if (GameObject.Find("Managers") != null) return;
        
        // 1. 새 씬 생성
        Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        
        // 2. 메인 카메라 아이소메트릭 뷰(쿼터뷰) 세팅
        Camera mainCam = Camera.main;
        if(mainCam != null)
        {
            mainCam.clearFlags = CameraClearFlags.SolidColor;
            mainCam.backgroundColor = new Color(0.9f, 0.9f, 0.9f); // 연한 회색 배경
            mainCam.orthographic = true;
            mainCam.orthographicSize = 10f;
            mainCam.transform.position = new Vector3(-10f, 10f, -10f);
            mainCam.transform.rotation = Quaternion.Euler(30f, 45f, 0f);
        }

        // 3. 통합 매니저 오브젝트 생성
        GameObject managersObj = new GameObject("Managers");
        
        string[] managerNames = new string[] 
        {
            "AudioManager", "CityManager", "EconomyManager", "EventManager", 
            "InventoryManager", "LightingManager", "QuestManager", 
            "ShopManager", "TimeTrackingManager", "VisitorManager", "WeatherManager"
        };

        foreach(string name in managerNames)
        {
            System.Type type = GetTypeByName(name);
            if(type != null) managersObj.AddComponent(type);
        }

        // 4. UI 캔버스 뼈대 생성 및 텍스트 추가
        GameObject canvasObj = new GameObject("UICanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObj.AddComponent<GraphicRaycaster>();
        
        GameObject textObj = new GameObject("StatusText");
        textObj.transform.SetParent(canvasObj.transform, false);
        Text uiText = textObj.AddComponent<Text>();
        uiText.text = "Life Town Engine Running...\nPress PLAY and check the Console!";
        uiText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        uiText.fontSize = 40;
        uiText.color = Color.black;
        uiText.alignment = TextAnchor.MiddleCenter;
        RectTransform rt = textObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1); rt.anchorMax = new Vector2(1, 1);
        rt.pivot = new Vector2(0.5f, 1); rt.sizeDelta = new Vector2(0, 150);
        rt.anchoredPosition = new Vector2(0, -50);
        
        // 5. 맵 바닥(Plane) 및 가짜 건물(Cube) 세팅
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(5f, 1f, 5f);
        Renderer groundRend = ground.GetComponent<Renderer>();
        groundRend.sharedMaterial = new Material(Shader.Find("Standard"));
        groundRend.sharedMaterial.color = new Color(0.4f, 0.8f, 0.4f); // 잔디색
        
        System.Type navMeshType = GetTypeByName("Unity.AI.Navigation.NavMeshSurface");
        if(navMeshType != null) ground.AddComponent(navMeshType);

        // 더미 건물(큐브) 3개 생성
        for(int i=0; i<3; i++)
        {
            GameObject dummyBuilding = GameObject.CreatePrimitive(PrimitiveType.Cube);
            dummyBuilding.name = $"Dummy_Building_Lv{i+1}";
            dummyBuilding.transform.position = new Vector3(i * 3 - 3, 0.5f, i * 2 - 2);
            dummyBuilding.transform.localScale = new Vector3(1.5f, 1f + (i*0.5f), 1.5f);
            
            Renderer r = dummyBuilding.GetComponent<Renderer>();
            r.sharedMaterial = new Material(Shader.Find("Standard"));
            r.sharedMaterial.color = i == 0 ? Color.red : (i == 1 ? Color.yellow : Color.blue);
        }

        Debug.Log("🎉 Life Town 기본 씬 자동 세팅이 완료되었습니다! 씬을 저장(Ctrl+S)하고 빌드를 진행하세요.");
    }

    private static System.Type GetTypeByName(string className)
    {
        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.Name == className)
                    return type;
            }
        }
        return null;
    }
}
