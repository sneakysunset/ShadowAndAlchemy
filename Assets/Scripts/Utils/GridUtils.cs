using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GridUtils
{
    public static Slot WorldPosToTile(Vector3 pos)
    {
        PlantationGrid ts;
        if (PlantationGrid.Instance != null) ts = PlantationGrid.Instance;
        else ts = GameObject.FindObjectOfType<PlantationGrid>();
        float xOffset = 0;
        int x;
        int y;


        y = Mathf.RoundToInt(pos.y / ts.SlotPrefab.transform.localScale.y);
        x = Mathf.RoundToInt((pos.x - xOffset) / ts.SlotPrefab.transform.localScale.x );
        if (ts.Grid != null && ts.Grid.GetLength(0) > x && ts.Grid.GetLength(1) > y && 0 <= x && 0 <= y) return ts.Grid[x, y];
        else return null;
    }

    public static Vector3 indexToWorldPos(int x, int y, Transform tT)
    {
        float xOffset = 0;
        Vector3 pos = new Vector3(x * tT.localScale.x  + xOffset,  y * tT.localScale.y, 0);

        return pos;
    }

    public static bool TContain(Slot[,] Grid, Slot slot)
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for(int j = 0; j < Grid.GetLength(1); j++)
            {
                if (Grid[i, j] == slot) return true;
            }
        }
        return false;
    }


/*    public static List<Slot> GetTilesAround(int numOfRows, Slot tile)
    {
        PlantationGrid tis = PlantationGrid.Instance;
        Dictionary<Slot, bool> tileDict = new Dictionary<Slot, bool>();
        int rowsSeen = 0;
        tileDict.Add(tile, false);
        while (rowsSeen < numOfRows)
        {
            int ix = tileDict.Count;
            foreach(var pair in  tileDict)
            {
                if (!pair.Value)
                {
                    foreach (Vector2Int vecs in pair.Key.AdjacentTilesCoordinates)
                    {
                        if (vecs.x >= 0 && vecs.x < tis.TileRows && vecs.y >= 0 && vecs.y < tis.TileColumns && tis.Tiles[vecs.x, vecs.y].Walkable && !tileDict.Any(m => m.Key == tis.Tiles[vecs.x, vecs.y]))
                        {
                            tileDict.Add(tis.Tiles[vecs.x, vecs.y], false);
                        }
                    }
                    tileDict[pair.Key] = true;
                }
            }
            rowsSeen++;
        }

        List<Slot> result = new List<Slot>();
        foreach (Slot t in tileDict.Keys)
        {
            result.Add(t);
        }
        return  result;
    }*/


/*    public static List<Tile> GetTilesBetweenRaws(int rowMin, int rowMax, Tile tile)
    {
        TileSystem tis = TileSystem.Instance;
        List<Tile> ts = new List<Tile>();
        List<Tile> ts2 = new List<Tile>();
        int rowsSeen = 0;
        ts.Add(tile);
        while (rowsSeen <= rowMax)
        {
            int ix = ts.Count;
            for (int i = 0; i < ix; i++)
            {
                if (!ts[i].isPathChecked)
                {
                    foreach (Vector2Int vecs in ts[i].adjTCoords)
                    {
                        if (vecs.x >= 0 && vecs.x < tis.rows && vecs.y >= 0 && vecs.y < tis.columns && !ts.Contains(tis.tiles[vecs.x, vecs.y]))
                        {
                            ts.Add(tis.tiles[vecs.x, vecs.y]);
                            if (rowsSeen >= rowMin && rowsSeen <= rowMax)
                            {
                                ts2.Add(tis.tiles[vecs.x, vecs.y]);
                            }
                        }
                    }
                    ts[i].isPathChecked = true;
                }
            }
            rowsSeen++;
        }

        foreach (Tile t in ts)
        {
            t.isPathChecked = false;
        }
        return ts2;
    }*/

}
