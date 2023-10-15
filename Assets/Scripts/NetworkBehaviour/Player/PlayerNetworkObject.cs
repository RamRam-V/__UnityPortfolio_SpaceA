using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using PlayerProtocol;
using Unity.Mathematics;

public class PlayerNetworkObject : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private NetworkController networkController;
    [SerializeField] private PlayerNetworkObjectController networkObjectController;

    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;

    //서버 동기화용 데이터
    public PlayerData playerData
    {
        get;
        private set;
    }

    [SerializeField] private AnimationCurve positionLerpCurve;
    [SerializeField] private AnimationCurve rotationLerpCurve;
    [SerializeField] private AnimationCurve rotationVCamLerpCurve;
    private float lerpDuration = 0.5f;
    private void Awake()
    {
        spawnController.OnSpawnedLocalPlayer += (UserCharacterInfo info) =>
        {
            if (networkController.isLocal)
            {
                networkObjectController.SetPlayerNetworkObject(info, playerTransform, vCam);
            }
        };
    }
    // Start is called before the first frame update
    void Start()
    {
        networkObjectController.AddPlayerObject(gameObject.name, this);

        networkObjectController.OnPlayerTransformUpdate += (PlayerData data) =>
            {
                if (data.info.nickname.Equals(gameObject.name))
                {
                    playerData = data;

                    //transform
                    if (networkController.isLocal)
                    {
                        MoveTo(data.transformData);
                    }
                    else
                    {
                        PlayerTransformData transformData = data.transformData;
                        playerTransform.position = transformData.position;
                        playerTransform.rotation = transformData.rotation;
                        vCam.Follow.transform.rotation = transformData.camRot;
                    }

                    //animation
                    if (data.animationData.isMoving)
                    {
                        if (!data.animationData.isSprint)
                        {
                            animator.SetFloat("Speed", 2);
                            animator.SetFloat("MotionSpeed", 1);
                        }
                        else
                        {

                            animator.SetFloat("Speed", 6);
                            animator.SetFloat("MotionSpeed", 1);
                        }
                    }
                    //뛰기
                    else if (data.animationData.isMoving && data.animationData.isSprint)
                    {
                    }
                    else if (!data.animationData.isMoving)
                    {
                        animator.SetFloat("Speed", 0);
                    }
                    //점프
                    animator.SetBool("Jump", data.animationData.isJump);
                    animator.SetBool("Grounded", data.animationData.isGrounded);
                }
            };
    }

    private void OnDestroy()
    {
        networkObjectController.RemovePlayerObject(gameObject.name);
    }


    public void MoveTo(PlayerTransformData transformData)
    {
        StopAllCoroutines();
        StartCoroutine(LerpTransform(transformData));
    }

    IEnumerator LerpTransform(PlayerTransformData transformData)
    {
        float elapsedTime = 0f;

        Vector3 startPos = playerTransform.position;
        Quaternion startRotation = playerTransform.rotation;
        Quaternion startVCamRot = vCam.Follow.transform.rotation;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            float positionCurveValue = positionLerpCurve.Evaluate(t);
            playerTransform.position = Vector3.Lerp(startPos, transformData.position, positionCurveValue);

            float rotationCurveValue = rotationLerpCurve.Evaluate(t);
            playerTransform.rotation = Quaternion.Slerp(startRotation, transformData.rotation, rotationCurveValue);

            float camRotCurveValue = rotationVCamLerpCurve.Evaluate(t);
            vCam.Follow.transform.rotation = Quaternion.Slerp(startVCamRot, transformData.camRot, camRotCurveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.rotation = transformData.rotation;
        playerTransform.position = transformData.position;
        vCam.Follow.transform.rotation = transformData.camRot;
    }

}
