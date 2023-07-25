using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Renderer[] middleRenderers;
    [SerializeField] string sortingLayerName;
    int originOrder;
    public bool isFull { get; set; }

    public void setOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetMostFrontOrder(bool isMostFront)
    {
        SetOrder(isMostFront ? 100 : originOrder);
    }

    public void SetOrder(int order)
    {
        int mulOrder = order * 10;
        foreach (var renderer in backRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder + 1; 
        }
    }

    internal void SetMostFrontOrder()
    {
        throw new NotImplementedException();
    }
}
