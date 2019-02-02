﻿using Xenko.Graphics;
using Xenko.Rendering;
using XenkoTerrain.Graphics;

namespace XenkoTerrain.TerrainSystem
{
  public class TerrainHeightSourceRenderObject : RenderObject
  {
    public Texture HeightMap;
    public RgbPixelRepository HeightData;

    public void Prepare(RenderDrawContext context)
    {
      if (HeightDataNeedsRebuilt() && TryGetHeightMapImageData(context.CommandList, out var heightData))
      {
        HeightData = heightData;
      }
    }

    private bool HeightDataNeedsRebuilt()
    {
      return HeightMap != null && HeightData == null;
    }

    protected bool TryGetHeightMapImageData(CommandList commandList, out RgbPixelRepository pixels)
    {
      if (HeightMap?.Width > 0)
      {
        pixels = new RgbPixelRepository(HeightMap.GetDataAsImage(commandList).PixelBuffer[0]);     
      }
      else
      {
        pixels = default;
      }

      return pixels != null;
    }
  }
}