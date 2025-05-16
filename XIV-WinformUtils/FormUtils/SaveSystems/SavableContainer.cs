namespace XIV.SaveSystems
{
    public struct SavableContainer<T> : ISavable
    {
        public T[] savables;

        public static SavableContainer<T> Create(T[] savables)
        {
            return new SavableContainer<T>() { savables = savables };
        }

        public object GetSaveData()
        {
            return this;
        }

        public void Load(object loadedData)
        {
            savables = ((SavableContainer<T>)loadedData).savables;
        }
    }
}
