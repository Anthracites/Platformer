using UnityEngine;

public class CharacterArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlatformLyer")
        {

        }
    }
}

//void DrawCircle(int _steps, float _radius)
//{
//    _lineRenderer.positionCount = _steps;

//    for(int _currentStep = 0; _currentStep < _steps; _currentStep++)
//    {
//        float _circumferenceProgress = (float)_currentStep / _steps;

//        float _currentRadian = _circumferenceProgress * 2 * Mathf.PI;

//        float _xScale = Mathf.Cos(_currentRadian);
//        float _yScale = Mathf.Sin(_currentRadian);

//        float x = _xScale * _radius;
//        float y = _yScale * _radius;

//        Vector3 _currentPosition = new Vector3(x, y, transform.position.z);

//        _lineRenderer.SetPosition(_currentStep, _currentPosition);
//    }
//}
