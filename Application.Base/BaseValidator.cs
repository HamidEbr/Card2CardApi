using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Base
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected bool IsValidPan(string pan)
        {
            if (string.IsNullOrEmpty(pan))
                return false;

            if (pan.Length != 16)
                return false;

            if (pan.Any(panDigit => !char.IsDigit(panDigit)))
                return false;

            var checkSum = 0;
            for (short i = 0; i < pan.Length; i++)
                if (i % 2 == 0)
                {
                    var sum = short.Parse(pan[i].ToString()) * 2;
                    if (sum > 9)
                        sum = sum - 9;

                    checkSum += sum;
                }

                else
                {
                    checkSum += short.Parse(pan[i].ToString());
                }

            if (checkSum % 10 != 0)
                return false;

            return true;
        }

        protected bool IsValidMobileNumber(string mobileNumber)
        {
            var pattern = @"09\d{9}";
            return Regex.IsMatch(mobileNumber, pattern);
        }

        protected bool IsValidCVV2(string cvv2)
        {
            if (string.IsNullOrEmpty(cvv2))
                return false;

            if (cvv2.Length < 3 || cvv2.Length > 4)
                return false;

            if (cvv2.Any(c => !char.IsNumber(c))) return false;
            return true;
        }

        protected bool IsValidExpYear(string expYear)
        {
            if (string.IsNullOrEmpty(expYear))
                return false;

            if (expYear.Length != 2)
                return false;

            if (expYear.Any(c => !char.IsNumber(c))) return false;

            return true;
        }

        protected bool IsValidExpMonth(string expMonth)
        {
            if (string.IsNullOrEmpty(expMonth))
                return false;

            if (expMonth.Length != 2)
                return false;

            if (expMonth.Any(c => !char.IsNumber(c))) return false;

            if (!int.TryParse(expMonth, out int iExpMonth)) return false;

            if (iExpMonth < 1 || iExpMonth > 12) return false;

            return true;
        }
    }

}
