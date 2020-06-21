using System;
using System.Threading.Tasks;
using Covid19Radar.Common;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Covid19Radar.Services
{
    public static class UserDataMigrationService
    {
        public async static Task Migrate()
        {
            await MigratePropetiesToSecureStorage(AppConstants.StorageKey.ExposureNotificationConfigration);
            await MigratePropetiesToSecureStorage(AppConstants.StorageKey.Secret);
            await MigratePropetiesToSecureStorage(AppConstants.StorageKey.UserData);
        }

        private async static Task MigratePropetiesToSecureStorage(string key)
        {
            var maybeMigratedValue = await SecureStorage.GetAsync(key);
            if (maybeMigratedValue == null && Application.Current.Properties.TryGetValue(key, out var originalValue))
            {
                if (originalValue == null)
                {
                    return;
                }
                await SecureStorage.SetAsync(key, originalValue as string);
                Application.Current.Properties.Remove(key);
                await Application.Current.SavePropertiesAsync();
            }
        }
    }
}
