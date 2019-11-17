using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private float shakePower = 0.025f;
    [SerializeField]
    private float shakeDuration = 0.6f;

    public Camera Camera => Camera.allCameras[0];
    public Vector3 CameraStartPos { get; private set; }

    public float ShakePower => shakePower;
    public float ShakeDuration => shakeDuration;

    public static CameraManager Instance { get; private set; } = null;
    
    private IEnumerator Coroutine { get; set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        //float tileSizeInPixels = 64f;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        //Camera.orthographicSize = tileSizeInPixels * Screen.height / Screen.width * 1f;
        float aspectRatio = Camera.aspect; //(width divided by height)
        float camSize = Camera.orthographicSize; //The size value mentioned earlier
        float correctPositionX = aspectRatio * camSize;
        Camera.transform.position = new Vector3(correctPositionX - 0.5f, camSize - 0.5f, -1);

        CameraStartPos = Camera.transform.position;
    }

    public void ShakeCamera()
    {
        Coroutine = ShakeCoroutine();
        StartCoroutine(Coroutine);
    }

    public void StopShakingCamera()
    {
        StopCoroutine(Coroutine);
    }

    private IEnumerator ShakeCoroutine()
    {
        float shakeTimer = ShakeDuration;
        while (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * ShakePower;
            Camera.transform.position = new Vector3(Camera.transform.position.x + shakePos.x, Camera.transform.position.y + shakePos.y, Camera.transform.position.z);
            shakeTimer -= Time.deltaTime;
            yield return null;
        }
        Camera.transform.position = CameraStartPos;
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            StopShakingCamera();
        }
    }
}
