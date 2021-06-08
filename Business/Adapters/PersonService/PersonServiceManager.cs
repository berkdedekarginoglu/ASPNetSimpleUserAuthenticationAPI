using System.Globalization;
using System.Threading.Tasks;
using wsKPSPublic;

namespace Business.Adapters.PersonService
{
    public class PersonServiceManager : IPersonService
    {
        public async Task<bool> VerifyCid(Citizen citizen)
        {
            return await Verify(citizen);
        }

        public static async Task<bool> Verify(Citizen citizen)
        {
            var locale = new CultureInfo("tr-TR", false);
            var svc = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);

            var cmd = await svc.TCKimlikNoDogrulaAsync(
                citizen.IdentityNumber,
                citizen.FirstName.ToUpper(locale),
                citizen.LastName.ToUpper(locale),
                citizen.YearOfBirth);
            return cmd.Body.TCKimlikNoDogrulaResult;

        }
    }
}
