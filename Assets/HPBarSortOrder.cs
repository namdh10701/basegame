using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarSortOrder : MonoBehaviour
{
    [SerializeField] SkeletonAnimation sa;
    [SerializeField] Canvas canvas;
    MeshRenderer mat;
    private void Start()
    {
        mat = sa.GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        canvas.sortingOrder = mat.sortingOrder + 1;
    }
}
