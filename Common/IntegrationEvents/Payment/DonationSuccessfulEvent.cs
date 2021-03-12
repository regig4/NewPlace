﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.IntegrationEvents.Payment
{
    public class DonationSuccessfulEvent : IntegrationEvent
    {
        public DonationSuccessfulEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}