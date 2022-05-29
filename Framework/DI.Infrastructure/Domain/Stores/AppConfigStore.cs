using System;
using System.ComponentModel;
using System.Threading.Tasks;
using DI.Domain.Config;
using DI.Domain.Core;
using DI.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DI.Domain.Stores
{
    public class AppConfigStore : DbQueryBase<AppConfigEntity>, IAppConfigStore
    {
        public AppConfigStore(DbSet<AppConfigEntity> set, IDataStore dataStore) : base(set, dataStore)
        {
        }

        public async Task<T> GetObjValue<T>(string key)
        {
            var value = await GetValue(key);
            if (value == null)
                return default;

            var tc = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                var convertedValue = (T) tc.ConvertFromString(value);
                return convertedValue;
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }

        public async Task<T> SetObjValue<T>(string key, T objValue)
        {
            await SetValue(key, JsonConvert.SerializeObject(objValue));
            return objValue;
        }

        public async Task<object> SetValue(string key, object value)
        {
            var keyName = key.Trim().ToLower();
            keyName.ThrowIfNull("Key is null ");
            value.ThrowIfNull("value is null ");

            var item = await Qry(x => x.Name == keyName)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                var ks = new AppConfigEntity
                {
                    Name = keyName,
                    Value = value.ToString()
                };
                await Set.AddAsync(ks);
            }
            else
            {
                item.Value = value.ToString();
                Set.Update(item);
            }

            await DataStore.SaveAsync();
            return value;
        }

        public async Task<string> GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;
            var keyName = key.ToLower().Trim();
            var item = await Qry(x => x.Name == keyName)
                .FirstOrDefaultAsync();
            return item?.Value;
        }
    }
}