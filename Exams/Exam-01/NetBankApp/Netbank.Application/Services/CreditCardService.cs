using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using System.Linq;
using NetBank.Utilities;
using System.Text.RegularExpressions;

namespace Netbank.Application.Services;

public class CreditCardService : ICreditCardService
{
    #region Loval-Vars

    private readonly IIssuingNetworkRepository _issuingNetworkRepository;

    // Regular Expression To Validate Only Numbers
    private const string NUMBER_REGEX = "^[0-9]*$";

    #endregion Loval-Vars

    #region Properties

    public CreditCardResult Result { get; set; } = null!;



    #endregion Properties

    public CreditCardService(IIssuingNetworkRepository issuingNetworkRepository)
    {
        _issuingNetworkRepository = issuingNetworkRepository;
    }

    public async Task<ValidationResultType> Validate(string creditCardNumber)

    {

        bool isValid = false; //CreditCardValidator.IsValid(creditCardNumber);
        string nombreTarjeta = " ";


        //Traer la los datos de la DB
        List<IssuingNetwork> issuingNetworksList = await GetIssuingNetworks();

        if (Regex.IsMatch(creditCardNumber, NUMBER_REGEX))
        {
            foreach (IssuingNetwork issuingNetwork in issuingNetworksList)
            {
                if (detectarRedEmisora(creditCardNumber, issuingNetwork))
                {

                    nombreTarjeta = issuingNetwork.Name;

                    if (VerificarLongitud(creditCardNumber, issuingNetwork.AllowedLengths))
                    {
                        isValid = CreditCardValidator.IsValid(creditCardNumber);
                        Result = new CreditCardResult(nombreTarjeta, isValid);
                        return ValidationResultType.Ok;

                    }
                    else
                    {

                        Result = new CreditCardResult(nombreTarjeta, isValid);
                        return ValidationResultType.Ok;
                    }

                }



            }
            isValid = false;
            nombreTarjeta = "Not Found";
            Result = new CreditCardResult(nombreTarjeta, isValid);
            return ValidationResultType.NotFound;


        }
        else
        {
            isValid = false;
            nombreTarjeta = "Bad Request";
            Result = new CreditCardResult(nombreTarjeta, isValid);
            return ValidationResultType.BadRequest;

        }






        throw new NotImplementedException();
    }






    private async Task<List<IssuingNetwork>> GetIssuingNetworks()
    {
        var issuingNetworks = await _issuingNetworkRepository.GetAllAsync();
        return issuingNetworks.ToList();
    }

    

    static bool IsNumeroEnRango(string numeroTarjeta, string rango)
    {
        if (string.IsNullOrEmpty(rango) || string.IsNullOrEmpty(numeroTarjeta))
        {
            return false;
        }

        string[] limites = rango.Split('-');

        if (limites.Length != 2 || !(Regex.IsMatch(limites[0], NUMBER_REGEX)) || !Regex.IsMatch(limites[0], NUMBER_REGEX))
        {
            return false;
        }

        int limiteInferior = int.Parse(limites[0]);
        int limiteSuperior = int.Parse(limites[1]);

        if (Regex.IsMatch(numeroTarjeta, NUMBER_REGEX))
        {
            int numeroTarjetaEntero = int.Parse(numeroTarjeta.Substring(0, limites[0].Length));
            return numeroTarjetaEntero >= limiteInferior && numeroTarjetaEntero <= limiteSuperior;
        }

        return false;
    }

    static bool detectarRedEmisora(string cardNumber, IssuingNetwork tarjeta)
    {



        if (tarjeta.InRange != null)
        {
            if (IsNumeroEnRango(cardNumber, tarjeta.InRange))
            {
                return true;
            }

        }

        if (inicioNumero(cardNumber, tarjeta.StartsWithNumbers))
        {
            return true;
        }

        return false;
    }


    static bool inicioNumero(string cardNumber, string startingNumbers)
    {
        if (string.IsNullOrEmpty(startingNumbers))
        {
            return false;
        }

        string[] startingNumbersList = startingNumbers.Split(',');
        return startingNumbersList.Any(startingNumber => cardNumber.StartsWith(startingNumber.Trim()));
    }



    static bool VerificarLongitud(string numeroTarjeta, string longitudesPermitidas)
    {
        var longitudes = longitudesPermitidas.Split(',');
        int longitudTarjeta = numeroTarjeta.Length;

        return longitudes.Any(longitud => longitudTarjeta == int.Parse(longitud));
    }





}