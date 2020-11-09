using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter importer = assetImporter as TextureImporter;
        if (importer.assetPath.Contains("Assets/Resources/FGUI"))
        {
            importer.mipmapEnabled = false;
            importer.textureType = TextureImporterType.Sprite;
            importer.alphaSource = TextureImporterAlphaSource.None;
            // 保存更改
            EditorUtility.SetDirty(importer);
        }
    }
}
