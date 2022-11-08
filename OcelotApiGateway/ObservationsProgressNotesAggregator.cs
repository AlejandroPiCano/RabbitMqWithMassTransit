using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using OcelotApiGateway.DTOs;
using System.Net;

namespace OcelotApiGateway
{
    public class ObservationsProgressNotesAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var observationsStr = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var progressNotesStr = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync(); ;

            var observations = JsonConvert.DeserializeObject<List<ObservationDTO>>(observationsStr);
            var progressNotes = JsonConvert.DeserializeObject<List<ProgressNoteDTO>>(progressNotesStr);

            foreach (var observation in observations)
            {
                observation.ProgressNotes = new List<ProgressNoteDTO>();
                observation.ProgressNotes.AddRange(progressNotes.Where(pn => pn.ObservationId == observation.Id));
            }

            observationsStr = JsonConvert.SerializeObject(observations);

            var stringContent = new StringContent(observationsStr)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");


        }
    }
}
