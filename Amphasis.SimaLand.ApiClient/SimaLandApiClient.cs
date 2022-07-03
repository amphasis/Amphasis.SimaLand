using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amphasis.SimaLand.JsonApi;
using Amphasis.SimaLand.Models;

namespace Amphasis.SimaLand
{
	/// <summary>
	/// Клиент https://www.sima-land.ru/api/v5
	/// </summary>
	public class SimaLandApiClient
	{
		private const string BaseUriString = "https://www.sima-land.ru/api/v5/";

		private readonly HttpClient _httpClient;

		public SimaLandApiClient() : this(new HttpClient())
		{
		}

		public SimaLandApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(BaseUriString, UriKind.Absolute);
		}

		public async Task<string> GetAccessTokenAsync(string email, string password)
		{
			const string signinUriString = "signin";
			var uri = new Uri(signinUriString, UriKind.Relative);

			var request = new SignInRequest
			{
				Email = email,
				Password = password,
				Regulation = true
			};

			using (var httpResponseMessage = await _httpClient.PostJsonAsync(uri, request))
			{
				await httpResponseMessage.EnsureSuccessAsync();
				var headers = httpResponseMessage.Headers;
				const string authorizationHeaderName = "Authorization";
				var authorizationHeaderValuesEnumerable = headers.GetValues(authorizationHeaderName);
				var tokenWithScheme = authorizationHeaderValuesEnumerable.First();
				var delimiterIndex = tokenWithScheme.IndexOf(' ');
				var token = tokenWithScheme.Substring(delimiterIndex + 1);

				return token;
			}
		}

		public void SetAccessToken(string accessToken)
		{
			const string scheme = "Bearer";
			var authenticationHeader = new AuthenticationHeaderValue(scheme, parameter: accessToken);
			_httpClient.DefaultRequestHeaders.Authorization = authenticationHeader;
		}

		public async Task<ItemResponse> GetItemAsync(int itemId)
		{
			var getItemUriString = $"item/{itemId}";
			var uri = new Uri(getItemUriString, UriKind.Relative);

			using (var httpResponseMessage = await _httpClient.GetAsync(uri))
			{
				await httpResponseMessage.EnsureSuccessAsync();
				var item = await httpResponseMessage.ReadJsonAsync<ItemResponse>();

				return item;
			}
		}

		public async Task<IList<ItemResponse>> GetItemsAsync(int pageId)
		{
			var getItemUriString = $"item?p={pageId}";
			var uri = new Uri(getItemUriString, UriKind.Relative);

			using (var httpResponseMessage = await _httpClient.GetAsync(uri))
			{
				await httpResponseMessage.EnsureSuccessAsync();
				var item = await httpResponseMessage.ReadJsonAsync<IList<ItemResponse>>();

				return item;
			}
		}
	}
}