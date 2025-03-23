using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.IO;
using System.Linq;

public class ForceNullableCsprojPostprocessor : AssetPostprocessor
{
    private const string NullableTag = "<Nullable>enable</Nullable>";

    [DidReloadScripts]
    static void OnScriptsReloaded()
    {
        string projectPath = Directory.GetParent(Application.dataPath).FullName;
        string csprojPath = Path.Combine(projectPath, "Assembly-CSharp.csproj");

        if (!File.Exists(csprojPath)) return;

        string[] lines = File.ReadAllLines(csprojPath);
        if (lines.Any(l => l.Contains(NullableTag))) return; // 이미 적용돼 있다면 무시

        using (StreamWriter writer = new StreamWriter(csprojPath))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line);

                // <LangVersion> 다음 줄에 Nullable 추가
                if (line.Contains("<LangVersion>"))
                {
                    writer.WriteLine("    " + NullableTag);
                }
            }
        }

        Debug.Log(" Nullable 설정을 Assembly-CSharp.csproj에 강제 삽입했습니다.");
    }
}