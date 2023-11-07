using UnityEngine;
using Random = UnityEngine.Random;

public class CubeCollision : MonoBehaviour
{
    private Cube _cube;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube _otherCube = collision.gameObject.GetComponent<Cube>();

        if (_otherCube != null && _cube.cubeID > _otherCube.cubeID)
        {
            if (_cube.cubeNumber == _otherCube.cubeNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;
                if (_otherCube.cubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    Cube _newCube = CubeSpawner.Instance.Spawn(_cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                    float _pushForce = 2.5f;
                    _newCube.cubeRigidBody.AddForce(new Vector3(0,0.3f,1f)*_pushForce, ForceMode.Impulse);

                    float _randomValue = Random.Range(-20f, 20f);
                    Vector3 _randomDirection = Vector3.one * _randomValue;
                    _newCube.cubeRigidBody.AddTorque(_randomDirection);
                }

                Collider[] _surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float _explosionForce = 400f;
                float _explosionRadius = 1.5f;

                foreach (Collider coll in _surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(_explosionForce,contactPoint,_explosionRadius);
                    }
                }
                
                FX.Instance.PlayCubeExplosionFX(contactPoint,_cube.cubeColor);
                
                CubeSpawner.Instance.DestroyCube(_cube);
                CubeSpawner.Instance.DestroyCube(_otherCube);
            }
        }
    }
}
