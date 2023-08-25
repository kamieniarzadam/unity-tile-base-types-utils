using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SectorsManager : MonoBehaviour
{
    public GameObject sectorPrefab;

    protected void SetupSector(int relation, float rotation)
    {
        var sector = Instantiate(sectorPrefab, transform.position, Quaternion.Euler(0, rotation, 0), transform);
        sector.transform.SetSiblingIndex(relation);
        sector.hideFlags = HideFlags.DontSave;
    }

    public void Start()
    {
        var sidesNumber = GetComponentInParent<LocationOnTilemapHelper>().NumberOfCellSides;
        for (int i = 0; i < sidesNumber; i++)
        {
            SetupSector(i, 360f / sidesNumber * i);
        }
    }
}