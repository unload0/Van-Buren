using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;
using System;

public class GLBAutoProcessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string path in importedAssets)
        {
            if (path.EndsWith(".glb") && !path.Contains("_compressed"))
            {
                // CompressGLB(path);
            }
        }
    }

    static void CompressGLB(string assetPath)
    {
        string fullPath = Path.GetFullPath(assetPath);
        string outPath = fullPath.Replace(".glb", "_compressed.glb");

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                // /C tells cmd to run the command and then terminate
                Arguments = $"/C gltf-pipeline -i \"{fullPath}\" -o \"{outPath}\" -d",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(@"gltf-pipeline is not installed, try doing ""npm install -g gltf-pipeline"". Make sure npm is installed aswell");
            return;
        }

        // Optional: Replace original with compressed
        if (File.Exists(outPath))
        {
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.Refresh();
        }

        UnityEngine.Debug.Log($"Auto-compressed GLB: {assetPath}");
    }
}