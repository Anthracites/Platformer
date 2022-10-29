using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Platformer.UIConnection;
using Doozy.Engine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System;

namespace Platformer.GamePlay
{
    public class CreateLevel : MonoBehaviour // Создание уровня
    {
        [Inject]
        PlatformController.Factory _platformFactory;
        [Inject]
        CharacterConroller.Factory _characterFactory;
        [Inject]
        SwichCameraControl.Factory _swichCameraControl;
        [Inject]
        UI_Manager _uiManager;
        [Inject]
        BorderCS.Factory _borderFactory;
        [Inject]
        GamePlay_Manager _gamePlayManager;

        [SerializeField]
        private int _seed;
        [SerializeField]
        private long y;
        [SerializeField]
        private Sprite[] _platformSprites; 
        [SerializeField]
        private GameObject _platforms, _pillars, _camTriggers;
        [SerializeField]
        private GameObject StartPlatformPref;
        [SerializeField]
        private GameObject EndPlatformPref;
        [SerializeField]
        private GameObject SceneCamera;
        [SerializeField]
        private GameObject MinMap;
        [SerializeField]
        private GameObject SliderProgersShow;
        [SerializeField]
        private List<float> PlatformSizes = new List<float>();
        [SerializeField]
        private List<float> PlatformSizeY = new List<float>();
        [SerializeField]
        private List<Vector3> LevelPillars = new List<Vector3>();
        [SerializeField]
        private List<Vector3> SpawnBorderPos = new List<Vector3>();
        [SerializeField]
        private int LevelNumber = 1, _lastPlatformIndex;
        [SerializeField]
        private float CoordX, CoordY, CoordZ = 0, _characterSpawnY;
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
        private bool _useSeed;
        [SerializeField]
        private Vector3 SpawnPositionArc;
        [SerializeField]
        private int _buildCount;



        private void Start()
        {
            _buildCount = 0;
        }

        public void BuildLevel()
        {
            CleanLevel();
            GetSettins();
            GetSizesPlatform();
            SpawnStartPlatform();
            //
            PlatformSpawn();
            SpawnEndPlatform();
            SpawnCamTriggers();
            SpawnPillars();
            PersCreate();
            _gamePlayManager.IsContunueLevelEnable = true;


            //SerilizeToFile();
        }
        void GetSettins()
        {
            GetSeed();
            if (_gamePlayManager.LevelNumber != 0)
            {
                LevelNumber = _gamePlayManager.LevelNumber;
            }
            else
            {
                LevelNumber = 1;
            }
        }
        private void DestroyLevelPart(GameObject _partParent)
        {
            for (int k = _partParent.transform.childCount; k > 0; --k)
            {
                DestroyImmediate(_partParent.transform.GetChild(0).gameObject);
            }
        }
        public void CleanLevel()
        {
            DestroyLevelPart(_platforms);
            DestroyLevelPart(_pillars);
            DestroyLevelPart(_camTriggers);
            PlatformSizes.Clear();
            PlatformSizeY.Clear();
            LevelPillars.Clear();
            SpawnBorderPos.Clear();
            PlatformSizeX = 0;
            PlatformBSizeX = 0;
            PlatformFSizeX = 0;
            _characterSpawnY = 0;
            CoordX = 0;
            CoordY = 0;
            CoordZ = 0;
            GameEventMessage.SendEvent(EventsLibrary.LeveCleared);
        }

        void GetSeed()
        {
            _useSeed = _gamePlayManager.UseSeed;
            if (_useSeed == true)
            {
                _seed = _gamePlayManager.Seed;
            }
            else
            {
                UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                bool _b;
                int i = UnityEngine.Random.Range(-10, 10);
                if (i >= 0)
                {
                    _b = true;
                }
                else
                {
                    _b = false;
                }

                int s = (int)DateTime.Now.Ticks;
                if (_b== true)
                {
                    _seed = -s;
                }
                else
                {
                    _seed = s;
                }
                y = DateTime.Now.Ticks;
                _gamePlayManager.Seed = _seed;
            }
            UnityEngine.Random.InitState(_seed);
        }

        void GetSizesPlatform()
        {
            _platformSprites = Resources.LoadAll<Sprite>("Sprites/Platforms");
            int i = 0;
            foreach (Sprite platform in _platformSprites)
            {
                PlatformSizeX = _platformSprites[i].bounds.size.x;
                i++;
                PlatformSizes.Add(PlatformSizeX);
            }
        }


        void PlatformSpawn() // Спавн платформ
        {
            int j = 0;
            int i = 0;
            Debug.Log("seed1 = " + _seed.ToString());

            while (i < LevelNumber * 100)
            {
                var _platform = _platformFactory.Create(PrefabsPathLibrary.Platform).gameObject;


                j = UnityEngine.Random.Range(0, _platformSprites.Length);
                CoordY = UnityEngine.Random.Range(-4, 2);
                CoordX += (PlatformSizes[j] * 0.5f) + (PlatformBSizeX * 0.5f);

                _platform.GetComponent<SpriteRenderer>().sprite = _platformSprites[j];
                _platform.AddComponent<PolygonCollider2D>();

                _platform.transform.position = new Vector3(CoordX, CoordY, CoordZ);
                _platform.transform.parent = _platforms.transform;

                PlatformBSizeX = PlatformSizes[j];
                PlatformSizeY.Add(_platformSprites[j].bounds.size.y);
                SpawnBorderPos.Add(_platform.transform.position);
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
            _uiManager.StartPoint = StartArcPoint;
            _characterSpawnY = CoordY + 5f;

            var a = StartPlatformPref.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            var b = StartPlatformPref.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            CoordX += (a + b)/2;
        }


        void SpawnEndPlatform() // Спавн последней платформы
        {
            var a = EndPlatformPref.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            var b = EndPlatformPref.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            CoordX += PlatformSizes[_lastPlatformIndex] + (a + b)/2;
            SpawnPositionArc = new Vector3(CoordX, CoordY, -1);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject inst_obj = Instantiate(EndPlatformPref, SpawnPositionArc, spawnRotation);
            inst_obj.transform.parent = _platforms.transform;
            EndArcPoint = SpawnPositionArc.x;
            _uiManager.EndPoint = EndArcPoint;
        }

        void PersCreate() //Спавн персонажа
        {
            var _character = _characterFactory.Create(PrefabsPathLibrary.Character);
            Vector3 _spawnPositionArc = new Vector3(StartArcPoint, _characterSpawnY, -1);
            _character.transform.position = _spawnPositionArc;
            _uiManager.Character = _character.gameObject;
            GameEventMessage.SendEvent(EventsLibrary.CharacterCreated);
        }

        void SpawnCamTriggers()
        {
            Vector3 _firstTriggerPoint = new Vector3(_uiManager.MiniMapCameraPosition.x, 0, -1);
            Vector3 _lastTriggerPoint = new Vector3(EndArcPoint - _firstTriggerPoint.x, 0, -1);

            var _MiniMapCameraFirstTrigger = _swichCameraControl.Create(PrefabsPathLibrary.MiniMapCameraTrigger).gameObject;

            _MiniMapCameraFirstTrigger.transform.position = _firstTriggerPoint;
            _MiniMapCameraFirstTrigger.transform.SetParent(_camTriggers.transform);

            var _MiniMapCameraSecondTrigger = _swichCameraControl.Create(PrefabsPathLibrary.MiniMapCameraTrigger).gameObject;

            _MiniMapCameraSecondTrigger.transform.position = _lastTriggerPoint;
            _MiniMapCameraSecondTrigger.transform.SetParent(_camTriggers.transform);
        }

        void SpawnPillars()
        {
            int i = 0;
            while (i < LevelNumber * 25)
            {
                int j = UnityEngine.Random.Range(0, SpawnBorderPos.Count - 1);
                var inst_obj = _borderFactory.Create(PrefabsPathLibrary.IcePillar);
               // float SizeY = inst_obj.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
                Vector3 SpawnPosition = new Vector3(SpawnBorderPos[j].x, (SpawnBorderPos[j].y + (PlatformSizeY[j] * 0.5f) - 0.25f), SpawnBorderPos[j].z);
                float _scale = UnityEngine.Random.Range(0.50f, 2.00f);
                Vector3 _pillarSize = new Vector3(_scale, _scale, 1.0f);
                inst_obj.transform.position = SpawnPosition;
                inst_obj.transform.localScale = _pillarSize;
                inst_obj.transform.parent = _pillars.transform;
                LevelPillars.Add(inst_obj.transform.position);
                PlatformSizeY.RemoveAt(j);
                SpawnBorderPos.RemoveAt(j);
                i++;
            }
            GameEventMessage.SendEvent(EventsLibrary.LevelCreated);
        }

        private void SerilizeToFile()
        {
            string _a = _useSeed.ToString() + '\n' + _seed.ToString() + '\n' + "Borders" + ":" + '\n';

            foreach (Vector3 _coord in SpawnBorderPos)
            {
                _a += _coord.ToString();

            }
            _a += '\n' + "Pillars" + ":" + '\n';
            foreach (Vector3 _coord in LevelPillars)
            {
                _a += _coord.ToString();
            }
            _a += _useSeed.ToString();
            string jsonString = _a;
            var path = Path.Combine(Application.dataPath + "/Jsons", "MyJson" + _buildCount.ToString() + ".json");
            File.WriteAllText(path, jsonString);
            _buildCount++;
        }
    }
}