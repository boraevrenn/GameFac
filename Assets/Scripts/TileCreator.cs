using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int buttonNumber = 8;
    [SerializeField] bool isEnvironmentTile;
    [SerializeField] bool isButtonClicked;
    [SerializeField] bool isRemoveTile;


    [Header("Tile")]
    [SerializeField] Tilemap previewTileMap;
    [SerializeField] Tilemap groundTileMap;
    [SerializeField] Tilemap environmentTileMap;
    [SerializeField] Tile[] groundTiles;
    [SerializeField] Tile[] environmentTiles;
    [SerializeField] Tile currentTile;




    [Header("Coordinates")]
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Vector3Int tilePosition;
    [SerializeField] Vector3Int previousTile;

    private void Awake()
    {
        previewTileMap = GameObject.FindGameObjectWithTag("Preview").ConvertTo<Tilemap>();
        groundTileMap = GameObject.FindGameObjectWithTag("Ground").ConvertTo<Tilemap>();
        environmentTileMap = GameObject.FindGameObjectWithTag("Environment").ConvertTo<Tilemap>();
    }



    private void Update()
    {
        TakeCoordinates();
        PreviewTile();
        PutTile();
        CancelPuttingTile();
        RemoveTile();
    }


    public void AssignGroundTilesToButtons()
    {
        string tileIndexString = EventSystem.current.currentSelectedGameObject.name[buttonNumber].ToString();
        int tileIndexInt = int.Parse(tileIndexString);
        currentTile = groundTiles[tileIndexInt];
        isEnvironmentTile = false;
        isButtonClicked = true;
        isRemoveTile = false;
    }

    public void AssignEnvironmentTilesToButtons()
    {
        string tileIndexString = EventSystem.current.currentSelectedGameObject.name[buttonNumber].ToString();
        int tileIndexInt = int.Parse(tileIndexString);
        currentTile = environmentTiles[tileIndexInt];
        isEnvironmentTile = true;
        isButtonClicked = true;
        isRemoveTile = false;

    }

    void TakeCoordinates()
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tilePosition = previewTileMap.WorldToCell(currentPosition);
    }

    void PreviewTile()
    {
        if (isButtonClicked)
        {
            previewTileMap.SetTile(tilePosition, currentTile);
            if (previousTile != tilePosition)
            {
                previewTileMap.SetTile(previousTile, null);
            }
            previousTile = tilePosition;
        }
    }

    void PutTile()
    {
        if (isButtonClicked)
        {
            if (isEnvironmentTile && Input.GetMouseButton(0) && !EventSystem.current.currentSelectedGameObject)
            {
                environmentTileMap.SetTile(tilePosition, currentTile);
            }
            else if (!isEnvironmentTile && Input.GetMouseButton(0) && !EventSystem.current.currentSelectedGameObject)
            {
                groundTileMap.SetTile(tilePosition, currentTile);
            }
        }
    }

    void CancelPuttingTile()
    {
        if (Input.GetMouseButton(1))
        {
            isButtonClicked = false;
            previewTileMap.SetTile(tilePosition, null);
        }
    }

    public void RemoveTileEnabler()
    {
        isRemoveTile = !isRemoveTile;
    }


    public void RemoveTile()
    {
        if (isRemoveTile)
        {
            isButtonClicked = false;
            if (Input.GetMouseButton(0))
            {
                groundTileMap.SetTile(tilePosition, null);
                environmentTileMap.SetTile(tilePosition, null);
            }
        }
        if (Input.GetMouseButton(1))
        {
            isRemoveTile = false;
        }
    }

}

