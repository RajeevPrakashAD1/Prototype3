using UnityEngine;
using Cinemachine;

public class Cinetouch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cineCam;
    [SerializeField] TouchPanel touchField;
    [SerializeField] float SenstivityX = 2f;
    [SerializeField] float SenstivityY = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //cineCam. += touchField.TouchDist.x * SenstivityX * Time.deltaTime;
        //cineCam.m_YAxis.Value += touchField.TouchDist.y * SenstivityY * Time.deltaTime;
    }
}