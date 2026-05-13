using MassTransit;
using Microsoft.Extensions.Logging;
using Ong.Application.Worker.Clients;
using Ong.Commom;

namespace Ong.Application.Worker.Consumers
{
    public class DonationCreatedConsumer : IConsumer<DonationCreated>
    {
        private readonly ILogger<DonationCreatedConsumer> _logger;
        private readonly ICampaignsApiClient _campaignsApiClient;

        public DonationCreatedConsumer(ILogger<DonationCreatedConsumer> logger, ICampaignsApiClient campaignsApiClient)
        {
            _logger = logger;
            _campaignsApiClient = campaignsApiClient;
        }

        public async Task Consume(ConsumeContext<DonationCreated> context)
        {
            var evento = context.Message;

            _logger.LogInformation(
                "Processando doação: {DonationId} no valor de {Amount} para campanha {CampaignId}",
                evento.DonationId, evento.Amount, evento.CampaignId);

            await _campaignsApiClient.NotifyDonationReceivedAsync(
                evento.CampaignId, evento.Amount, context.CancellationToken);

            _logger.LogInformation(
                "Doação {DonationId} notificada com sucesso para campanha {CampaignId}.",
                evento.DonationId, evento.CampaignId);
        }
    }
}
