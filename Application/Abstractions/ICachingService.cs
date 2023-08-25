namespace Application.Abstrations
{
	public interface ICachingService
	{
		Task<T?> GetDataAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;

		void RemoveRecordsByKeyPattern(string pattern, CancellationToken cancellationToken = default);

		//void RemoveRecord(string key);

		Task SetDataAsync<T>(string key, T data, TimeSpan? absoluteExpTime = null, TimeSpan? unusedExpTime = null, CancellationToken cancellationToken = default) where T : class;
	}
}