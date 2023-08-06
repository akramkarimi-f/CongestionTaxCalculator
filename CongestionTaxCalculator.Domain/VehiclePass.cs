using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class VehiclePass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Vehicle Vehicle { get; set; }
        public string LicensePlate { get; set; }
        public int TollFee { get; set; }
        public DateTime PassDateTime { get; set; }
        public DateTime LastUpdate { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}