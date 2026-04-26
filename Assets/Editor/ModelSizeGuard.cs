using UnityEditor;
using UnityEngine;
using System.IO;

public class ModelSizeGuard : AssetPostprocessor
{
    private const long MaxFileSizeMB = 20;
    private const long MaxFileSizeBytes = MaxFileSizeMB * 1024 * 1024;

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            if (assetPath.EndsWith(".fbx", System.StringComparison.OrdinalIgnoreCase) ||
                assetPath.EndsWith(".glb", System.StringComparison.OrdinalIgnoreCase))
            {
                FileInfo fileInfo = new FileInfo(assetPath);

                if (fileInfo.Exists)
                {
                    if (fileInfo.Length > MaxFileSizeBytes)
                    {
                        double actualSizeMB = (double)fileInfo.Length / (1024 * 1024);

                        EditorUtility.DisplayDialog("Import Blocked",
                            $"The model '{fileInfo.Name}' is {actualSizeMB:F2}MB, which exceeds the {MaxFileSizeMB}MB limit. It will be deleted.", "OK");

                        AssetDatabase.DeleteAsset(assetPath);

                        Debug.LogWarning($"[ModelSizeGuard] Blocked and deleted: {assetPath} ({actualSizeMB:F2}MB)");
                    }
                }
            }
        }
    }
}