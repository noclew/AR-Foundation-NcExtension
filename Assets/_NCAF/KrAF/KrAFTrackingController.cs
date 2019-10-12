using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Video;

public class KrAFTrackingController : MonoBehaviour
{
    private ARTrackedImageManager m_ARTIManager;
    public GameObject VideoPlayer;
    private VideoPlayer m_vp;
    private string rootDir = "VideoClips/";

    // Start is called before the first frame update
    private void Awake()
    {
        m_ARTIManager = GetComponent<ARTrackedImageManager>();
        m_vp = VideoPlayer.GetComponent<VideoPlayer>();
    }
    void Start()
    {
        //print(m_ARTIManager);
        //Debug.Log("??????????????????????????????????");
    }

    private void OnEnable()
    {
        m_ARTIManager.trackedImagesChanged += KuroImageHandler;
        //PlayVideoFromResources(rootDir + "84-003-041");
    }
    private void OnDisable()
    {
        m_ARTIManager.trackedImagesChanged -= KuroImageHandler;
    }

    // Update is called once per frame
    void Update()
    {
        //TrackableCollection<ARTrackedImage> imageList = m_ARTIManager.trackables;
        //foreach (ARTrackedImage arImage in imageList)
        //{
        //    if (arImage.trackingState == TrackingState.Tracking)
        //    {

        //    }
        //    else if (arImage.trackingState == TrackingState.Limited)
        //    {

        //    }
        //    else if (arImage.trackingState == TrackingState.None)
        //    {

        //    }
        //    //do shit for the all detected image in the current frame
        //}
    }

    void KuroImageHandler(ARTrackedImagesChangedEventArgs arg)
    {
        foreach (ARTrackedImage image in arg.added)
        {
            //do shit for added image
        }

        foreach (ARTrackedImage arImage in arg.updated)
        {
            if (arImage.trackingState == TrackingState.Tracking)
            {
                string videoname = rootDir + arImage.referenceImage.name;
                //Debug.Log("TrackingState.tracking: " + arImage.referenceImage.name);
                if (Resources.Load<VideoClip>(videoname) != null)
                {
                    PlayVideoFromResources(videoname);
                }
            }
            else if (arImage.trackingState == TrackingState.Limited)
            {
                //
            }

        }

        foreach (ARTrackedImage image in arg.removed)
        {
            //do shit for removed image
        }
    }
    void PlayVideoFromResources(string videoname)
    {
        string currentVideoname = null;
        if (m_vp.clip != null)
        {
            currentVideoname = rootDir + m_vp.clip.name;
        }
        else
        {
            currentVideoname = null;
        }

        if (currentVideoname != videoname)
        {
            ////before set a clip to play.
            m_vp.Stop();
            VideoPlayer.SetActive(false);

            ////set a clip
            VideoClip clip = Resources.Load<VideoClip>(videoname) as VideoClip;
            Debug.Log(clip);
            //VideoPlayer vp = VideoPlayer.GetComponent<StreamVideo>().videoPlayer;
            m_vp.clip = clip;

            ////to get VideoPlayer Object enable
            VideoPlayer.SetActive(true);
        }
        else
        {
            //Debug.Log("it's the same!");
        }

        m_vp.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        m_vp.Stop();
        VideoPlayer.SetActive(false);
    }
}
