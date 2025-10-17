using DevTest.Service.Models;
using DevTest.Services.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DevTest.Service.Services
{
    public class DebtApiService
    {
        private readonly RestClient _client;
        private readonly string _apiKey;

        public DebtApiService(IConfiguration config)
        {
            var baseUrl = config["DebtApi:BaseUrl"];
            _apiKey = config["DebtApi:ApiKey"];
            _client = new RestClient(baseUrl);
        }

        public async Task<LoginModel> ValidateLogin(string email, string password)
        {
            var request = new RestRequest("/api/webuser/validate", Method.Post);
            request.AddHeader("X-API-Key", _apiKey);
            request.AddJsonBody(new { email, password });

            var response = await _client.ExecuteAsync(request);

            return new LoginModel()
            {
                isSuccessful = response.IsSuccessful,
                Email = email,
                AccountId = "123" //get from response
            };
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var request = new RestRequest("/api/webuser/register", Method.Post);
            request.AddHeader("X-API-Key", _apiKey);
            request.AddBody(model);

            var response = await _client.ExecuteAsync(request);

            //will consider conflict as true, because the user exists.
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return true;
            }

            return response.IsSuccessful;
        }

        public async Task<AccountDetails> GetAccount(string accountId)
        {
            var request = new RestRequest("/api/account/"+ accountId);
            request.AddHeader("X-API-Key", _apiKey);
            request.Method = Method.Get;

            var response = await _client.ExecuteAsync<AccountDetails>(request);
            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<AccountTransactions> GetAccountTransactions(string accountId, int? pageNumber = 1, int? pageSize = 20)
        {
            var request = new RestRequest("/api/account/"+ accountId +"/transactions?pageNumber="+ pageNumber +"&pageSize="+ pageSize);
            request.AddHeader("X-API-Key", _apiKey);
            request.Method = Method.Get;

            var response = await _client.ExecuteAsync<AccountTransactions>(request);
            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<bool> UpdateAddress(string accountId, UpdateAddressViewModel model)
        {
            var request = new RestRequest($"/api/account/{accountId}/address", Method.Put);
            request.AddHeader("X-API-Key", _apiKey);
            request.AddJsonBody(model);

            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        public async Task<bool> UpdateEmail(string accountId, UpdateEmailViewModel model)
        {
            var request = new RestRequest($"/api/account/{accountId}/email", Method.Put);
            request.AddHeader("X-API-Key", _apiKey);
            request.AddJsonBody(new { model });

            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
    }
}
