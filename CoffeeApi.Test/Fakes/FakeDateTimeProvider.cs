using CoffeeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeApi.Test.Fakes
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
