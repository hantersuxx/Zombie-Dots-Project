public class LocalSerializer : ISavedGames
{
    public void Write<T>(T value)
    {
        string json = value.SerializeJson();
        string filename = Globals.GetFilename<T>();
        Extensions.WriteText(filename, json);
        Extensions.Log(GetType(), $"{filename} saved.");
    }

    public T Read<T>()
    {
        string filename = Globals.GetFilename<T>();
        string json = Extensions.ReadText(filename);
        Extensions.Log(GetType(), $"{filename} read.");
        return json.DeserializeJson<T>();
    }
}
