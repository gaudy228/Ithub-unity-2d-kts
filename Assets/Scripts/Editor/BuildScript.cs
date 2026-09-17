using System.IO;
using UnityEditor;

public class BuildScript
{
    private const string PcBuildPath = "Builds/PC/Game.exe";
    private const string AndroidBuildPath = "Builds/Android/Game.apk";

    private static string[] Scenes =>
        EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);

    [MenuItem("Build/Build PC")]
    public static void BuildPC()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(PcBuildPath));

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = Scenes,
            locationPathName = PcBuildPath,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(options);
    }

    [MenuItem("Build/Build Android")]
    public static void BuildAndroid()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(AndroidBuildPath));

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = Scenes,
            locationPathName = AndroidBuildPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(options);
    }

    [MenuItem("Build/Build All")]
    public static void BuildAll()
    {
        BuildPC();
        BuildAndroid();
    }
}
