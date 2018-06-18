public interface IPooledObject
{
    void HandleObjectSpawn(object value);
    void HandleObjectDestroy();
}