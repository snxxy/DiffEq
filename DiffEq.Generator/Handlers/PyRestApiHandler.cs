using DiffEq.Generator.Models.JSON;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace DiffEq.Generator.Handlers
{
    sealed class PyRestApiHandler
    {
        public async Task<PyExpressionJson> ScrambleExpression(string expression, int maxVar)
        {
            var client = new RestClient("http://127.0.0.1:5050/");
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

        public async Task<PyEquationJson> SolveEquation(PyEquationJson equation)
        {
            var client = new RestClient("http://127.0.0.1:5050/");
            var jsonEquationString = JsonConvert.SerializeObject(equation);
            var request = new RestRequest("solve", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonEquationString);

            var response = await client.ExecuteAsync(request);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return JsonConvert.DeserializeObject<PyEquationJson>(response.Content);
            }
            return null;
        }
    }
}
