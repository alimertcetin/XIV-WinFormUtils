namespace XIV.SaveSystems
{
    public interface ISavable
    {
        object GetSaveData();
        void Load(object loadedData);
    }
}