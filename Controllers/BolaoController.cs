using MediatR;
using Microsoft.AspNetCore.Mvc;
using PLE_Ranking.Domain.Entities;
using PLE_Ranking.Domain.Interfaces;
using PLE_Ranking.Domain.Request;
using PLE_Ranking.Domain.Response;

namespace PLE_Rankings.Controllers
{
    [ApiController]
    [Route("WWE")]
    public class BolaoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBolaoService _bolaoService;

        public BolaoController(IMediator mediator, IBolaoService bolaoService)
        {
            _mediator = mediator;
            _bolaoService = bolaoService;
        }

        [HttpPost]
        [Route("")]
        public async Task<CreateCustumerResponse> Create([FromBody] CreateCustumerRequest request)
        {
            
            return await _mediator.Send(request);
        }
        [HttpGet]
        [Route("GetResults")]
        public IActionResult GetResults()
        {
            const string PLE = "MITB";
            List<Bolao> bets = _bolaoService.GetBets($@"L:\Estudos\Boloes\Bolao{PLE}_2024.csv");
            List<BetsPLE> betsPle = _bolaoService.GetExpectedWinners($@"L:\Estudos\Boloes\{PLE}_2024.csv");

            Dictionary<string, string> resultsPLE = _bolaoService.GetResultsPLE($@"L:\Estudos\Boloes\Results{PLE}_2024.csv");
            List<Bolao> betResults = _bolaoService.CalculateBolao(resultsPLE, bets);
            string finalMessage = $"RESULTADOS BOLÃO {PLE.ToUpper()} \n";
            int counter = 1;
            foreach (Bolao betResult in betResults)
            {
                switch (counter)
                { 
                    case 1: finalMessage += $"🥇 {counter++}º -> {betResult.Name} - *{betResult.Points} PONTOS*  \n";
                        break;
                    case 2: finalMessage += $"🥈 {counter++}º -> {betResult.Name} - *{betResult.Points} PONTOS*  \n";
                        break;
                    case 3: finalMessage += $"🥉 {counter++}º -> {betResult.Name} - *{betResult.Points} PONTOS*  \n";
                        break;
                    default: finalMessage += $"{counter++}º -> {betResult.Name} - *{betResult.Points} PONTOS* \n";
                        break;
                }
            }

            return Ok(finalMessage);
        }
    }
}
