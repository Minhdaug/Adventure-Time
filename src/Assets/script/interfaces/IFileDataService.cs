public interface IDataService 
{
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted);
    public T LoadData<T>(string RelativePath, bool Encryted);
}
