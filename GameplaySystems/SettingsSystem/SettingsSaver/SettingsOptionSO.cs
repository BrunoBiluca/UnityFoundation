using UnityEngine;

namespace UnityFoundation.SettingsSystem
{
    public abstract class SettingsOptionSO<T>
        : ScriptableObject,
        ISettingsSaver<T>
        where T : ISettingsOption
    {
        [field: SerializeField] public string OptionName { get; private set; }

        [field: SerializeField] public T Value { get; private set; }

        public void OnEnable()
        {
            CreateIfNotExists();
        }

        protected abstract T Instantiate();

        public void Save(T value)
        {
            Value = value;
        }

        public bool Load(out T value)
        {
            if(Value == null)
            {
                value = default;
                return false;
            }

            value = Value;
            return true;
        }

        public void CreateIfNotExists()
        {
            Value ??= Instantiate(); 
        }
    }
}