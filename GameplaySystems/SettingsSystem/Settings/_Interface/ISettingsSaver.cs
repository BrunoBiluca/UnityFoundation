namespace UnityFoundation.SettingsSystem
{
    public interface ISettingsSaver<T> where T : ISettingsOption
    {
        bool Load(out T value);
        void Save(T value);
    }
}