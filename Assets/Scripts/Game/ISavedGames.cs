public interface ISavedGames
{
    void Write<T>(T value);
    T Read<T>();
}
