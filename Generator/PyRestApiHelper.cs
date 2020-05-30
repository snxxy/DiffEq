using System;
using System.Threading.Tasks;
using DiffEq.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DiffEq
{
    sealed class PyRestApiHelper
    {
        public async Task<PyExpressionJson> ScrambleExpression(string expression, int maxVar)
        {
            var client = new RestClient("http://127.0.0.1:5000/");
            var expr = new PyExpressionJson();
            expr.Expression = expression;
            expr.MaxVar = maxVar;
            var jsonExpr = JsonConvert.SerializeObject(expr);
            var request = new RestRequest("scramble", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonExpr);

            var response = await client.ExecuteAsync(request);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return JsonConvert.DeserializeObject<PyExpressionJson>(response.Content);
            }

            return null;
        }

        public async Task<PyEquationJson> SolveEquation(string left, string right)
        {
            var client = new RestClient("http://127.0.0.1:5000/");
            var equat = new PyEquationJson();
            equat.LeftSide = left;
            equat.RightSide = right;

            var jsonEquat = JsonConvert.SerializeObject(equat);
            var request = new RestRequest("solve", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonEquat);

            var response = await client.ExecuteAsync(request);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return JsonConvert.DeserializeObject<PyEquationJson>(response.Content);
            }

            return null;
        }
    }
}
