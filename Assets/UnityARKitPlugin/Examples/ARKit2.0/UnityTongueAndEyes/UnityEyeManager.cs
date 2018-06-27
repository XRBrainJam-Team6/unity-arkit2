using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class UnityEyeManager : MonoBehaviour 
{
	[SerializeField]
	private GameObject eyePrefab;
    public PositionEyesOnScreen peos;

    private UnityARSessionNativeInterface m_session;
	public GameObject leftEyeGo;
	public GameObject rightEyeGo;
	public GameObject sphere;
	public GameObject initialSphere;
	private Vector3 startupLocation;

	// Use this for initialization
	void Start () {
		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

		Application.targetFrameRate = 60;
		ARKitFaceTrackingConfiguration config = new ARKitFaceTrackingConfiguration();
		config.alignment = UnityARAlignment.UnityARAlignmentGravity;
		config.enableLightEstimation = true;

		if (config.IsSupported )
		{

			m_session.RunWithConfig (config);

			UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
			UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
			UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;

		}

		leftEyeGo = GameObject.Instantiate (eyePrefab);
        Debug.Log(peos.leftEye.name);
        Debug.Log(peos.leftEye.transform.position);
        Debug.Log(leftEyeGo.transform.position);


        peos.leftEye.transform.SetParent(leftEyeGo.transform);
        rightEyeGo = GameObject.Instantiate (eyePrefab);
        peos.RightEye.transform.SetParent(rightEyeGo.transform);

        leftEyeGo.SetActive (false);
		rightEyeGo.SetActive (false);

	}

	void FaceAdded (ARFaceAnchor anchorData)
	{
		startupLocation = anchorData.lookAtPoint;
		Debug.Log("startup: " + startupLocation);
		// initialSphere.transform.localPosition = new Vector3(
		// 	startupLocation.x,
		// 	startupLocation.y,
		// 	0f
		// );
		//sphere.transform.position = anchorData.lookAtPoint;
		leftEyeGo.transform.position = anchorData.leftEyePose.position;
		leftEyeGo.transform.rotation = anchorData.leftEyePose.rotation;

		rightEyeGo.transform.position = anchorData.rightEyePose.position;
		rightEyeGo.transform.rotation = anchorData.rightEyePose.rotation;

		leftEyeGo.SetActive (true);
		rightEyeGo.SetActive (true);
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		Vector3 deltaLook = new Vector3(
			anchorData.lookAtPoint.x - startupLocation.x,
			anchorData.lookAtPoint.y - startupLocation.y,
			0f
		);

		sphere.transform.localPosition = new Vector3(
			deltaLook.x * 10f,
			deltaLook.y * 30f + 10f,
			0f
		);

		Debug.Log("localPosition: " + sphere.transform.localPosition);
		leftEyeGo.transform.position = anchorData.leftEyePose.position;
		leftEyeGo.transform.rotation = anchorData.leftEyePose.rotation;

		rightEyeGo.transform.position = anchorData.rightEyePose.position;
		rightEyeGo.transform.rotation = anchorData.rightEyePose.rotation;

	}

	void FaceRemoved (ARFaceAnchor anchorData)
	{
		leftEyeGo.SetActive (false);
		rightEyeGo.SetActive (false);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
