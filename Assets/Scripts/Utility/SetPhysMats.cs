using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPhysMats : MonoBehaviour {

    [SerializeField]
    private PhysicMaterial PhysMat;

    private void Start()
    {
        foreach (var collider in FindObjectsOfType<Collider>())
            collider.material = PhysMat;
    }
}
