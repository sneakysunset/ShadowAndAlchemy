using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEditor;
using UnityEngine;

[DeclareTabGroup("Tile Generation"), DeclareHorizontalGroup("Tile Generation/Instantiation")]
public class PlantationGrid : Singleton<PlantationGrid>
{
    public Slot[,] Grid { get => _Grid; }
    public Slot SlotPrefab { get => _SlotPrefab; }
    public int Rows { get => _Rows; }
    public int Columns { get => _Columns; }

    [SerializeField] private Slot[,] _Grid;
    [SerializeField, Group("Tile Generation/Instantiation"), Tab("Tile Generation")] private int _Rows;
    [SerializeField, Group("Tile Generation/Instantiation"), Tab("Tile Generation")] private int _Columns;
    [SerializeField, Group("Tile Generation/Instantiation"), Tab("Tile Generation")] private Slot _SlotPrefab;
    [SerializeField, Group("Tile Generation"), Tab("Tile Generation")] private Transform _SlotFolder;
    [SerializeField, Group("Tile Generation"), Tab("Tile Generation")] private bool _AllowGridEdition;

#if UNITY_EDITOR
    [ShowIf("_AllowGridEdition"), Button, Group("Tile Generation"), Tab("Tile Generation")]
    public void InstantiateGrid()
    {
        DestroyGrid();
        _Grid = new Slot[Rows, Columns];
        for (int i = 0; i < _Grid.GetLength(0); i++)
        {
            for (int j = 0; j < _Grid.GetLength(1); j++)
            {
                _Grid[i, j] = PrefabUtility.InstantiatePrefab(_SlotPrefab) as Slot;
                _Grid[i, j].transform.parent = _SlotFolder;
                _Grid[i, j].transform.position = GridUtils.indexToWorldPos(i, j, _SlotPrefab.transform);
                _Grid[i, j]._Coordinates = new Vector2Int(i, j);
                _Grid[i, j].gameObject.name = i + "  " + j;
            }
        }
    }
#endif

    [ShowIf("_AllowGridEdition"), Button, Group("Tile Generation"), Tab("Tile Generation")]
    public void DestroyGrid()
    {
        Slot[] previousSlots = FindObjectsOfType<Slot>();
        if (previousSlots != null && previousSlots.Length > 0)
        {
#if UNITY_EDITOR
            foreach (Slot slot in previousSlots) DestroyImmediate(slot.gameObject);
#else
            foreach (Slot tile in previousTiles) Destroy(tile.gameObject);
#endif
        }
    }


    [Button, Group("Tile Generation"), Tab("Tile Generation")]
    private void GetTilesReferences()
    {
        Slot[] currentTiles = FindObjectsOfType<Slot>();
        int maxRow = currentTiles.Max(m => m._Coordinates.x) + 1;
        int maxColumn = currentTiles.Max(m => m._Coordinates.y) + 1;
        _Grid = new Slot[maxRow, maxColumn];
        for (int i = 0; i < maxRow; i++)
        {
            for (int j = 0; j < maxColumn; j++)
            {
                _Grid[i, j] = currentTiles.Where(m => m._Coordinates.x == i && m._Coordinates.y == j).FirstOrDefault();
            }
        }

    }
}


