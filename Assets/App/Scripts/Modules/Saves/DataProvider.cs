﻿using Unity.Plastic.Newtonsoft.Json;

namespace App.Scripts.Modules.Saves
{
    public class DataProvider<T> : IDataProvider<T> where T : class
    {
        private string key;
        private IStorage storage;

        private JsonSerializerSettings settings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
        };

        public DataProvider(string key, IStorage storage)
        {
            this.key = key;
            this.storage = storage;
        }

        public void DeleteData()
        {
            storage.DeleteString(key);
        }

        public T GetData()
        {
            var json = storage.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public bool HasData()
        {
            var json = storage.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }

            return true;
        }

        public void SaveData(T data)
        {
            var json = JsonConvert.SerializeObject(data, settings);
            storage.SetString(key, json);
        }
    }
}