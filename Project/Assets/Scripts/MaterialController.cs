using System.Linq;
using System;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] private SurfaceType _surfaceType;
    [SerializeField] private RendererData[] rendererDatas;


    public SurfaceType SurfaceType
    {
        get => _surfaceType;
        set 
        {
            if (value == _surfaceType) return;
            _surfaceType = value;
            //SyncMaterialsToType();
        }
    }


    private void OnValidate()
    {
        SetMaterialType(_surfaceType);
    }


    public void SetMaterialType(SurfaceType type)
    {
        _surfaceType = type;

        foreach (RendererData item in rendererDatas)
        {
            item.ChangeMaterialsToSurfaceType(_surfaceType);
        }
    }
}

public enum SurfaceType
{
    Opaque,
    Transparent,
    Invisible
}


[Serializable]
public class RendererData
{
    public Renderer Renderer;
    public MaterialPerSurfaceType[] materialsPerSurfaceType;


    public void ChangeMaterialsToSurfaceType(SurfaceType category)
    {
        if (category == SurfaceType.Invisible)
        {
            Renderer.enabled = false;
            return;
        }

        Renderer.enabled = true;
        Material[] materialsOfRenderer = Renderer.sharedMaterials;

        for (int i = 0, materialCount = materialsOfRenderer.Length; i < materialCount; i++)
        {
            try
            {
                materialsOfRenderer[i] = materialsPerSurfaceType[i].GetMaterial(category);
            }
            catch
            {

            }
        }

        Renderer.SetSharedMaterials(materialsOfRenderer.ToList());
    }


    [Serializable]
    public struct MaterialPerSurfaceType
    {
        public Material OpaqueMaterial;
        public Material TransparentMaterial;

        public readonly Material GetMaterial(SurfaceType type)
        {
            return type == SurfaceType.Opaque ? OpaqueMaterial : type == SurfaceType.Transparent ? TransparentMaterial : null;
        }
    }
}
