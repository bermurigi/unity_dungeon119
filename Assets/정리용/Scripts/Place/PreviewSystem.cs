using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewYOffset = 0.06f;
    [SerializeField] private GameObject cellIndicator;
    private GameObject previewObject;

    //[SerializeField] private Material previewMaterialsPrefab;
    //private Material previewMaterialInstance;

    private Renderer cellIndicatorRender;
    private void Start()
    {
        //previewMaterialInstance = new Material(previewMaterialsPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRender = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab);
        //PreparePreavie(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            cellIndicatorRender.material.mainTextureScale = size;
        }
    }

    // private void PreparePreavie(GameObject o)
    // {
    //     Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
    //     foreach (Renderer renderer in renderers)
    //     {
    //         Material[] materials = renderer.materials;
    //         for (int i = 0; i < materials.Length; i++)
    //         {
    //             materials[i] = previewMaterialInstance;
    //         }
    //
    //         renderer.materials = materials;
    //
    //     }
    // }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        if (previewObject != null)
            Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(position);
        }
        
        MoveCursor(position);
        ApplyFeedbackToCursor(validity);
    }

    private void ApplyFeedbackToCursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        cellIndicatorRender.material.color = c;
        c.a = 0.5f;
        //previewMaterialInstance.color = c;
    }
    

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(
            position.x, 
            position.y+previewYOffset, 
            position.z);
    }

    public void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        
        ApplyFeedbackToCursor(false);
    }
}
