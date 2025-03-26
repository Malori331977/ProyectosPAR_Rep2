using BlazorBootstrap;


namespace ProyectoPAR.Data.Interfaces
{
    public interface IGridExtensions
    {
        public Task<IEnumerable<FilterOperatorInfo>> GridFiltersTranslationProvider();
    }
}
