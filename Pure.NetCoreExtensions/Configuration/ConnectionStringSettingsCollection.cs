using System.Collections.Generic;
namespace Pure.NetCoreExtensions
{
    public class ConnectionStringSettingsCollection: List<ConnectionStringSettings>
    {
        public ConnectionStringSettings this[string name]
        {
            get
            {
                for (int i = 0; i < base.Count; i++)
                {
                    if (base[i].Name.ToLower() == name.ToLower())
                    {
                        return base[i];
                    }
                }
                return null;
            }
        }

    }
}