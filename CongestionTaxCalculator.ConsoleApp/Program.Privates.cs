using System;
using CongestionTaxCalculator.Application.Interfaces;
using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Application.Interfaces.Services;
using CongestionTaxCalculator.Application.Services;
using CongestionTaxCalculator.ConsoleApp.Resources;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Infrastructure;
using CongestionTaxCalculator.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.ConsoleApp
{
    internal partial class Program
    {
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // DbContext registration
            var cnn = new SqliteConnection("Filename=:memory:");
            cnn.Open();
            services.AddDbContext<TaxContext>(options =>
                options.UseSqlite(cnn));

            // Application layer registration
            services.AddScoped<ICongestionTaxCalculator, Application.CongestionTaxCalculator>();
            services.AddScoped<ITaxRuleService, TaxRuleService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IVehiclePassService, VehiclePassService>();

            // Infrastructure layer registration
            services.AddScoped<ITaxRuleRepository, TaxRuleRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IVehiclePassRepository, VehiclePassRepository>();

            return services.BuildServiceProvider();
        }

        private static void InsertData()
        {
            _program.AddNewCity(1, "Gothenburg");
            InsertGothenburgTaxRules();
            InsertVehiclePasses();
        }

        private static void InsertGothenburgTaxRules()
        {
            _program.AddNewTaxRule(1, TimeSpan.FromHours(6),
                TimeSpan.FromHours(6).Add(TimeSpan.FromMinutes(30)), 8);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(6).Add(TimeSpan.FromMinutes(30)),
                TimeSpan.FromHours(7), 13);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(7),
                TimeSpan.FromHours(8), 18);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(8),
                TimeSpan.FromHours(8).Add(TimeSpan.FromMinutes(30)), 13);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(8).Add(TimeSpan.FromMinutes(30)),
                TimeSpan.FromHours(15), 8);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(15),
                TimeSpan.FromHours(15).Add(TimeSpan.FromMinutes(30)), 13);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(15).Add(TimeSpan.FromMinutes(30)),
                TimeSpan.FromHours(17), 18);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(17),
                TimeSpan.FromHours(18), 13);
            _program.AddNewTaxRule(1, TimeSpan.FromHours(18),
                TimeSpan.FromHours(18).Add(TimeSpan.FromMinutes(30)), 8);
        }

        private static void InsertVehiclePasses()
        {
            var dateTimes = new[]
            {
                new DateTime(2013, 1, 14, 21, 0, 0),
                new DateTime(2013, 1, 15, 21, 0, 0),
                new DateTime(2013, 2, 7, 6, 23, 27),
                new DateTime(2013, 2, 7, 15, 27, 00),
                new DateTime(2013, 2, 8, 06, 27, 00),
                new DateTime(2013, 2, 8, 06, 20, 27),
                new DateTime(2013, 2, 8, 14, 35, 00),
                new DateTime(2013, 2, 8, 15, 29, 00),
                new DateTime(2013, 2, 8, 15, 47, 00),
                new DateTime(2013, 2, 8, 16, 01, 00),
                new DateTime(2013, 2, 8, 16, 48, 00),
                new DateTime(2013, 2, 8, 17, 49, 00),
                new DateTime(2013, 2, 8, 18, 29, 00),
                new DateTime(2013, 2, 8, 18, 35, 00),
                new DateTime(2013, 3, 26, 14, 25, 00),
                new DateTime(2013, 3, 28, 14, 07, 27)
            };

            foreach (var dateTime in dateTimes)
            {
                foreach (var vehicle in Enum.GetNames(typeof(Vehicle)))
                    _program.AddNewVehiclePass(Enum.Parse<Vehicle>(vehicle), vehicle + "1234-20", dateTime, 1);

                // To test a specific scenario
                _program.AddNewVehiclePass(Vehicle.Car, "Car1234-10", dateTime, 1);
            }
        }

        private static void CalculateTotalCongestionTax()
        {
            while (true)
            {
                Console.WriteLine(Constants.Program_CalculateTotalCongestionTax_EnterLicensePlateMessage);
                var licensePlate = Console.ReadLine();

                var congestionTax = _program.CalculateCongestionTax(licensePlate);

                Console.WriteLine(Constants.Program_CalculateTotalCongestionTax_TotalCongestionTaxOutputMessage,
                    congestionTax);
                Console.WriteLine();
            }
        }
    }
}