using UnityEngine;
using TMPro;
public class Cube : MonoBehaviour
{
    private static int _staticID = 0;
    [SerializeField] private TMP_Text[] _numbersText;

    [HideInInspector] public int cubeID;
    [HideInInspector] public Color cubeColor;
    [HideInInspector] public int cubeNumber;
    [HideInInspector] public Rigidbody cubeRigidBody;
    [HideInInspector] public bool isMainCube;

    private MeshRenderer _cubeMeshRender;

    private void Awake()
    {
        cubeID = _staticID++;
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
