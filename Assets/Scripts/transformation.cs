using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transformation : MonoBehaviour
{

    public GameObject successmarker;
    public Renderer objectRenderer;
    public Material mat1;
    public Material mat2;
    public GameObject[] piecesOfMask;
    public float numberOfSecondsLookingAtEyes;
    public float numberOfSecondContinous = 0;
    public bool lookingAtEyes;
    public Text counterTextUI;

    // Use this for initialization
    void Start()
    {
        objectRenderer = successmarker.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingAtEyes)
        {
            
            numberOfSecondContinous = numberOfSecondContinous + Time.deltaTime;
            counterTextUI.text = numberOfSecondContinous + "";
        }
    }

    public void changeRedMaterial()
    {
        objectRenderer.material = mat2;
    }

    public void changeWhiteMaterial()
    {
        objectRenderer.material = mat1;
    }


}
