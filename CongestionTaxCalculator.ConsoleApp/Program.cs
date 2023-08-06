using System;
using CongestionTaxCalculator.ConsoleApp.Resources;

namespace CongestionTaxCalculator.ConsoleApp
{
    internal partial class Program
    {
        private static Program _program;

        private static void Main(string[] args)
        {
            _program = CreateProgramInstance();

            if (_program == null)
            {
                Console.WriteLine(Constants.ProgramInstanceCreationError);
                return;
            }

            InsertData();
            CalculateTotalCongestionTax();
        }
    }
}