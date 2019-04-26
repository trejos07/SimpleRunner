using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AcelerationController : MonoBehaviour
{
    public static AcelerationController Instance;
    [Range(0.5f,2.5f)] [SerializeField]float sensiviliti;
    Vector3 refInicial;
    Vector3 aceleration;

    public Vector3 Aceleration { get => aceleration; set => aceleration = value; }

    private void Awake()
    {
        if (Instance== null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        refInicial = new Vector3(0, Input.acceleration.y);
    }
    void Update()
    {
        aceleration = Input.acceleration* sensiviliti;
    }
}
