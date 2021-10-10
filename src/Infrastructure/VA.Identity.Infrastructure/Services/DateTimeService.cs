using System;
using VA.Identity.Application.Common.Interfaces;

namespace VA.Identity.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
