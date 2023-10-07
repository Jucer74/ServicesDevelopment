<<<<<<< HEAD
namespace NetBank.Domain.Dto;
=======
﻿namespace NetBank.Domain.Dto;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83

public class CreditCardResult
{
    public string IssuingNetwork { get; set; }
    public bool Valid { get; set; }

    public CreditCardResult(string issuingNetworkName, bool valid)
    {
        IssuingNetwork = issuingNetworkName;
        Valid = valid;
    }
}