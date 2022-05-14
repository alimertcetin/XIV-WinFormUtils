namespace XIV.SaveSystems
{
    public interface ISaveable
    {
        object GetSaveData();
        void Load(object loadedData);
    }
}