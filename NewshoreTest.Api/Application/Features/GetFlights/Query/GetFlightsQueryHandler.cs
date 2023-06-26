using MediatR;
using Microsoft.IdentityModel.Tokens;
using NewshoreTest.Api.Domain.Entities;
using NewshoreTest.Api.Domain.Interfaces;

namespace NewshoreTest.Api.Application.Features.GetFlights.Query
{
    public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, List<GetFlightsResponse>>
    {
        private readonly IRepository<Flight> _FLight;
        private readonly IRepository<Aircraft> _Aircraft;
        private readonly IRepository<Journey> _Journey;
        private readonly IRepository<Transport> _Transport;
        private readonly IRepository<Carrier> _Carrier;

        public GetFlightsQueryHandler(IRepository<Flight> fLight, 
                                      IRepository<Aircraft> aircraft, 
                                      IRepository<Journey> journey, 
                                      IRepository<Transport> transport, 
                                      IRepository<Carrier> carrier)
        {
            _FLight = fLight;
            _Aircraft = aircraft;
            _Journey = journey;
            _Transport = transport;
            _Carrier = carrier;
        }

        public async Task<List<GetFlightsResponse>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
        {
            List<GetFlightsResponse> Result = new();
            try
            {
                var Journey = (await _Journey.GetAsync()).ToList();
                var Carrier = (await _Carrier.GetAsync()).ToList();

                foreach (var j in Journey)
                {
                    GetFlightsResponse ListFlight = new();
                    var Flight = (await _FLight.GetAsync(x => x.Id == j.FlightID)).FirstOrDefault();
                    var Transport = (await _Transport.GetAsync(x => x.Id == Flight.TransportId)).FirstOrDefault();
                    var FlightCarrier = Carrier.Where(x => x.Id == Transport.FlightCarrier).FirstOrDefault();

                    ListFlight.FLightNomber = Transport.FlightNumber;
                    ListFlight.DepartureStation = Flight.Origin;
                    ListFlight.ArrivalStation = Flight.Destination;
                    ListFlight.FlightCarrier = FlightCarrier.CarrierCode + " - " + FlightCarrier.CarrierName;
                    ListFlight.Price = Flight.Price;
                    ListFlight.FlightDate = j.FlightDate;

                    Result.Add(ListFlight);
                }

                return Result;

            }
            catch(Exception ex)
            {
                return null;
            }
            return Result;
        }
    }
}
