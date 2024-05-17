using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float DefaultSize;
    public float ZoomInSize;

    [Range(0.1f,5)]
    public float Zoomspeed;

    bool zoomingIn = false;

    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        DefaultSize = vcam.m_Lens.OrthographicSize;
    }

    void Update()
    {
        if (GameManager.Instance != null)
        {
            if(GameManager.Instance.playerInConversation && !zoomingIn && vcam.m_Lens.OrthographicSize != ZoomInSize)
            {
                StartCoroutine(ZoomLerp(ZoomInSize));
            }
            else if(!GameManager.Instance.playerInConversation && !zoomingIn && vcam.m_Lens.OrthographicSize != DefaultSize)
            {
                StartCoroutine(ZoomLerp(DefaultSize));
            }
        }
    }

    public IEnumerator ZoomLerp(float goal)
    {
        float currentsize = vcam.m_Lens.OrthographicSize;

        zoomingIn = true;
        float elapsedTime = 0f;
        while (elapsedTime < Zoomspeed)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(currentsize, goal, elapsedTime / Zoomspeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        vcam.m_Lens.OrthographicSize = goal;
        zoomingIn = false;

    }
}
