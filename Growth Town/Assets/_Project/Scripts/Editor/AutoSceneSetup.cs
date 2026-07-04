using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class AutoSceneSetup : EditorWindow
{
    private static readonly string fbxPath = "Assets/_Project/Art/KayKit/fbx(unity)";
    private static readonly string prefabPath = "Assets/_Project/Art/Prefabs/KayKit";

    [MenuItem("Life Town/풀 플레이어블 씬 세팅 (20x20, UI 연동)")]
    public static void SetupScene()
    {
        if (Application.isPlaying) 
        {
            Debug.LogWarning("플레이 모드 중에는 씬을 세팅할 수 없습니다. 상단의 재생(▶) 버튼을 눌러 정지한 후 다시 시도해주세요.");
            return;
        }

        GeneratePrefabs();

        if (GameObject.Find("Managers") != null) 
        {
            Debug.Log("씬이 이미 세팅되어 있습니다. 다시 세팅하려면 기존 오브젝트들을 지워주세요.");
            return;
        }
        
        Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        
        // 1. 카메라 세팅
        Camera mainCam = Camera.main;
        if(mainCam != null)
        {
            mainCam.clearFlags = CameraClearFlags.SolidColor;
            mainCam.backgroundColor = new Color(0.9f, 0.9f, 0.9f);
            mainCam.orthographic = true;
            mainCam.orthographicSize = 25f;
            mainCam.transform.position = new Vector3(-30f, 40f, -30f);
            mainCam.transform.rotation = Quaternion.Euler(30f, 45f, 0f);
            mainCam.gameObject.AddComponent<PhysicsRaycaster>();
        }

        // 2. 통합 매니저 생성 및 시스템 스크립트 연결
        GameObject managersObj = new GameObject("Managers");
        System.Type inputMgrType = GetTypeByName("LifeTown.Core.InputManager");
        if(inputMgrType != null) managersObj.AddComponent(inputMgrType);

        System.Type audioMgrType = GetTypeByName("LifeTown.Managers.AudioManager");
        if(audioMgrType != null) managersObj.AddComponent(audioMgrType);

        // 3. UI 캔버스 완벽 구축
        GenerateUI();

        // 4. 맵 바닥 (거대한 초록 평야)
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(10f, 1f, 10f); // 100x100 size
        Renderer groundRend = ground.GetComponent<Renderer>();
        groundRend.sharedMaterial = new Material(Shader.Find("Standard"));
        groundRend.sharedMaterial.color = new Color(0.4f, 0.8f, 0.4f);
        ground.AddComponent<BoxCollider>();

        // 5. 20x20 포춘시티 스타일 바둑판 도시 생성
        GenerateCityGrid();

        // 6. NPC (고양이/강아지/앵무새 대용 캡슐) 스폰
        GenerateNPCs();

        Debug.Log("🎉 Life Town 풀 플레이어블 씬 자동 세팅 완료! (NPC 추가 및 버그 수정)");
    }

    private static void GeneratePrefabs()
    {
        if (!AssetDatabase.IsValidFolder("Assets/_Project/Art/Prefabs"))
            AssetDatabase.CreateFolder("Assets/_Project/Art", "Prefabs");
        if (!AssetDatabase.IsValidFolder(prefabPath))
            AssetDatabase.CreateFolder("Assets/_Project/Art/Prefabs", "KayKit");

        string[] modelsToProcess = {
            "building_A", "building_B", "building_C", "building_D", 
            "building_E", "building_F", "building_G", "building_H",
            "road_straight", "road_corner", "road_junction", "road_tsplit",
            "car_taxi"
        };

        foreach(string model in modelsToProcess)
        {
            string src = $"{fbxPath}/{model}.fbx";
            string dst = $"{prefabPath}/{model}.prefab";
            
            if (!File.Exists(src)) continue;
            
            GameObject fbxAsset = AssetDatabase.LoadAssetAtPath<GameObject>(src);
            if (fbxAsset != null && AssetDatabase.LoadAssetAtPath<GameObject>(dst) == null)
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(fbxAsset);
                
                // Add Components to buildings
                if (model.StartsWith("building_"))
                {
                    BoxCollider col = instance.AddComponent<BoxCollider>();
                    col.size = new Vector3(2f, 3f, 2f);
                    col.center = new Vector3(0, 1.5f, 0);

                    System.Type mergeAnimType = GetTypeByName("LifeTown.UI.MergeAnimator");
                    if (mergeAnimType != null) instance.AddComponent(mergeAnimType);

                    System.Type buildingType = GetTypeByName("LifeTown.Core.Building");
                    if (buildingType != null) 
                    {
                        var b = instance.AddComponent(buildingType);
                        // 레벨 맵핑 (A=1, B=2...)
                        int level = model[9] - 'A' + 1;
                        SerializedObject so = new SerializedObject(b);
                        so.FindProperty("level").intValue = level;
                        so.ApplyModifiedProperties();
                    }
                }

                PrefabUtility.SaveAsPrefabAsset(instance, dst);
                DestroyImmediate(instance);
            }
        }
    }

    private static void GenerateCityGrid()
    {
        GameObject cityRoot = new GameObject("CityEnvironment");
        
        GameObject roadStraight = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}/road_straight.prefab");
        GameObject roadJunction = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}/road_junction.prefab");
        GameObject buildingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}/building_A.prefab"); // 기본 생성 건물
        
        int gridSize = 20;
        float tileSize = 2f; 

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 pos = new Vector3((x - gridSize/2) * tileSize, 0, (z - gridSize/2) * tileSize);
                GameObject tileObj = null;

                // 5칸마다 도로 (index 4, 9, 14, 19)
                bool isRoadX = ((x + 1) % 5 == 0);
                bool isRoadZ = ((z + 1) % 5 == 0);

                if (isRoadX && isRoadZ)
                {
                    if (roadJunction != null) tileObj = (GameObject)PrefabUtility.InstantiatePrefab(roadJunction);
                }
                else if (isRoadX)
                {
                    if (roadStraight != null) 
                    {
                        tileObj = (GameObject)PrefabUtility.InstantiatePrefab(roadStraight);
                        tileObj.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                }
                else if (isRoadZ)
                {
                    if (roadStraight != null) tileObj = (GameObject)PrefabUtility.InstantiatePrefab(roadStraight);
                }
                else
                {
                    // 4x4 블록 내부: 일정 확률로 1레벨 건물 스폰
                    if (Random.value > 0.6f && buildingPrefab != null)
                    {
                        tileObj = (GameObject)PrefabUtility.InstantiatePrefab(buildingPrefab);
                        
                        // 랜덤으로 도로 쪽을 바라보게 회전
                        float[] rotations = { 0f, 90f, 180f, 270f };
                        tileObj.transform.rotation = Quaternion.Euler(0, rotations[Random.Range(0, 4)], 0);
                    }
                }

                if (tileObj != null)
                {
                    tileObj.transform.position = pos;
                    tileObj.transform.SetParent(cityRoot.transform);
                }
            }
        }
    }

    private static void GenerateUI()
    {
        // EventSystem
        if (Object.FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        // Canvas
        GameObject canvasObj = new GameObject("UICanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100; // Ensure UI is on top
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        canvasObj.AddComponent<GraphicRaycaster>();
        
        System.Type themeType = GetTypeByName("LifeTown.UI.ThemeManager");
        if (themeType != null) canvasObj.AddComponent(themeType);

        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        // Top Bar
        GameObject topBar = CreateUIElement("TopBar", canvasObj.transform, new Vector2(0.5f, 1), new Vector2(0.5f, 1), new Vector2(1080, 150), new Vector2(0, -75));
        Image topImg = topBar.AddComponent<Image>();
        topImg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        GameObject goldTextObj = CreateUIElement("GoldText", topBar.transform, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector2(300, 100), new Vector2(200, 0));
        Text goldText = goldTextObj.AddComponent<Text>();
        goldText.text = "🪙 1,500";
        goldText.font = font; goldText.fontSize = 50; goldText.color = Color.white; goldText.alignment = TextAnchor.MiddleLeft;

        // Bottom Bar
        GameObject bottomBar = CreateUIElement("BottomBar", canvasObj.transform, new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(1080, 200), new Vector2(0, 100));
        
        GameObject invBtnObj = CreateUIElement("InventoryBtn", bottomBar.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(300, 120), new Vector2(0, 0));
        Image invImg = invBtnObj.AddComponent<Image>();
        invImg.color = new Color(0.3f, 0.6f, 0.9f);
        Button invBtn = invBtnObj.AddComponent<Button>();

        GameObject invBtnText = CreateUIElement("Text", invBtnObj.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(300, 120), Vector2.zero);
        Text btnText = invBtnText.AddComponent<Text>();
        btnText.text = "📦 인벤토리";
        btnText.font = font; btnText.fontSize = 45; btnText.color = Color.white; btnText.alignment = TextAnchor.MiddleCenter;

        // Inventory Panel
        GameObject invPanel = CreateUIElement("InventoryPanel", canvasObj.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(900, 1200), Vector2.zero);
        Image panelImg = invPanel.AddComponent<Image>();
        panelImg.color = new Color(0.9f, 0.9f, 0.95f, 1f);
        invPanel.SetActive(false); // Default hidden

        GameObject closeBtnObj = CreateUIElement("CloseBtn", invPanel.transform, new Vector2(1, 1), new Vector2(1, 1), new Vector2(100, 100), new Vector2(-60, -60));
        Image closeImg = closeBtnObj.AddComponent<Image>();
        closeImg.color = Color.red;
        Button closeBtn = closeBtnObj.AddComponent<Button>();
        
        GameObject closeTxt = CreateUIElement("Text", closeBtnObj.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(100, 100), Vector2.zero);
        Text cTxt = closeTxt.AddComponent<Text>();
        cTxt.text = "X"; cTxt.font = font; cTxt.fontSize = 60; cTxt.color = Color.white; cTxt.alignment = TextAnchor.MiddleCenter;

        GameObject titleObj = CreateUIElement("Title", invPanel.transform, new Vector2(0.5f, 1), new Vector2(0.5f, 1), new Vector2(400, 100), new Vector2(0, -60));
        Text titleTxt = titleObj.AddComponent<Text>();
        titleTxt.text = "내 인벤토리"; titleTxt.font = font; titleTxt.fontSize = 60; titleTxt.color = Color.black; titleTxt.alignment = TextAnchor.MiddleCenter;

        // Attach UI Script
        System.Type shopInvType = GetTypeByName("LifeTown.UI.ShopInventoryUI");
        if (shopInvType != null)
        {
            var shopUI = canvasObj.AddComponent(shopInvType);
            SerializedObject so = new SerializedObject(shopUI);
            so.FindProperty("uiPanel").objectReferenceValue = invPanel.GetComponent<RectTransform>();
            so.FindProperty("closeButton").objectReferenceValue = closeBtn;
            so.FindProperty("openButton").objectReferenceValue = invBtn;
            so.ApplyModifiedProperties();
        }
    }

    private static GameObject CreateUIElement(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, Vector2 size, Vector2 pos)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        RectTransform rt = go.AddComponent<RectTransform>();
        rt.anchorMin = anchorMin; rt.anchorMax = anchorMax;
        rt.sizeDelta = size; rt.anchoredPosition = pos;
        return go;
    }

    private static void GenerateNPCs()
    {
        GameObject cityRoot = GameObject.Find("CityEnvironment");
        System.Type npcType = GetTypeByName("LifeTown.Core.SimpleNPC");

        for (int i = 0; i < 15; i++)
        {
            GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            npc.name = $"NPC_{i}";
            npc.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            npc.transform.position = new Vector3(Random.Range(-15f, 15f), 0.5f, Random.Range(-15f, 15f));
            
            Renderer r = npc.GetComponent<Renderer>();
            r.sharedMaterial = new Material(Shader.Find("Standard"));
            r.sharedMaterial.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.8f, 1f);

            if (npcType != null) npc.AddComponent(npcType);
            if (cityRoot != null) npc.transform.SetParent(cityRoot.transform);
        }
    }

    private static System.Type GetTypeByName(string className)
    {
        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.FullName == className || type.Name == className)
                    return type;
            }
        }
        return null;
    }
}
