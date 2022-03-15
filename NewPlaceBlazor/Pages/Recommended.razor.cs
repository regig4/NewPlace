using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using NewPlaceBlazor.Models;

namespace NewPlaceBlazor.Pages
{
    public partial class Recommended
    {
        [Inject]
        public ApiClient Api { get; set; }

        HubConnection _hubConnection;
        List<AdvertisementDetailsRepresentation> _recommendations;
        object lockObj = new object();

        private Map _map;
        private MapOptions mapOptions = new MapOptions()
        {
            DivId = "mapId",
            Center = new LatLng(50.06409489164344, 19.928898998922403),
            Zoom = 13,
            UrlTileLayer = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
            SubOptions = new MapSubOptions()
            {
                Attribution = "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
                TileSize = 512,
                ZoomOffset = -1,
                MaxZoom = 19,
            }
        };

        public Marker Marker { get; set; }

        private List<Marker> _recommendationsMarkers = new List<Marker>();

        readonly Location _location = new();

        public bool Loading { get; set; }

        private void RecommendationReceivedHandler(AdvertisementDetailsRepresentation recommendation)
        {
            lock (lockObj)
            {
                if (_recommendations is null)
                    _recommendations = new List<AdvertisementDetailsRepresentation>();
                _recommendations.Add(recommendation);
                StateHasChanged();
            }
        }

        protected async Task ClickHandler()
        {
            await _map.OnClick(async (mouseEvent) =>
            {
                _location.Longitude = mouseEvent.LatLng.Lng;
                _location.Latitude = mouseEvent.LatLng.Lat;
                Marker?.Remove();
                Marker = await MarkerFactory.CreateAndAddToMap(new LatLng(_location.Latitude, _location.Longitude), _map);

                foreach (var r in _recommendationsMarkers)
                    r.Remove();

                _recommendationsMarkers.Clear();

                Loading = true;

                _recommendations = new List<AdvertisementDetailsRepresentation>(await Api.LocationAsync(_location.Latitude, _location.Longitude));

                foreach(var recommendation in _recommendations)
                    _recommendationsMarkers.Add(await MarkerFactory.CreateAndAddToMap(
                        new LatLng(recommendation.Resource.Latitude, recommendation.Resource.Longitude), _map));

                Loading = false;

                StateHasChanged();
            });
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {

            //_hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44347/recommendationHub").Build();
            //_hubConnection.On<AdvertisementRepresentation>("ReceiveRecommendation", RecommendationReceivedHandler);
            //await _hubConnection.StartAsync();
            //await _hubConnection.SendAsync("RecommendBasedOnCurrentLocation", Guid.Empty, 0, 0);

            
        }

    }
}
