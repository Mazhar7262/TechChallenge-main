using System.Text;
using TechChallenge.Common.Infrastructure.Dto;

namespace TechChallenge.Exporters.Infrastructure.Services
{
	public class BankOfIrelandPaymentExporter
	{
		public static Task<List<Payment>> ExtractPaymentsAsync(string selectedBank, List<Payment> allPayments)
		{
			var selectedPayments = new List<Payment>();
			foreach (var payment in allPayments)
			{
				if (payment.Currency == "EUR" || (payment.Currency == "USD" && payment.Amount > 100000))

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
			for (int i = 0; i <= selectedPayments.Count-1; i++)
			{
				if(!Validate(selectedPayments, errors, i))
					validPayments.Add(selectedPayments[i]);
			}
			validationDictionary.Add(1, errors);

			return Task.FromResult(validPayments);
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
			if (int.TryParse(selectedPayments[i].BeneficiaryFirstName, out _)) 
			{ 
				errors.Add("BeneficiaryFirstName contains numbers.");
				IsNotValid = true;
			}

			if (int.TryParse(selectedPayments[i].BeneficiaryLastName, out _))
			{
				errors.Add("BeneficiaryLastName contains numbers..");
				IsNotValid = true;
			}

			if (selectedPayments[i].Amount < 320)
			{
				errors.Add("EUR Payments is less than 320EUR.");
				IsNotValid = true;
			}

			return IsNotValid;
		}

		public static Task<string> ExportPaymentsAsync(List<Payment> validPayments)
		{
			var path = Path.GetTempFileName();
			using (var file = File.CreateText(path))
			{
				file.WriteLine("Currency,Amount,BeneficaryFirstName,BeneficaryLastName,Address");
				foreach (var arr in validPayments)
				{
					string str = $"{arr.Currency},{arr.Amount},{arr.BeneficiaryFirstName},{arr.BeneficiaryLastName},{arr.Address}";
					file.WriteLine(String.Join(",", str));
				}
			}
			return Task.FromResult(path);
		}
	}
	
}


