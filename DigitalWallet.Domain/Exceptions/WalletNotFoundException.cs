using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.Exceptions
{
    public class WalletNotFoundException : DomainException
    {
        public string SearchedValue { get; }

        public WalletNotFoundException(string searchedValue)
            : base($"'{searchedValue}' ilə uyğun cüzdan tapılmadı.")
        {
            SearchedValue = searchedValue;
        }
    }
}
