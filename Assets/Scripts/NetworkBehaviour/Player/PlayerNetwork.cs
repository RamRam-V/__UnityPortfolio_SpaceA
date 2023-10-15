using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerProtocol;
using Player;

public class PlayerNetwork : NetworkBehaviour
{
    public PlayerAnimationData animData;
    public Animator animator;

    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    public AnimationCurve positionLerpCurve;
    public AnimationCurve rotationLerpCurve;
    public AnimationCurve rotationVCamLerpCurve;
    public float lerpDuration = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        animData = new PlayerAnimationData();
        animator = GetComponent<Animator>();

        // if (networkController.isLocal && isMe)
        // {
        //     networkController.OnTickClient += OnTickClient;
        // }
        // else
        // {
        //     networkController.OnSocketEventServer += OnTickServer;
        // }
    }

    // private void OnTickClient(float deltaTime)
    // {
    //     PlayerData data = new()
    //     {
    //         socketId = socketId,
    //         transformData = new()
    //         {
    //             position = transform.position,
    //             rotation = transform.rotation,
    //             camRot = vCam.Follow.transform.rotation,
    //         },
    //         animationData = animData
    //     };
    //     string payload = JsonUtility.ToJson(data);
    //     // networkManager.io.D.Emit("PlayerTransform", payload);
    // }
    // private void OnTickServer(string eventName, string payload)
    // {
    //     if (eventName.Equals("PlayerTransform"))
    //     {
    //         PlayerData data = JsonUtility.FromJson<PlayerData>(payload);
    //         if (data.socketId.Equals(socketId))
    //         {
    //             PlayerTransformData transformData = data.transformData;
    //             transform.position = transformData.position;
    //             transform.rotation = transformData.rotation;
    //             vCam.Follow.transform.rotation = transformData.camRot;

    //             animData = data.animationData;
    //         }
    //     }
    // }

    private void OnDestroy()
    {
        if (networkController.isLocal && isMe)
        {
            // networkManager.OnTickClient -= OnTickClient;
        }
        else
        {
            // networkManager.OnSocketEventServer -= OnTickServer;
        }
    }

    // Update is called once per frame
    override protected void Update()
    {

    }

    public void MoveTo(PlayerTransformData transformData)
    {
        StopAllCoroutines();
        StartCoroutine(LerpTransform(transformData));
    }

    IEnumerator LerpTransform(PlayerTransformData transformData)
    {
        float elapsedTime = 0f;

        Vector3 startPos = transform.position;
        Quaternion startRotation = transform.rotation;
        Quaternion startVCamRot = vCam.Follow.transform.rotation;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            float positionCurveValue = positionLerpCurve.Evaluate(t);
            transform.position = Vector3.Lerp(startPos, transformData.position, positionCurveValue);

            float rotationCurveValue = rotationLerpCurve.Evaluate(t);
            transform.rotation = Quaternion.Slerp(startRotation, transformData.rotation, rotationCurveValue);

            float camRotCurveValue = rotationVCamLerpCurve.Evaluate(t);
            vCam.Follow.transform.rotation = Quaternion.Slerp(startVCamRot, transformData.camRot, camRotCurveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = transformData.rotation;
        transform.position = transformData.position;
        vCam.Follow.transform.rotation = transformData.camRot;
    }

}
