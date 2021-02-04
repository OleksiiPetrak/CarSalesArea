namespace CarSalesArea.Core.Infrastructure
{
    public interface IEtaggable
    {
        string GetEtag();
    }
}
