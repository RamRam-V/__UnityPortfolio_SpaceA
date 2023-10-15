using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    [SerializeField] private PlayerFootstepSoundSO playerFootstepSoundSO;

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (playerFootstepSoundSO.FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, playerFootstepSoundSO.FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(playerFootstepSoundSO.FootstepAudioClips[index], transform.position, playerFootstepSoundSO.FootstepAudioVolume);
            }
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(playerFootstepSoundSO.LandingAudioClip, transform.position, playerFootstepSoundSO.FootstepAudioVolume);
        }
    }
}
