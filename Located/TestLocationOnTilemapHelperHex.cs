using UnityEngine;

public class TestLocationOnTilemapHelperHex : LocationOnTilemapHelper
{
    public override Vector3Int Location { get => new Vector3Int(); }
    public override GridLayout.CellLayout CellLayout { get => GridLayout.CellLayout.Hexagon; }
}