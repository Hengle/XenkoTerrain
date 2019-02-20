﻿using System;
using System.Collections.Generic;
using System.Text;
using Xenko.Core.Mathematics;
using Xenko.Graphics;
using System.IO;

namespace XenkoTerrain.TerrainSystem
{
  // Reference: https://people.cs.clemson.edu/~dhouse/courses/405/docs/brief-obj-file-format.html
  public class TerrainGeometryExporer
  {
    private string Print(Vector2 vector)
    {
      return $"{vector.X:0.00000} {vector.Y:0.00000}";
    }

    private string Print(Vector3 vector)
    {
      return $"{vector.X:0.00000} {vector.Y:0.00000} {vector.Z:0.00000}";
    }
    
    public void SaveObj(string path, TerrainGeometryData data)
    {
      var vertIdx = 0;
      var uvIdx = 0;
      var normIdx = 0;

      using (var sr = new StreamWriter(path))
      {
        sr.WriteLine("# Generated by XenkoTerrain " + DateTime.Now.ToLongDateString());
        sr.WriteLine("# Vertices: " + data.Vertices.Length);
        sr.WriteLine("# Size: " + data.Size);
        sr.WriteLine("usemtl Terrain");

        var topLeft = 0;
        var topRight = 1;
        var bottomLeft = (int)data.TessellationY+1;
        var bottomRight = bottomLeft + 1;        

        while (bottomRight < data.Vertices.Length )
        {
          WriteCurrentQuad();
          MoveNext();          
        }

        void MoveNext()
        {
          topLeft += 1;
          topRight = topLeft + 1;
          bottomLeft = topLeft + (int)data.TessellationY + 1;
          bottomRight = bottomLeft + 1;
        }

        void WriteCurrentQuad()
        {
          var topLeftVertex = data.Vertices[topLeft];
          var topRightVertex = data.Vertices[topRight];
          var bottomLeftVertex = data.Vertices[bottomLeft];
          var botomRightVertex = data.Vertices[bottomRight];


          sr.WriteLine("v " + Print(topRightVertex.Position));
          sr.WriteLine("v " + Print(topLeftVertex.Position));          
          sr.WriteLine("v " + Print(bottomLeftVertex.Position));
          sr.WriteLine("v " + Print(botomRightVertex.Position));

          sr.WriteLine("vn " + Print(topRightVertex.Normal));
          sr.WriteLine("vn " + Print(topLeftVertex.Normal));          
          sr.WriteLine("vn " + Print(bottomLeftVertex.Normal));
          sr.WriteLine("vn " + Print(botomRightVertex.Normal));

          sr.WriteLine("vt " + Print(topRightVertex.TextureCoordinate));
          sr.WriteLine("vt " + Print(topLeftVertex.TextureCoordinate));          
          sr.WriteLine("vt " + Print(bottomLeftVertex.TextureCoordinate));
          sr.WriteLine("vt " + Print(botomRightVertex.TextureCoordinate));

          sr.WriteLine($"f {++vertIdx}/{++uvIdx}/{++normIdx} {++vertIdx}/{++uvIdx}/{++normIdx} {++vertIdx}/{++uvIdx}/{++normIdx} {++vertIdx}/{++uvIdx}/{++normIdx}");
        }
      }
    }
  }
}