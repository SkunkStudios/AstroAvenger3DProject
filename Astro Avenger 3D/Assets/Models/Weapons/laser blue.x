xof 0303txt 0032
template XSkinMeshHeader {
 <3cf169ce-ff7c-44ab-93c0-f78f62d172e2>
 WORD nMaxSkinWeightsPerVertex;
 WORD nMaxSkinWeightsPerFace;
 WORD nBones;
}

template VertexDuplicationIndices {
 <b8d65549-d7c9-4995-89cf-53a9a8b031e3>
 DWORD nIndices;
 DWORD nOriginalVertices;
 array DWORD indices[nIndices];
}

template SkinWeights {
 <6f0d123b-bad2-4167-a0d0-80224f25fabb>
 STRING transformNodeName;
 DWORD nWeights;
 array DWORD vertexIndices[nWeights];
 array FLOAT weights[nWeights];
 Matrix4x4 matrixOffset;
}


Frame Scene_Root {
 

 FrameTransformMatrix {
  1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000;;
 }

 Frame lazer_blue {
  

  FrameTransformMatrix {
   1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000;;
  }

  Mesh {
   4;
   0.037009;0.335491;0.000000;,
   -0.037009;0.335491;0.000000;,
   0.037009;-0.335491;-0.000000;,
   -0.037009;-0.335491;-0.000000;;
   2;
   3;2,0,3;,
   3;1,3,0;;

   MeshNormals {
    4;
    0.000000;-0.000000;1.000000;,
    0.000000;-0.000000;1.000000;,
    0.000000;-0.000000;1.000000;,
    0.000000;-0.000000;1.000000;;
    2;
    3;2,0,3;,
    3;1,3,0;;
   }

   MeshTextureCoords {
    4;
    0.454193;0.947090;,
    0.530908;0.947090;,
    0.454193;0.094681;,
    0.530908;0.094681;;
   }

   MeshMaterialList {
    1;
    2;
    0,
    0;

    Material {
     1.000000;1.000000;1.000000;1.000000;;
     0.000000;
     1.000000;1.000000;1.000000;;
     0.000000;0.000000;0.000000;;

     TextureFilename {
      "D:\\DevStudio\\MyProjects\\Astro Avenger\\Data\\Max\\all ships\\lazer_blue.tga";
     }
    }
   }
  }
 }
}