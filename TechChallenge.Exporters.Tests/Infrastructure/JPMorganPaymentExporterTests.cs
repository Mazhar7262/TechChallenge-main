using FluentAssertions;
using TechChallenge.Exporters.Infrastructure.Services;
using Xunit;

namespace TechChallenge.Exporters.Tests.Infrastructure
{
	public class JPMorganPaymentExporterTests
    {
		[Fact]
		public void ExtractPaymentsAsyncTest()
		{
			var allPayments = Common.Infrastructure.Helpers.TechChallengeHelper.Payments;
			var test = JPMorganPaymentExporter.ExtractPaymentsAsync("", allPayments);
			test.Should().NotBeNull();
		}
		[Fact]
		public async void ValidatePaymentsAsyncTest()
		{
			var allPayments = Common.Infrastructure.Helpers.TechChallengeHelper.Payments;
			var validationDictionary = new Dictionary<int, List<string>>();
			var test = await JPMorganPaymentExporter.ValidatePaymentsAsync(allPayments, validationDictionary);
			test.Should().NotBeNull();
		}
		[Fact]
		public void ExportPaymentsAsyncTest()
		{
			var allPayments = Common.Infrastructure.Helpers.TechChallengeHelper.Payments;
			var test = JPMorganPaymentExporter.ExportPaymentsAsync(allPayments);
			test.Should().NotBeNull();
		}
	}
}
