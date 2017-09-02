using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    interface IGeolocationService
    {
        Position GetCurrentPosition();
        double GetDistanceToPosition(Position destination);
    }
}
