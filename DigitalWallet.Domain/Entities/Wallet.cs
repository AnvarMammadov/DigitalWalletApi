using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.Exceptions;

namespace DigitalWallet.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; private set; }    
        public Guid UserId { get; private set; }    
        public string WalletNumber { get; private set; }    
        public string Currency {  get; private set; }   
        public decimal Balance { get; private set; }

        // Race Condition-ın qarşısını alan optimistik kilidləmə nəzarətçisi
        public byte[] RowVersion { get; private set; }

        public void Debit(decimal amount)
        {
            // Qayda 1: Göndərilən məbləğ sıfır və ya mənfi ola bilməz
            if (amount <= 0)
            {
                throw new InvalidTransactionAmountException(amount);
            }

            // Qayda 2: Balans bu məbləği çıxmağa bəs etməlidir
            if (Balance < amount)
            {
                throw new InsufficientFundsException(Id,amount,Balance);
            }

            // Əgər yuxarıdakı "mühafizəçilərdən" keçə bildisə, deməli Happy Path-dir
            Balance -= amount;
        }

        public void Credit(decimal amount)
        {
            if (amount <= 0) { 
                throw new InvalidTransactionAmountException(amount);
            }
            Balance += amount;
        }

        public Wallet(Guid userid , string walletnumber , string currency) 
        {
            Id=Guid.NewGuid();
            UserId = userid;
            WalletNumber = string.IsNullOrWhiteSpace(walletnumber) ? throw new ArgumentNullException(nameof(walletnumber)) : walletnumber;
            Currency = string.IsNullOrWhiteSpace(currency) ? throw new ArgumentNullException(nameof(currency)) : currency;

            Balance = 0;
        }

        // Entity Framework Core üçün boş konstruktor
        private Wallet() { }

    }
}
