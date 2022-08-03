using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLevel : MonoBehaviour // Создание уровня
{
    public GameObject[] Platforms;
    public GameObject StartPlatformPref;
    public GameObject EndPlatformPref;
    public GameObject PersPref;
    public GameObject CamTriggerOnPref;
    public GameObject CamTriggerOffPref;
    public GameObject inst_obj;
    public GameObject GamePadObj;
    public GameObject CanvasPlayObj;
    public GameObject SceneCamera;
    public GameObject MinMap;
    public GameObject SliderProgersShow;
    public GameObject Pers;
    public GameObject BorderPref;
    public List<float> PlatformSizes = new List<float>();
    public List<float> PlatformSizeY = new List<float>();
    public List<GameObject> LevelPlatforms = new List<GameObject>();
    public List<Vector3> SpawnBorderPos = new List<Vector3>();

    public int LevelNumber = 1;
    public int i;
    public int j;
    public int R;
    public int k;
    public float CoordX;
    public float CoordY;
    public float CoordZ = 0;
    public float PlatformSizeX;
    public float PlatformBSizeX;
    public float PlatformFSizeX;
    public float StartArcPoint;
    public float EndArcPoint;
    public float RangeRplatform = 0;
    public Vector3 SpawnPositionArc;
    [SerializeField]
    private Button JumpButtonObj;
    [SerializeField]
    private Button FireButtonLObj;
    [SerializeField]
    private Button FireButtonRObj;
    public bool UseSeed;


    void Awake()
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
            Random.seed = R;
        }
    }

    void GetSizesPlatform()
    {
        int l = 0;
        foreach (GameObject Platform in Platforms)
        {
            PlatformSizeX = Platforms[l].GetComponent<SpriteRenderer>().bounds.size.x;
            l++;
            PlatformSizes.Add(PlatformSizeX);
        }
    }

    void PlatformSpawn() // Спавн платформ

    {
        CoordX = -1;
        i = 0;
        k = 0;

        for (k = gameObject.transform.childCount; k > 0; --k)
        {
            DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
        }
        CoordX = CoordX + 5;

        while (i < LevelNumber * 100)
        {
            Quaternion spawnRotation = Quaternion.identity;
            j = Random.Range(0, Platforms.Length);
            CoordY = Random.Range(-4, 2);
            CoordX = CoordX + (PlatformSizes[j] * 0.5f) + (PlatformBSizeX * 0.5f);
            Vector3 SpawnPosition = new Vector3(CoordX, CoordY, CoordZ);
            inst_obj = Instantiate(Platforms[j], SpawnPosition, spawnRotation) as GameObject;
            inst_obj.transform.parent = gameObject.transform;
            PlatformBSizeX = PlatformSizes[j];
            PlatformSizeY.Add(inst_obj.GetComponent<SpriteRenderer>().bounds.size.y);
            SpawnBorderPos.Add(SpawnPosition);
            i++;
        }
    }

    void SpawnStartPlatform() // Спавн первой платформы
    {
        SpawnPositionArc = new Vector3(CoordX, CoordY, -1);
        Quaternion spawnRotation = Quaternion.identity;
        inst_obj = Instantiate(StartPlatformPref, SpawnPositionArc, spawnRotation) as GameObject;
        StartArcPoint = SpawnPositionArc.x;
    }


    void SpawnEndPlatform() // Спавн последней платформы
    {
        CoordX = CoordX + PlatformSizes[j] * 2;
        SpawnPositionArc = new Vector3(CoordX, CoordY, -1);
        Quaternion spawnRotation = Quaternion.identity;
        inst_obj = Instantiate(EndPlatformPref, SpawnPositionArc, spawnRotation) as GameObject;
        LevelPlatforms.Add(inst_obj);
        EndArcPoint = SpawnPositionArc.x;
    }

    void PersCreate() //Спавн персонажа
    {
        CoordY = CoordY + 2.4f;
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 SpawnPositionArc = new Vector3(CoordX, CoordY, -1);
        Pers = Instantiate(PersPref, SpawnPositionArc, spawnRotation) as GameObject;
        Pers.GetComponent<PersStats>().GamePad = GamePadObj;
        Pers.GetComponent<PersStats>().CanvasControlObj = CanvasPlayObj;
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
        inst_obj = Instantiate(CamTriggerOffPref, TriggerOffPos1, spawnRotation) as GameObject;
        inst_obj.GetComponent<OffCamControl>().MinMapCam = MinMap;

        Vector3 TriggerOnPos1 = new Vector3(110, 0, -1);
        inst_obj = Instantiate(CamTriggerOnPref, TriggerOnPos1, spawnRotation) as GameObject;
        inst_obj.GetComponent<OnCamControl>().MinMapCam = MinMap;

        Vector3 TriggerOnPos2 = new Vector3(SpawnPositionArc.x - 110, 0, -1);
        inst_obj = Instantiate(CamTriggerOnPref, TriggerOnPos2, spawnRotation) as GameObject;
        inst_obj.GetComponent<OnCamControl>().MinMapCam = MinMap;

        Vector3 TriggerOffPos2 = new Vector3(SpawnPositionArc.x - 100, 0, -1);
        inst_obj = Instantiate(CamTriggerOffPref, TriggerOffPos2, spawnRotation) as GameObject;
        inst_obj.GetComponent<OffCamControl>().MinMapCam = MinMap;
    }

    void SpawnPillars()
    {
        float SizeY = BorderPref.GetComponent<SpriteRenderer>().bounds.size.y;
        i = 0;
        while (i < LevelNumber * 25)
        {
            Quaternion spawnRotation = Quaternion.identity;
            j = Random.Range(0, SpawnBorderPos.Count - 1);
            Vector3 SpawnPosition = SpawnBorderPos[j];
            inst_obj = Instantiate(BorderPref, new Vector3 (SpawnPosition.x, SpawnPosition.y + (PlatformSizeY[j] * 0.5f) + (SizeY * 0.5f) - 0.25f, SpawnPosition.z), spawnRotation) as GameObject;
            PlatformSizeY.RemoveAt(j);
            SpawnBorderPos.RemoveAt(j);
            inst_obj.GetComponent<BorderCS>().Pers = Pers;
            i++;
        }
    }
}
