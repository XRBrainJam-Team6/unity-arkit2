using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionEyesOnScreen : MonoBehaviour {


    public GameObject leftEye;
    public GameObject hitMarkerPrefab;
    public GameObject RightEye;
    public GameObject averagePoint;
    public transformation tra;
    public bool markerExists = false;
    public GameObject marker;

    // public Transform target;
    public Camera cam;

    void Update()
    {
        averagePoint.transform.position = new Vector3((leftEye.transform.position.x + RightEye.transform.position.x) / 2, (leftEye.transform.position.y + RightEye.transform.position.y) / 2, (leftEye.transform.position.z + RightEye.transform.position.z) / 2);

        //Vector3 screenPosEyeLeft = cam.WorldToScreenPoint(leftEye.transform.position);
        //Vector3 screenPosRightRight = cam.WorldToScreenPoint(RightEye.transform.position);

        //Vector3 AveragePoint2D = cam.WorldToScreenPoint(averagePoint.transform.position);


        // Debug.Log("averageEyePosition is " + AveragePoint2D);
        //Vector3 fwd = averagePoint.transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        //Debug.DrawRay(AveragePoint2D, cam.transform.forward, Color.green);
        //Debug.DrawRay(transform.position, cam.transform.forward, Color.RED);
        Debug.DrawRay(averagePoint.transform.position, cam.transform.forward, Color.red);


        //var ray = new Ray(AveragePoint2D, cam.transform.forward);
        //Ray ray = Camera.main.ScreenPointToRay(averagePoint.transform.position);

        if (Physics.Raycast(averagePoint.transform.position, cam.transform.forward, out hit, 1000000))
        {
            Debug.Log("There is something in front of the object!");
            Debug.Log(hit.transform.gameObject.name);
            tra.lookingAtEyes=true;
            if (markerExists == false)
            {
                marker = GameObject.Instantiate(hitMarkerPrefab);
                marker.transform.position = hit.point;
                markerExists = true;
            }
            else {
                marker.SetActive(true);
                marker.transform.position = hit.point;
            }
        }
        else
        {
            Debug.Log("hit nothing");
            tra.lookingAtEyes = false;
            marker.SetActive(false);
        }
    }

    public float averageFloat(float x, float y)
    {
        return x + y / 2;
    }
}
