﻿using Newtonsoft.Json;
using Stripe.Client.Sdk.Attributes;
using Stripe.Client.Sdk.Extensions;
using System;

namespace Stripe.Client.Sdk.Models.Filters
{
    public class ChargeListFilter : ListFilter
    {
        [JsonIgnore]
        public DateTime? CreatedDateTime { get; set; }

        [JsonIgnore]
        public DateFilter CreatedFilter { get; set; }

        [ChildModel]
        public object Created
        {
            get { return CreatedDateTime.HasValue ? (object)CreatedDateTime.Value.ToEpoch() : CreatedFilter; }
        }

        public string Customer { get; set; }

        [ChildModel]
        public SourceObjectFilter Source { get; set; }
    }
}