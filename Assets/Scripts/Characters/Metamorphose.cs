using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metamorphose : CharacterAction
{

    [SerializeField] private GameObject enragedVersionPrefab;

    protected override void EndAction()
    {
        base.EndAction();
        GameObject newObject = Instantiate(enragedVersionPrefab, transform.position, Quaternion.identity);
        TransferProperties(newObject);
        Destroy(gameObject);
    }

    private void TransferProperties(GameObject newObject)
    {
        iMetamorphosisTransfer[] componentsToTransfer = GetComponents<iMetamorphosisTransfer>();
        foreach(iMetamorphosisTransfer component in componentsToTransfer)
        {
            component.Transfer(newObject);
        }
    }
}
