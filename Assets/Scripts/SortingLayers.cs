using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayers : MonoBehaviour
{
    public string sortingLayerName = "Default";
    public int sortingOrder = 0;

    // Use this for initialization
    void Start()
    {
        ParticleSystemRenderer particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        SetupSortingLayer(particleSystemRenderer);
    }

    private void SetupSortingLayer(ParticleSystemRenderer particleSystemRenderer)
    {
        particleSystemRenderer.sortingLayerName = sortingLayerName;
        particleSystemRenderer.sortingOrder = sortingOrder;
    }
}
