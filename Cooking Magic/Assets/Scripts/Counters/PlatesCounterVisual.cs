using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform platesVisual;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private PlatesCounter platesCounter;
    private float interPlateOffsetY = 0.1f;
    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();   
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnSpawnTime;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnSpawnTime(object sender, EventArgs e)
    {
        Transform platesVisualTransform = Instantiate(platesVisual, counterTopPoint);
        platesVisualTransform.localPosition = new Vector3(0, interPlateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(platesVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject lastPlateObjectVisual = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        Destroy(lastPlateObjectVisual);
        plateVisualGameObjectList.Remove(lastPlateObjectVisual);
    }
}
