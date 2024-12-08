using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using FotF.Api.Prisms;

public class PrismController : MonoBehaviour
{
    public IPrism Model;


    public void Awake()
    {
        Model = new PrismAgent(new PrismConfig());
    }

    public void Start()
    {

    }

    public void Update()
    {

    }
}