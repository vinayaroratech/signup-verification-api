using VA.Identity.Application.Common.Interfaces;
using System;

namespace VA.Identity.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
