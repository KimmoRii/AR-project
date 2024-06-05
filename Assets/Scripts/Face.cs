using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Face : MonoBehaviour
{
    [SerializeField] private ARCameraManager arCamera;
    [SerializeField] private ARFaceManager arFace;

    private void OnEnable() => arFace.facesChanged += OnFaceChanged;
    private void OnDisable() => arFace.facesChanged -= OnFaceChanged;
    private List<ARFace> faces = new List<ARFace>();

    [SerializeField] private TMP_Text debugText;

    private void OnFaceChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach (var newFace in eventArgs.added)
        {
            faces.Add(newFace);
        }

        foreach (var lostFace in eventArgs.removed)
        {
            faces.Remove(lostFace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arCamera.currentFacingDirection == CameraFacingDirection.User)
        {
            if(faces.Count > 0)
            {
                Vector3 lowerlippos = faces[0].vertices[14];
                debugText.text = lowerlippos.ToString("F3");
            }
        }
    }
}
