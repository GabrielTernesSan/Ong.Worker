namespace Ong.Application.Worker.Clients
{
    public interface ICampaignsApiClient
    {
        Task NotifyDonationReceivedAsync(Guid campaignId, decimal amount, CancellationToken cancellationToken = default);
    }
}
