using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingState
{
    void EndState();
    int Check(Vector3Int gridPosition);
    void OnAction(Vector3Int gridPosition, Vector3Int beforeGridPosition);
    void UpdateState(Vector3Int gridPosition);

    void BuyUnit();
}