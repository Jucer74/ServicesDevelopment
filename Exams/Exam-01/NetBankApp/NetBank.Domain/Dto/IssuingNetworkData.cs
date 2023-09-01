namespace NetBank.Domain.Dto
{
    public class IssuingNetworkData
    {
        public string Name { get; set; } = null!;
        public List<int>? StartsWithNumbers { get; set; } = null!;
        public RangeNumber? InRange { get; set; } = null!;
        public List<int>? AllowedLengths { get; set; } = null!;

        // Método para validar si una tarjeta de crédito es identificada por esta red emisora
        public Boolean ValidateCreditCard(string creditCardNumber)
        {
            Boolean isIdentified = false;

            // Comprueba si la tarjeta de crédito comienza con números específicos o está en el rango especificado
            if (this.ValidateStartsNumbers(creditCardNumber) || this.ValidateInRange(creditCardNumber))
            {
                isIdentified = true;
            }

            return isIdentified;
        }

        // Método para validar las longitudes permitidas
        public bool ValidateAllowedLengths(string creditCardNumber)
        {
            bool isValid = false;

            // Comprueba si las longitudes permitidas contienen la longitud de la tarjeta de crédito
            if (this.AllowedLengths != null && this.AllowedLengths.Contains(creditCardNumber.Length))
            {
                isValid = true;
            }

            return isValid;
        }

        // Método privado para validar si el número está en el rango especificado
        private Boolean ValidateInRange(string creditCardNumber)
        {
            Boolean isValid = false;
            if (this.InRange != null)
            {
                string numString = this.InRange.MinValue.ToString();
                int numLength = numString.Length;
                string cuttedCreditCard = creditCardNumber.Substring(0, numLength);
                int? doubleCreditCard = Alter.TurningStringIntoInt(cuttedCreditCard);

                // Comprueba si el número de la tarjeta de crédito está en el rango especificado
                if (doubleCreditCard >= this.InRange.MinValue && doubleCreditCard <= this.InRange.MaxValue)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        // Método privado para validar si la tarjeta de crédito comienza con los números especificados
        private Boolean ValidateStartsNumbers(string creditCardNumber)
        {
            Boolean isValid = false;
            if (this.StartsWithNumbers != null)
            {
                foreach (int num in this.StartsWithNumbers)
                {
                    string numString = num.ToString();
                    int numLength = numString.Length;
                    string cuttedCreditCard = creditCardNumber.Substring(0, numLength);

                    // Comprueba si la tarjeta de crédito comienza con los números especificados
                    if (cuttedCreditCard == numString)
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }

        // Clase interna para realizar transformaciones y conversiones
        public static class Alter
        {
            // Método para convertir una cadena separada por comas en una lista de enteros
            public static List<int>? AlterCommaIntoaList(string comma)
            {
                List<int>? intList = null;
                if (comma != null)
                {
                    intList = new List<int>();
                    List<string> stringList = AlterDistinctValuationToaList(comma, ',');
                    foreach (var str in stringList)
                    {
                        int? intValue = TurningStringIntoInt(str);
                        if (intValue != null)
                        {
                            intList.Add(intValue.Value);
                        }
                    }
                }
                return intList;
            }

            // Método para convertir un rango especificado con guiones en un objeto RangeNumber
            public static RangeNumber? ChangeGuionRange(string guion)
            {
                RangeNumber? rangeNumber = null;
                if (guion != null)
                {
                    List<int> intList = ConvertGuionToSeparatedToNumberRange(guion);

                    if (intList.Count == 2)
                    {
                        rangeNumber = new RangeNumber();
                        rangeNumber.MinValue = intList[0];
                        rangeNumber.MaxValue = intList[1];
                    }
                }
                return rangeNumber;
            }

            // Método para convertir un rango especificado con guiones en una lista de enteros
            public static List<int> ConvertGuionToSeparatedToNumberRange(string guion)
            {
                List<int> intList = new List<int>();
                List<string> stringList = AlterDistinctValuationToaList(guion, '-');
                foreach (var str in stringList)
                {
                    int? num = TurningStringIntoInt(str);
                    if (num != null)
                    {
                        intList.Add(num.Value);
                    }
                }
                return intList;
            }

            // Método para dividir una cadena en una lista de cadenas usando un separador especificado
            public static List<string> AlterDistinctValuationToaList(string sv, char separator)
            {
                List<string> stringList = sv.Split(separator).ToList();
                return stringList;
            }

            // Método para convertir una cadena en un entero
            public static int? TurningStringIntoInt(string str)
            {
                int? num;
                try
                {
                    num = int.Parse(str);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    num = null;
                }
                return num;
            }

            // Método para convertir una cadena en un número decimal
            public static Double? AlterStringIntoDouble(string str)
            {
                Double? num;
                try
                {
                    num = Double.Parse(str);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    num = null;
                }
                return num;
            }
        }
    }
}
