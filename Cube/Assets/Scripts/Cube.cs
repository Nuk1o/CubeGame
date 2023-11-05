using System;
using UnityEngine;
using TMPro;
public class Cube : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _numbersText;

    [HideInInspector] public Color cubeColor;
    [HideInInspector] public int cubeNumber;
    [HideInInspector] public Rigidbody cubeRigidBody;
    [HideInInspector] public bool isMainCube;

    private MeshRenderer _cubeMeshRender;

    private void Awake()
    {
        _cubeMeshRender = GetComponent<MeshRenderer>();
        cubeRigidBody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        cubeColor = color;
        _cubeMeshRender.material.color = color;
    }

    public void SetNumber(int number)
    {
        cubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            _numbersText[i].text = number.ToString();
        }
    }
}
