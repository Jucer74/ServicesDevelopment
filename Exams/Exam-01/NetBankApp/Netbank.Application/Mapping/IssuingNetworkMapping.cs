using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using NetBank.Domain;

namespace NetBank.Application.Mapping
{
    // Esta clase es una clase que mapea modelos de red emisora a objetos de datos de red emisora.

    public class IssuingNetworkMapping
    {
        // Este método convierte un modelo de red emisora en un objeto de datos de red emisora.

        public static IssuingNetworkData ToIssuingNetworkData(IssuingNetwork issuingNetwork)
        {
            // Crea un nuevo objeto de datos de red emisora.
            IssuingNetworkData issuingNetworkData = new IssuingNetworkData();

            // Establece el nombre de la red emisora.
            issuingNetworkData.Name = issuingNetwork.Name;

            // Convierte la lista separada por comas de números iniciales en una lista de enteros.
            issuingNetworkData.StartsWithNumbers = NumberConverter.ComaSeparatedValuesToIntList(issuingNetwork.StartsWithNumbers);

            // Convierte el rango de números separado por guiones en una lista de enteros.
            issuingNetworkData.InRange = NumberConverter.HyphenSeparatedValuesToRangeNumber(issuingNetwork.InRange);

            // Convierte la lista separada por comas de longitudes permitidas en una lista de enteros.
            issuingNetworkData.AllowedLengths = NumberConverter.ComaSeparatedValuesToIntList(issuingNetwork.AllowedLengths);

            // Devuelve el objeto de datos de red emisora.
            return issuingNetworkData;
        }

        // Este método convierte una lista de modelos de red emisora en una lista de objetos de datos de red emisora.

        public static List<IssuingNetworkData> ToIssuingNetworkDataList(List<IssuingNetwork> issuingNetworks)
        {
            // Crea una nueva lista de objetos de datos de red emisora.
            List<IssuingNetworkData> issuingNetworkDataList = new();

            // Itera sobre los modelos de red emisora.
            foreach (IssuingNetwork issuingNetwork in issuingNetworks)
            {
                // Convierte el modelo de red emisora en un objeto de datos de red emisora.
                IssuingNetworkData issuingNetworkData = ToIssuingNetworkData(issuingNetwork);

                // Agrega el objeto de datos de red emisora a la lista.
                issuingNetworkDataList.Add(issuingNetworkData);
            }

            // Devuelve la lista de objetos de datos de red emisora.
            return issuingNetworkDataList;
        }
    }
}
