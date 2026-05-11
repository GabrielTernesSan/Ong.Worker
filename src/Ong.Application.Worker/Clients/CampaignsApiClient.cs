using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Ong.Application.Worker.Clients
{
    public class CampaignsApiClient : ICampaignsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CampaignsApiClient> _logger;

        public CampaignsApiClient(HttpClient httpClient, ILogger<CampaignsApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task NotifyDonationReceivedAsync(Guid campaignId, decimal amount, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsJsonAsync(
                $"/campaigns/{campaignId}/donation-received",
                new { amount },
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError(
                    "Falha ao notificar doação para campanha {CampaignId}. Status: {StatusCode}. Resposta: {Body}",
                    campaignId, response.StatusCode, body);
                throw new HttpRequestException(
                    $"Falha ao notificar campanha {campaignId}. Status: {(int)response.StatusCode}");
            }
        }
    }
}
