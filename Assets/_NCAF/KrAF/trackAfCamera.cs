using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.IO;
using System.Text;

public class trackAfCamera : MonoBehaviour
{
    public GameObject ARCamera;
    public ARTrackedImageManager m_ARTIManager;
    private string worldOriginImage;
    private string logpath;
    private FileStream fs;
    public GameObject ARImageTargetInfo;

    // Start is called before the first frame update
    private void Awake()
    {

        //m_ARTIManager = GetComponent<ARTrackedImageManager>();
    }

    private void Start()
    {
        if (m_ARTIManager == null)
        {
            Debug.LogError(">>> Tracked Image Manager is not set in tracked AF camera component");
        }

        string datetimestring = DateTime.Now.ToString("yyyyMMdd_hhmmss");
        logpath = Path.Combine(Application.persistentDataPath, "ARCameraPath_" + datetimestring + ".txt");
        fs = File.Open(logpath, FileMode.Append, FileAccess.Write);

        //write a header line
        foreach (Transform TargetImage in ARImageTargetInfo.transform)
        {
            Debug.Log(TargetImage.name.ToString());
            List<string> items = new List<string> {
                TargetImage.name.ToString(),
                TargetImage.position.x.ToString(),
                TargetImage.position.y.ToString(),
                TargetImage.position.z.ToString(),
                TargetImage.rotation.w.ToString(),
                TargetImage.rotation.x.ToString(),
                TargetImage.rotation.y.ToString(),
                TargetImage.rotation.z.ToString(),
                TargetImage.forward.x.ToString(),
                TargetImage.forward.y.ToString(),
                TargetImage.forward.z.ToString()
            };
            string line = string.Join(", ", items);
            Byte[] info = new UTF8Encoding(true).GetBytes(line + "\n");
            fs.Write(info, 0, info.Length);
        }

        Byte[] kaigyo = new UTF8Encoding(true).GetBytes("\n");
        fs.Write(kaigyo, 0, kaigyo.Length);

        List<string> header_items = new List<string> {
            "timestamp",
            "worldOriginImage",
            "position.x",
            "position.y",
            "position.z",
            "forward.x",
            "forward.y",
            "forward.z"
        };
        string header_line = string.Join(", ", header_items);
        Byte[] header_info = new UTF8Encoding(true).GetBytes(header_line + "\n");
        fs.Write(header_info, 0, header_info.Length);
    }

    //private void OnEnable()
    //{
    //    m_ARTIManager.trackedImagesChanged += KuroImageHandler;
    //}
    //private void OnDisable()
    //{
    //    m_ARTIManager.trackedImagesChanged -= KuroImageHandler;
    //}

    // Update is called once per frame
    void Update()
    {
        TrackableCollection<ARTrackedImage> imageList = m_ARTIManager.trackables;
        foreach (ARTrackedImage arImage in imageList)
        {
            //if (arImage.trackingState == TrackingState.Tracking)
            //{

            //}
            //else if (arImage.trackingState == TrackingState.Limited)
            //{

            //}
            //else if (arImage.trackingState == TrackingState.None)
            //{

            //}
            //do shit for the all detected image in the current frame
            worldOriginImage = arImage.referenceImage.name.ToString();
        }

        List<string> items = new List<string> {
            DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            worldOriginImage,
            ARCamera.transform.position.x.ToString(),
            ARCamera.transform.position.y.ToString(),
            ARCamera.transform.position.z.ToString(),
            ARCamera.transform.forward.normalized.x.ToString(),
            ARCamera.transform.forward.normalized.y.ToString(),
            ARCamera.transform.forward.normalized.z.ToString()
        };
        string line = string.Join(", ", items);
        Byte[] info = new UTF8Encoding(true).GetBytes(line + "\n");
        fs.Write(info, 0, info.Length);

        //Debug.Log(worldOriginImage);
        //Debug.Log("position: " + ARCamera.transform.localPosition.ToString());
        //Debug.Log("forward : " + ARCamera.transform.forward.normalized.ToString());
    }
    void OnApplicationQuit()
    {
        fs.Close();
    }

    //void KuroImageHandler(ARTrackedImagesChangedEventArgs arg)
    //{
    //    foreach (ARTrackedImage image in arg.added)
    //    {
    //        //do shit for added image
    //    }

    //    foreach (ARTrackedImage arImage in arg.updated)
    //    {
    //        if (arImage.trackingState == TrackingState.Tracking)
    //        {
    //            //Debug.Log("TrackingState.tracking: " + arImage.referenceImage.name);
    //        }
    //        else if (arImage.trackingState == TrackingState.Limited)
    //        {
    //            //
    //        }
    //    }

    //    foreach (ARTrackedImage image in arg.removed)
    //    {
    //        //do shit for removed image
    //    }
    //}
}
