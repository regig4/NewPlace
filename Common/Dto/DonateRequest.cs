namespace Common.Dto
{
    public record DonateRequest(string UserId, ulong Amount, string Currency);
}
