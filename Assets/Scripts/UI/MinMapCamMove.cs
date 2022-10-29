using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;
using Platformer.UIConnection;
using System;

public class MinMapCamMove : MonoBehaviour
{
    [Inject]
    UI_Manager _uiManager;

    [SerializeField]
    private GameObject _character;
    [SerializeField]
    private bool _isTracing;
    [SerializeField]
    private IEnumerator _trace;
    [SerializeField]
    private RenderTexture _miniMap;
    [SerializeField]
    private GameObject _miniMapRawImage, _miniMapPanel;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Vector3 _startPosition;

    void  Awake()
    {
        _trace = Trace();
        _camera.enabled = false;
        CreateTexture();
    }
    public void SetCamPosition(float _panelWidth)
    {
        float _positionX = _panelWidth / 14.5f;
        gameObject.transform.position = new Vector3(_positionX, transform.position.y, transform.position.z);
        _uiManager.MiniMapCameraPosition = gameObject.transform.position;
        //Debug.Log("Camera on position!!!" + '\n' + "Width:" + _screenWidth.ToString());
    }

    public void MiniMapSettings()
    {
        TraceCharacter();
    }

    public void BackOnStart()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _isTracing = false;
    }

    public void CreateTexture()
    {
        _miniMapPanel = _uiManager.MiniMapPanel;
        _miniMapRawImage = _uiManager.MiniMap;
        var y = Math.Abs((int)(_miniMapPanel.GetComponent<RectTransform>().sizeDelta.y));
        var x = Math.Abs((int)(_miniMapPanel.GetComponent<RectTransform>().sizeDelta.x));
        _miniMap = new RenderTexture(x, y, 1, RenderTextureFormat.ARGB32);
        _miniMap.Create();
        _miniMapRawImage.GetComponent<RawImage>().texture = _miniMap;
        gameObject.GetComponent<Camera>().targetTexture = _miniMap;
        Debug.Log("Texture created!!!" + '\n' + x.ToString() + "x" + y.ToString() + '\n' + "Resolution:" + Screen.currentResolution.ToString() + ", w:" + Screen.currentResolution.width.ToString());
        SetCamPosition(x);
        _startPosition = gameObject.transform.position;
        _camera.enabled = true;
    }
    public void SwichTracing()
    {
        _isTracing = !_isTracing;
        {
            if (_isTracing == true)
            {
                TraceCharacter();
            }
            else
            {
                StopTraceCharacter();
            }
        }

    }

    public void TraceCharacter()
    {
        _character = _uiManager.Character;
        transform.LookAt(_character.transform.position);
        StartCoroutine(_trace);
    }

    public void StopTraceCharacter()
    {
        StopCoroutine(_trace);
    }

    private IEnumerator Trace()
    {
        while ((_character != null) & (_isTracing == true))
        {
            transform.LookAt(new Vector3(_character.transform.position.x, gameObject.transform.position.y, 0));
            yield return null;
        }
    }
        //gameObject.transform.position = new Vector3(PersObj.transform.position.x + 45, gameObject.transform.position.y, gameObject.transform.position.z);

}
