using System.Threading.Tasks;

namespace Pure.NetCoreExtensions
{
    public interface IGeolocationProvider
    {
        Task<IpInformation> GeolocateAsync(string ip);
    }
}
