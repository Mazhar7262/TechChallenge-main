using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using TechChallenge.Exporters.Infrastructure.Services;
using Xunit;

namespace TechChallenge.Exporters.Tests.Infrastructure
{
	
	public class BankOfIrelandPaymentExporterTests
    {
        [Fact]
        public void ATestHere()
        {
            //Ideally please use fluent notation
            //e.g. 
            //var payment.Name.Length.Should().Be(100);
        }

		[Fact]
		public async void ExtractPaymentsAsyncTest()
		{
			var allPayments = Common.Infrastructure.Helpers.TechChallengeHelper.Payments;
			var test = await BankOfIrelandPaymentExporter.ExtractPaymentsAsync("",allPayments);
			test.Should().NotBeNull();
		}
		[Fact]
		public async void ValidatePaymentsAsyncTest()
		{
			var allPayments = Common.Infrastructure.Helpers.TechChallengeHelper.Payments;
			var validationDictionary = new Dictionary<int, List<string>>();
			var test = await BankOfIrelandPaymentExporter.ValidatePaymentsAsync(allPayments, validationDictionary);
			foreach (var item in test)
			{
				item.Amount.Should().BeGreaterThan(320);
			}
		}
		[Fact]
		public async void ExportPaymentsAsyncTest()
		{
			var allPayments = Common.Infrastructure.Helpers.TechChallengeHelper.Payments;
			var test = await BankOfIrelandPaymentExporter.ExportPaymentsAsync(allPayments);
			test.Should().NotBeNull();
		}
	}
}
