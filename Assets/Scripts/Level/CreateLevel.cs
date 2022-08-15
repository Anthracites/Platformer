using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLevel : MonoBehaviour // Создание уровня
{
    [SerializeField]
    private int _seed;
    [SerializeField]
    private GameObject[] _platformPrefabs;
    [SerializeField]
    private GameObject _platforms, _pillars;
    [SerializeField]
    private GameObject StartPlatformPref;
    [SerializeField]
    private GameObject EndPlatformPref;
    [SerializeField]
    private GameObject PersPref;
    [SerializeField]
    private GameObject CamTriggerOnPref;
    [SerializeField]
    private GameObject CamTriggerOffPref;
    [SerializeField]
    private GameObject GamePadObj;
    [SerializeField]
    private GameObject SceneCamera;
    [SerializeField]
    private GameObject MinMap;
    [SerializeField]
    private GameObject SliderProgersShow;
    [SerializeField]
    private GameObject Pers;
    [SerializeField]
    private GameObject BorderPref;
    [SerializeField]
    private List<float> PlatformSizes = new List<float>();
    [SerializeField]
    private List<float> PlatformSizeY = new List<float>();
    [SerializeField]
    private List<GameObject> LevelPlatforms = new List<GameObject>();
    [SerializeField]
    private List<Vector3> SpawnBorderPos = new List<Vector3>();
    [SerializeField]
    private int LevelNumber = 1, _lastPlatformIndex;

    [SerializeField]
    private float CoordX, CoordY, CoordZ = 0;
    [SerializeField]
    private float PlatformSizeX, PlatformBSizeX, PlatformFSizeX;

    [SerializeField]
    private float StartArcPoint, EndArcPoint;
    [SerializeField]
    private float RangeRplatform = 0;
    [SerializeField]
    private Button JumpButtonObj;
    [SerializeField]
    private Button FireButtonLObj;
    [SerializeField]
    private Button FireButtonRObj;
    [SerializeField]
    private bool UseSeed;
    [SerializeField]
    private Vector3 SpawnPositionArc;


    void Start()
    {
        BuildLevel();
    }

    public void BuildLevel()
    {
        GetSeed();
        GetSizesPlatform();
        SpawnStartPlatform();
        PersCreate();
        PlatformSpawn();
        SpawnEndPlatform();
        ShowProgresParam();
        SpawnCamTriggers();
        SpawnPillars();
    }
    void GetSeed()
    {
        if (UseSeed == true)
        {
            Random.seed = _seed;
        }
    }

    void GetSizesPlatform()
    {
        int i = 0;
        foreach (GameObject Platform in _platformPrefabs)
        {
            PlatformSizeX = _platformPrefabs[i].GetComponent<SpriteRenderer>().bounds.size.x;
            i++;
            PlatformSizes.Add(PlatformSizeX);
        }
    }

    void PlatformSpawn() // Спавн платформ

    {
        CoordX = -1;

        for (int k = gameObject.transform.childCount; k > 0; --k)
        {
            DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
        }
        CoordX = CoordX + 5;
        int j = 0;
        int i = 0;
        while (i < LevelNumber * 100)
        {
            Quaternion spawnRotation = Quaternion.identity;
            j = Random.Range(0, _platformPrefabs.Length);
            CoordY = Random.Range(-4, 2);
            CoordX = CoordX + (PlatformSizes[j] * 0.5f) + (PlatformBSizeX * 0.5f);
            Vector3 SpawnPosition = new Vector3(CoordX, CoordY, CoordZ);
            GameObject inst_obj = Instantiate(_platformPrefabs[j], SpawnPosition, spawnRotation);
            inst_obj.transform.parent = gameObject.transform;
            PlatformBSizeX = PlatformSizes[j];
            PlatformSizeY.Add(inst_obj.GetComponent<SpriteRenderer>().bounds.size.y);
            SpawnBorderPos.Add(SpawnPosition);
            i++;
        }
        _lastPlatformIndex = j;
    }

    void SpawnStartPlatform() // Спавн первой платформы
    {
        SpawnPositionArc = new Vector3(CoordX, CoordY, -1);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject inst_obj = Instantiate(StartPlatformPref, SpawnPositionArc, spawnRotation);
        inst_obj.transform.parent = _platforms.transform;
        StartArcPoint = SpawnPositionArc.x;
    }


    void SpawnEndPlatform() // Спавн последней платформы
    {
        CoordX = CoordX + PlatformSizes[_lastPlatformIndex] * 2;
        SpawnPositionArc = new Vector3(CoordX, CoordY, -1);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject inst_obj = Instantiate(EndPlatformPref, SpawnPositionArc, spawnRotation);
        inst_obj.transform.parent = _platforms.transform;
        LevelPlatforms.Add(inst_obj);
        EndArcPoint = SpawnPositionArc.x;
    }

    void PersCreate() //Спавн персонажа
    {
        CoordY = CoordY + 2.4f;
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 SpawnPositionArc = new Vector3(CoordX, CoordY, -1);
        Pers = Instantiate(PersPref, SpawnPositionArc, spawnRotation);
        Pers.GetComponent<PersStats>().GamePad = GamePadObj;
        Pers.GetComponent<PersStats>().JumpButton = JumpButtonObj;
        Pers.GetComponent<Shooting>().FireButtonL = FireButtonLObj;
        Pers.GetComponent<Shooting>().FireButtonR = FireButtonRObj;
        SceneCamera.GetComponent<CameraMove>().PersObj = Pers;
        MinMap.GetComponent<MinMapCamMove>().PersObj = Pers;
    }

    void ShowProgresParam()
        {
        SliderProgersShow.GetComponent<ProgressShow>().PersObj = Pers;
        SliderProgersShow.GetComponent<ProgressShow>().StartCoordX = StartArcPoint;
        SliderProgersShow.GetComponent<ProgressShow>().EndCoordX = EndArcPoint;
        SliderProgersShow.SetActive(true);
        }

    void SpawnCamTriggers()
    {

        Vector3 TriggerOffPos1 = new Vector3(100, 0, -1);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject inst_obj = Instantiate(CamTriggerOffPref, TriggerOffPos1, spawnRotation);
        inst_obj.GetComponent<SwichCameraControl>().MinMapCam = MinMap;

        Vector3 TriggerOnPos1 = new Vector3(110, 0, -1);
        inst_obj = Instantiate(CamTriggerOnPref, TriggerOnPos1, spawnRotation);
        inst_obj.GetComponent<SwichCameraControl>().MinMapCam = MinMap;

        Vector3 TriggerOnPos2 = new Vector3(SpawnPositionArc.x - 110, 0, -1);
        inst_obj = Instantiate(CamTriggerOnPref, TriggerOnPos2, spawnRotation);
        inst_obj.GetComponent<SwichCameraControl>().MinMapCam = MinMap;

        Vector3 TriggerOffPos2 = new Vector3(SpawnPositionArc.x - 100, 0, -1);
        inst_obj = Instantiate(CamTriggerOffPref, TriggerOffPos2, spawnRotation);
        inst_obj.GetComponent<SwichCameraControl>().MinMapCam = MinMap;
    }

    void SpawnPillars()
    {
        float SizeY = BorderPref.GetComponent<SpriteRenderer>().bounds.size.y;
        int i = 0;
        while (i < LevelNumber * 25)
        {
            Quaternion spawnRotation = Quaternion.identity;
            int j = Random.Range(0, SpawnBorderPos.Count - 1);
            Vector3 SpawnPosition = SpawnBorderPos[j];
            GameObject inst_obj = Instantiate(BorderPref, new Vector3 (SpawnPosition.x, SpawnPosition.y + (PlatformSizeY[j] * 0.5f) + (SizeY * 0.5f) - 0.25f, SpawnPosition.z), spawnRotation);
            inst_obj.transform.parent = _pillars.transform;
            PlatformSizeY.RemoveAt(j);
            SpawnBorderPos.RemoveAt(j);
            inst_obj.GetComponent<BorderCS>().Pers = Pers;
            i++;
        }
    }
}
