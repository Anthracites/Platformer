using UnityEngine;

public class SwichCameraControl : MonoBehaviour
{
    private string PersTag = "Pers";
    public GameObject MinMapCam;


    void OnTriggerEnter2D(Collider2D other)
    {
        bool _isMove = MinMapCam.GetComponent<MinMapCamMove>().enabled;
        if (other.tag == PersTag)
        {
            MinMapCam.GetComponent<MinMapCamMove>().enabled = !_isMove;
        }
    }
}
