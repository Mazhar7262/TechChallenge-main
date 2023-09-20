using System.Diagnostics.Metrics;
using TechChallenge.Common.Infrastructure.Dto;

namespace TechChallenge.Exporters.Infrastructure.Services
{
	public class JPMorganPaymentExporter
	{
		public static Task<List<Payment>> ExtractPaymentsAsync(string selectedBank, List<Payment> allPayments)
		{
			var selectedPayments = new List<Payment>();
			foreach (var payment in allPayments)
			{
				if (payment.Currency == "GBP" || (payment.Currency == "USD" && payment.Amount <= 100000))
				{
					selectedPayments.Add(payment);
				}
			}
			return Task.FromResult(selectedPayments);
		}

		public static Task<List<Payment>> ValidatePaymentsAsync(List<Payment> selectedPayments, Dictionary<int, List<string>> validationDictionary)
		{
			var validPayments = new List<Payment>();
			var errors = new List<string>();
			
			for (int i = 0; i <= selectedPayments.Count - 1; i++)
			{
				if (!Validate(selectedPayments, errors, i))
					validPayments.Add(selectedPayments[i]);
			}
			validationDictionary.Add(2, errors);
			return Task.FromResult(selectedPayments);
		}

		private static bool Validate(List<Payment> selectedPayments, List<string> errors, int i)
		{
			bool IsNotValid = false;
			if (string.IsNullOrWhiteSpace(selectedPayments[i].Country))
			{
				errors.Add("Country is mandatory");
				IsNotValid = true;
			}
			if (selectedPayments[i].Amount == 0)
			{
				errors.Add("Amount cannot be 0.");
				IsNotValid = true;
			}
			if (string.IsNullOrWhiteSpace(selectedPayments[i].Postcode))
			{
				errors.Add("Postcode is null or empty.");
				IsNotValid = true;
			}
			if ($"{selectedPayments[i].BeneficiaryLastName} {selectedPayments[i].BeneficiaryLastName}".Length > 30)
			{
				errors.Add("BeneficiaryName exceeded 30 characters.");
				IsNotValid = true;
			}
		
			return IsNotValid;
		}

		public static Task<string> ExportPaymentsAsync(List<Payment> validPayments)
		{
			var path = Path.GetTempFileName();
			using (var file = File.CreateText(path))
			{
				file.WriteLine("Currency,Amount,BeneficiaryName,Address,Postcode");
				foreach (var arr in validPayments)
				{
					string str = $"{arr.Currency},{arr.Amount},{arr.BeneficiaryFirstName} {arr.BeneficiaryLastName},{arr.Address},{arr.Postcode}";
					file.WriteLine(String.Join(",", str));
				}
				
			}
			return Task.FromResult(path);
		}
	}


}


