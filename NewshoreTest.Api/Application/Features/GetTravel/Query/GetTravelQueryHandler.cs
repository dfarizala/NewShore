using MediatR;
using NewshoreTest.Api.Domain.Entities;
using NewshoreTest.Api.Domain.Interfaces;

namespace NewshoreTest.Api.Application.Features.GetTravel.Query
{
    public class GetTravelQueryHandler : IRequestHandler<GetTravelQuery, GetTravelResponse>
    {
        private readonly IRepository<Flight> _FLight;
        private readonly IRepository<Aircraft> _Aircraft;
        private readonly IRepository<Journey> _Journey;
        private readonly IRepository<Transport> _Transport;
        private readonly IRepository<Carrier> _Carrier;
        private readonly IConfiguration _Configuration;

        public GetTravelQueryHandler(IRepository<Flight> fLight,
                                     IRepository<Aircraft> aircraft,
                                     IRepository<Journey> journey,
                                     IRepository<Transport> transport,
                                     IRepository<Carrier> carrier,
                                     IConfiguration configuration)
        {
            _FLight = fLight;
            _Aircraft = aircraft;
            _Journey = journey;
            _Transport = transport;
            _Carrier = carrier;
            _Configuration = configuration;
        }

        public async Task<GetTravelResponse> Handle(GetTravelQuery request, CancellationToken cancellationToken)
        {
            GetTravelResponse Result = new();
            try
            {
                var CarrierList = (await _Carrier.GetAsync()).ToList();

                int ConvertionRate = 1;
                int FligthCount = 0;
                if(!string.IsNullOrEmpty(request.Currency))
                {
                    ConvertionRate = _Configuration.GetValue<int>("Currency:" + request.Currency + "");
                }
                decimal TotalPrice = 0;
                
                Flight OriginFlight = new();
                Flight DestinationFligth = new();

                List<FlightList> FLights = new();

                do
                {
                    if(FligthCount == 0)
                    {
                        FlightList ListObject = new();
                        TransportElement TransportObject = new();
                        OriginFlight = (await _FLight.GetAsync(x => x.Origin == request.Origin)).FirstOrDefault();
                        if (OriginFlight == null) throw new Exception("OriginFlight fligh can't be found!");
                        var TransportElement = (await _Transport.GetAsync(x => x.Id == OriginFlight.TransportId)).FirstOrDefault();
                        if (TransportElement == null) throw new Exception("No transport information for location " + OriginFlight.Origin);
                        ListObject.Origin = OriginFlight.Origin;
                        ListObject.Destination = OriginFlight.Destination;
                        ListObject.Price = OriginFlight.Price;
                        TransportObject.FlightCarrier = CarrierList.Where(x => x.Id == TransportElement.FlightCarrier).FirstOrDefault().CarrierCode;
                        TransportObject.FLightNumber = TransportElement.FlightNumber;
                        ListObject.Transport = TransportObject;
                        TotalPrice = TotalPrice + OriginFlight.Price;
                        FligthCount = FligthCount + 1;

                        FLights.Add(ListObject);
                    }
                    else
                    {
                        if (OriginFlight.Destination == request.Destination) break;

                        FlightList ListObject = new();
                        TransportElement TransportObject = new();

                        var LastFlight = DestinationFligth.Origin;

                        if(string.IsNullOrEmpty(DestinationFligth.Origin))
                        {
                            DestinationFligth = (await _FLight.GetAsync(x => x.Origin == OriginFlight.Destination)).FirstOrDefault();
                        }
                        else
                        {
                            DestinationFligth = (await _FLight.GetAsync(x => x.Origin == DestinationFligth.Destination)).FirstOrDefault();
                        }

                        if (DestinationFligth == null) throw new Exception("OriginFlight fligh can't be found!");
                        var TransportElement = (await _Transport.GetAsync(x => x.Id == DestinationFligth.TransportId)).FirstOrDefault();
                        if (TransportElement == null) throw new Exception("No transport information for location " + OriginFlight.Origin);
                        ListObject.Origin = DestinationFligth.Origin;
                        ListObject.Destination = DestinationFligth.Destination;
                        ListObject.Price = DestinationFligth.Price;
                        TransportObject.FlightCarrier = CarrierList.Where(x => x.Id == TransportElement.FlightCarrier).FirstOrDefault().CarrierCode;
                        TransportObject.FLightNumber = TransportElement.FlightNumber;
                        ListObject.Transport = TransportObject;
                        TotalPrice = TotalPrice + DestinationFligth.Price;
                        FligthCount = FligthCount + 1;

                        if (DestinationFligth.Destination == LastFlight) break;

                        FLights.Add(ListObject);

                    }
                } while (DestinationFligth.Destination != request.Destination);

                if (FLights.LastOrDefault().Destination != request.Destination)
                    throw new Exception("No route available!");

                JourneyElement ResponseElement = new JourneyElement
                {
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Flights = FLights,
                    Price = Convert.ToDecimal(TotalPrice * ConvertionRate)
                };

                Result.Status = "OK";
                Result.Message = "Successfully";
                Result.Journey = ResponseElement;
            }
            catch(Exception ex) 
            {
                Result.Status = "FAIL";
                Result.Message = ex.Message;
                Result.Journey = null;
            }
            return Result;
        }
    }
}
