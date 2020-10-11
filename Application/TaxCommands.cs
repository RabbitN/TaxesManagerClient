using Domain.Dto;
using Domain.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Application
{
    public class TaxCommands
    {
        private static RestClient _client;
        public TaxCommands(RestClient client)
        {
            _client = client;
        }

        public void GetAllTaxes()
        {
            Console.WriteLine("COMMAND: Get all taxes");
            RestRequest request = new RestRequest("", Method.GET);
            IRestResponse<List<Tax>> response = _client.Execute<List<Tax>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var taxes = JsonConvert.DeserializeObject<List<Tax>>(response.Content);
                if (taxes.Count != 0)
                {
                    for (int i = 0; i < taxes.Count; i++)
                    {
                        Console.WriteLine("{0}, {1}, {2}, {3}",
                            taxes[i].Municipality, taxes[i].StartDate.ToString("yyyy-MM-dd"), taxes[i].EndDate.ToString("yyyy-MM-dd"), taxes[i].TaxAmount);
                    }
                }
                else
                {
                    Console.WriteLine("No taxes found");
                }
            }
            else
            {
                Console.WriteLine(response.Content);
            }
            Console.WriteLine();
        }

        public void GetTax(string municipality, string startDate, string endDate)
        {
            Console.WriteLine("COMMAND: Get tax");
            string resource = municipality + "/" + startDate + "/" + endDate;
            RestRequest request = new RestRequest(resource, Method.GET);
            IRestResponse<Tax> response = _client.Execute<Tax>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var tax = JsonConvert.DeserializeObject<Tax>(response.Content);
                Console.WriteLine("{0}, {1}, {2}, {3}",
                        tax.Municipality, tax.StartDate.ToString("yyyy-MM-dd"), tax.EndDate.ToString("yyyy-MM-dd"), tax.TaxAmount);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
            }
            Console.WriteLine();
        }

        public void PostTaxYearly(TaxDto taxDto)
        {
            Console.WriteLine("COMMAND: Post tax yearly");
            RestRequest request = new RestRequest("Yearly", Method.POST);
            request.AddJsonBody(taxDto);
            var response = _client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine();
        }

        public void PostTaxMonthly(TaxDto taxDto)
        {
            Console.WriteLine("COMMAND: Post tax monthly");
            RestRequest request = new RestRequest("Monthly", Method.POST);
            request.AddJsonBody(taxDto);
            var response = _client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine();
        }

        public void PostTaxWeekly(TaxDto taxDto)
        {
            Console.WriteLine("COMMAND: Post tax weekly");
            RestRequest request = new RestRequest("Weekly", Method.POST);
            request.AddJsonBody(taxDto);
            var response = _client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine();
        }

        public void PostTaxDaily(TaxDto taxDto)
        {
            Console.WriteLine("COMMAND: Post tax daily");
            RestRequest request = new RestRequest("Daily", Method.POST);
            request.AddJsonBody(taxDto);
            var response = _client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine();
        }

        public void UpdateTax(string municipality, string startDate, string endDate, double taxAmount)
        {
            Console.WriteLine("COMMAND: Update tax");
            string resource = municipality + "/" + startDate + "/" + endDate;
            RestRequest request = new RestRequest(resource, Method.PUT);
            request.AddJsonBody(taxAmount);
            var response = _client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine();
        }

        public void DeleteTax(string municipality, string startDate, string endDate)
        {
            Console.WriteLine("COMMAND: Delete tax");
            string resource = municipality + "/" + startDate + "/" + endDate;
            RestRequest request = new RestRequest(resource, Method.DELETE);
            var response = _client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine();
        }

        public void GetTaxAmount(string municipality, string date)
        {
            Console.WriteLine("COMMAND: Get tax amount");
            RestRequest request = new RestRequest("Amount", Method.GET);
            request.AddParameter("municipality", municipality);
            request.AddParameter("date", date);
            var response = _client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine();
        }

        public void ImportTaxesFromFile(string path)
        {
            Console.WriteLine("COMMAND: Import taxes");
            try
            {
                RestRequest request = new RestRequest("Import", Method.POST) { RequestFormat = DataFormat.Json, AlwaysMultipartFormData = true };
                request.AddFile("file", path);
                var response = _client.Execute(request);
                Console.WriteLine(response.Content);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File path not found!");
            }
            Console.WriteLine();
        }
    }
}
