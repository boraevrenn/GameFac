using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour
{
    [Header("Tilemaps and Tiles")]
    [SerializeField] Tilemap previewTilemap;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tile[] tiles;

    [Header("Positions")]
    Vector3 currentVector3Position;
    Vector3Int currentCellPosition;
    Vector3Int oldCellPosition;
    [SerializeField] bool isPreviewTrue;
    [SerializeField]GameManager gameManager;




    private void Update()
    {
        if (gameManager.enterEditMode)
        {
            TakeCoordinates();
            PreviewTile();
            PutTile();
        }

    }



    public void MakeisPreviewTrue()
    {
        if(isPreviewTrue)
        {
            isPreviewTrue = false;
        }
        else
        {
            isPreviewTrue = true;
        }
    }


    void TakeCoordinates()
    {
        currentVector3Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentCellPosition = previewTilemap.WorldToCell(currentVector3Position);

    }

    void PreviewTile()
    {
        if (isPreviewTrue)
        {
            previewTilemap.SetTile(currentCellPosition, tiles[0]);
            if (oldCellPosition != currentCellPosition)
            {
                previewTilemap.SetTile(oldCellPosition, null);
            }
            oldCellPosition = currentCellPosition;
        }
    }

    void PutTile()
    {
        if (isPreviewTrue && Input.GetMouseButton(0) && !EventSystem.current.currentSelectedGameObject)
        {
            groundTilemap.SetTile(currentCellPosition, tiles[0]);
        }
    }





}

