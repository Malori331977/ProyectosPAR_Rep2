using BlazorBootstrap;


namespace PortalNomina.Data.Interfaces
{
    public interface IGridExtensions
    {
        public Task<IEnumerable<FilterOperatorInfo>> GridFiltersTranslationProvider();
    }
}
