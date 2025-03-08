using Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Wallets")]
    public class Wallet : EntityBase<int>
    {
        [Required]
        public string WalletAddress { get; set; }
        [Required]
        public string ProviderWallet { get; set; }
        public int UserId { get; set; }
    }
}
