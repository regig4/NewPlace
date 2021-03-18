﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.IntegrationEvents.Payment
{
    public record DonationSuccessfulEvent : IntegrationEvent
    {
        public DonationSuccessfulEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
