public static class Path
{
    public const string RTS_RESOURCES = "Assets/Resources/RTS/";
    public static string Generate(string name)
    {
        return $"{RTS_RESOURCES}{name}.asset";
    }
    public static string Generate<T>(string prefix = "", string suffix = "")
    {
        return Generate(prefix + typeof(T).Name + suffix);
    }
}
