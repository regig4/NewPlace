using System;
using System.Collections.Generic;
using System.Linq;
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

        [Inject]
        NavigationManager NavigationManager { get; set; }

        HubConnection _hubConnection;
        List<AdvertisementDetailsRepresentation> _recommendations;
        object lockObj = new object();

        private Map _map;
        private static readonly LatLng _center = new LatLng(50.06409489164344, 19.928898998922403);
        private MapOptions mapOptions = new MapOptions()
        {
            DivId = "mapId",
            Center = _center,
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

        public Marker UserMarker { get; set; }

        private List<(AdvertisementDetailsRepresentation recommendation, Marker marker)> _recommendationsMarkers = new();

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

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {

            //_hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44347/recommendationHub").Build();
            //_hubConnection.On<AdvertisementRepresentation>("ReceiveRecommendation", RecommendationReceivedHandler);
            //await _hubConnection.StartAsync();
            //await _hubConnection.SendAsync("RecommendBasedOnCurrentLocation", Guid.Empty, 0, 0);

            await Task.Delay(3000).ContinueWith(async t =>
                await _map.OnClick(async (mouseEvent) =>
                {
                    _location.Longitude = mouseEvent.LatLng.Lng;
                    _location.Latitude = mouseEvent.LatLng.Lat;
                    UserMarker?.Remove();
                    UserMarker = await MarkerFactory.CreateAndAddToMap(
                                new LatLng(_location.Latitude, _location.Longitude), _map);

                    foreach (var (recommendation, marker) in _recommendationsMarkers)
                        await marker.Remove();

                    _recommendationsMarkers.Clear();

                    Loading = true;

                    _recommendations = new List<AdvertisementDetailsRepresentation>(
                        await Api.LocationAsync(_location.Latitude, _location.Longitude));

                    foreach (var recommendation in _recommendations)
                    {
                        var marker = await MarkerFactory.CreateAndAddToMap(
                            new LatLng(recommendation.Resource.Latitude, recommendation.Resource.Longitude), _map);
                        var popup = await marker.BindPopup(recommendation.Resource.Title);
                        await marker.OnDblClick(args => NavigateToAdvertisement(recommendation.Resource.Id));
                        _recommendationsMarkers.Add((recommendation, marker));
                    }


                    Loading = false;

                    StateHasChanged();
                }));
        }

        private async Task NavigateToAdvertisement(int? id)
        {
            NavigationManager.NavigateTo($"/details/{id}");
            await Task.CompletedTask;
        }

        private async Task OpenPopup(int id)
        {
            foreach (var (recommendation, marker) in _recommendationsMarkers)
                if (recommendation.Resource.Id == id)
                    await marker.OpenPopup(new LatLng(recommendation.Resource.Latitude, recommendation.Resource.Longitude));
        }

        private async Task ClosePopup(int id)
        {
            foreach (var (recommendation, marker) in _recommendationsMarkers)
                if (recommendation.Resource.Id == id)
                    await marker.ClosePopup();
        }
    }
}
