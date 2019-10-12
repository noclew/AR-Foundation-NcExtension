using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    //public AudioSource audioSource;
    private bool enableAutoPlay;

    // Use this for initialization
    void OnEnable()
    {
        enableAutoPlay = false;
        StartCoroutine(PlayVideo());
    }
    IEnumerator PlayVideo()
    {
        rawImage.color = new Color(1f, 1f, 1f, 0f);// to turn transparent
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(2f);
        while (!videoPlayer.isPrepared)
        {
            //Debug.Log("waiting for preparing");
            yield return waitForSeconds;
            break;
        }
        rawImage.color = new Color(1f, 1f, 1f, 1f);// to turn white
        rawImage.texture = videoPlayer.texture;

        if (enableAutoPlay)
        {
            //Play video
            videoPlayer.Play();
            //audioSource.Play();
        }
        else
        {
            //Just show the 1st frame.
            videoPlayer.Play();
            while (videoPlayer.frame < 1)
            {
                Debug.Log(videoPlayer.frame);
                yield return null;
            }
            videoPlayer.Pause();
        }
    }

    public void playVideoManually()
    {
        if (!enableAutoPlay)
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
            else
            {
                videoPlayer.Pause();
            }
        }
    }
}
