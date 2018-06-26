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
    public List<GameObject> piecesOfMask;
    public float numberOfSecondsLookingAtEyes;
    public float numberOfSecondContinous = 0;
    public bool lookingAtEyes;
    public Text counterTextUI;
    public GameObject head;
    public float indexOfLastPiece;
    public ParticleSystem particleSystemWhenPieceIsGone;

    // Use this for initialization
    void Start()
    {
        objectRenderer = successmarker.GetComponent<Renderer>();
        receiveHeadMesh(head);
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

    public void receiveHeadMesh(GameObject head )
    {
        foreach (Transform child in head.transform)
        {
            piecesOfMask.Add(child.gameObject);
          //  spawnPoints.Add(child.position);
        }
    }

    public void dissapearingPiece()
    {

    }

}
