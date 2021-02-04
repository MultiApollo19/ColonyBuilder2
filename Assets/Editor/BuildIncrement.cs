using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.IO;


public class BuildIncrement : IPreprocessBuildWithReport
{
    public int callbackOrder => 1;



    public void OnPreprocessBuild(BuildReport report)
    {
        BuildScriptableObject buildScriptableObject = ScriptableObject.CreateInstance<BuildScriptableObject>();

        switch (report.summary.platform) {
            case BuildTarget.StandaloneWindows64:
                PlayerSettings.macOS.buildNumber = IncrementBuildNumber(PlayerSettings.macOS.buildNumber);
                buildScriptableObject.BuildNumber = PlayerSettings.macOS.buildNumber;
                break;
        }
        AssetDatabase.DeleteAsset("Assets/Resources/Build.asset");
        AssetDatabase.CreateAsset(buildScriptableObject,"Assets/Resources/Build.asset");
        AssetDatabase.SaveAssets();
    }

    private string IncrementBuildNumber(string buildNumber) {
        int.TryParse(buildNumber, out int outputBuildNumber);

        return (outputBuildNumber + 1).ToString();
    }
}
