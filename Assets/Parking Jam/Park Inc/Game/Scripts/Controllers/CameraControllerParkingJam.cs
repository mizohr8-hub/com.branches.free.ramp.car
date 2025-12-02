#pragma warning disable 649

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Watermelon;
//using UnityEngine.Rendering.Universal;

public class CameraControllerParkingJam : MonoBehaviour
{

    private static CameraControllerParkingJam instance;


    //private static UniversalRenderPipelineAsset URP => instance.urp;
    //[SerializeField] UniversalRenderPipelineAsset urp;


    public static Camera MainCamera { get; private set; }
    public static Vector3 Position { get => instance.transform.position; set => instance.transform.position = value; }
    public static Quaternion Rotation { get => instance.transform.rotation; set => instance.transform.rotation = value; }
    public static Vector3 Forward => instance.transform.forward;
    public static Vector3 Euler { get => instance.transform.eulerAngles; set => instance.transform.eulerAngles = value; }

    private static float hfov;

    public float field = 7;

    void Start()
    {
        instance = this;

        MainCamera = GetComponent<Camera>();
        if (GameControllerParkingJam.IsBossLevel || PlayerPrefs.GetInt("IsChallenges") == 1)
        {
            field = 3;
        }
        else
        {
            field = 7;
        }

        float fovRadians = MainCamera.fieldOfView * Mathf.Deg2Rad;
        hfov = field * Mathf.Atan(Mathf.Tan(fovRadians /field) * MainCamera.aspect); //2

        if (GameControllerParkingJam.IsBossLevel || PlayerPrefs.GetInt("IsChallenges") == 1)
        {
            MainCamera.fieldOfView = 55;
        }
        else
        {
            MainCamera.fieldOfView = 57;
        }

        Invoke(nameof(StartGameInitially), 0.25f);
        //urp.shadowDistance = 50;

        //----------Park Inc----
        //Tween.NextFrame(delegate
        //{
        //    StartCoroutine(FovAnimation(LevelController.Environment.ShowStartPanel));
        //});
    }

    public void BossLevelView()
    {
        field = 3;
        float fovRadians = MainCamera.fieldOfView * Mathf.Deg2Rad;
        hfov = field * Mathf.Atan(Mathf.Tan(fovRadians / field) * MainCamera.aspect); //2
        MainCamera.fieldOfView = 55;
    }

    void StartGameInitially()
    {
        
        UITouchHandler.Enabled = true;
        GameControllerParkingJam.StartGame();
    }

    private IEnumerator FovAnimation(UnityAction action)
    {
        float duration = 1.5f;
        float initial = 45;
        float final = 50;//camera 50

        Ease.EaseFunction ease = Ease.GetFunction(Ease.Type.BackOut);

        float time = 0;
        do
        {
            yield return null;
            time += Time.deltaTime;

            float t = ease(time / duration);

            MainCamera.fieldOfView = initial + (final - initial) * t;

        } while (time < duration);
        MainCamera.fieldOfView = final;

        action?.Invoke();
    }

    public static void Init(Level level)
    {
        Vector3 centerPoint = new Vector3(level.Size.x, 0, level.Size.y) / 2f;

        float width = level.Size.x + 5;//5

        Position = GetCameraPosition(centerPoint, width, Quaternion.Euler(Euler.x, 0, Euler.z) * Vector3.forward);

        Euler = Euler.SetY(0);//0

        //URP.shadowDistance = (Position - Vector3.forward * (level.Size.y + 40)).magnitude;

    }

    public static void ChangeAngleToGamePosition(Level level)
    {

        Vector3 centerPoint = new Vector3(level.Size.x, 0, level.Size.y) / 2f;

        float width = level.Size.magnitude + 1;//+ 1

        Vector3 finalRotation = new Vector3(Euler.x, -15, Euler.z);//-15

        Vector3 finalPosition = GetCameraPosition(centerPoint, width, Quaternion.Euler(finalRotation) * Vector3.forward);

        instance.transform.DOMove(finalPosition, 0.5f).SetEasing(Ease.Type.SineInOut);
        instance.StartCoroutine(DoRotate(finalRotation, 0.5f, Ease.Type.SineInOut));
        instance.StartCoroutine(DoShadowDistance((Position - Vector3.forward * (level.Size.magnitude + 30)).magnitude, 0.5f, Ease.Type.SineInOut)); //30
    }

    public static void ChangeAngleToMenuPosition(Level level)
    {

        Vector3 centerPoint = new Vector3(level.Size.x, 0, level.Size.y) / 2f;

        float width = level.Size.x + 5;

        Vector3 finalRotation = new Vector3(Euler.x, 0, Euler.z);

        Vector3 finalPosition = GetCameraPosition(centerPoint, width, Quaternion.Euler(finalRotation) * Vector3.forward);

        instance.transform.DOMove(finalPosition, 0.5f).SetEasing(Ease.Type.SineInOut);
        instance.StartCoroutine(DoRotate(finalRotation, 0.5f, Ease.Type.SineInOut));
        instance.StartCoroutine(DoShadowDistance((Position - Vector3.forward * (level.Size.magnitude + 30)).magnitude, 0.5f, Ease.Type.SineInOut));
    }

    private static IEnumerator DoShadowDistance(float finalDistance, float duration, Ease.Type ease = Ease.Type.Linear)
    {
        //float initialDistance = URP.shadowDistance;

        Ease.EaseFunction easeFunc = Ease.GetFunction(ease);

        float time = 0;
        do
        {
            yield return null;
            time += Time.deltaTime;

            float t = easeFunc(time / duration);

            //URP.shadowDistance = initialDistance + (finalDistance - initialDistance) * t;

        } while (time < duration);

        //URP.shadowDistance = finalDistance;
    }

    private static IEnumerator DoRotate(Vector3 finalRotation, float duration, Ease.Type ease = Ease.Type.Linear)
    {

        Vector3 initialRotation = Euler;

        if(initialRotation.y > 180)
        {
            initialRotation.y -= 360;
        }

        Ease.EaseFunction easeFunc = Ease.GetFunction(ease);

        float time = 0;

        do
        {
            yield return null;
            time += Time.deltaTime;

            float t = easeFunc(time / duration);

            Euler = initialRotation + (finalRotation - initialRotation) * t;

        } while (time < duration);

        Euler = finalRotation;

    }
    public static IEnumerator ReturnToOriginalFOV()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        float newView = MainCamera.fieldOfView - 10f;
        while (MainCamera.fieldOfView > newView)
        {
            yield return new WaitForSecondsRealtime(0.0001f);
            MainCamera.fieldOfView -= 0.5f;
        }
    }

    public static IEnumerator ChangeFOVForRollerAttack()
    {
        float newView = MainCamera.fieldOfView + 10f;
        while(MainCamera.fieldOfView < newView)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            MainCamera.fieldOfView += 1f;      
        }
    }
    public static void Move(Level level)
    {
        Vector3 centerPoint = new Vector3(level.Size.x, 0, level.Size.y) / 2f;

        float width = level.Size.magnitude + 1;

        Vector3 newCameraPosition = GetCameraPosition(centerPoint, width, Forward);

        instance.transform.DOMove(newCameraPosition, 0.5f).SetEasing(Ease.Type.SineInOut);

    }

    private static Vector3 GetCameraPosition(Vector3 centerPoint, float width, Vector3 forward)
    {
        float length = width / (2f * Mathf.Tan(hfov / 2f));//2  //0.65 for challenge boss ,2 for normal 0.7 for 250 cars

        float height = Mathf.Abs((forward * length).y);

        Vector3 initialPosition = Vector3.up * height;

        Vector3 initialLookPoint = new Vector3(
            x: -height / forward.y * forward.x,
            y: 0,
            z: -height / forward.y * forward.z);

        Vector3 offset = centerPoint - initialLookPoint;

        return initialPosition + offset;
    }




}
