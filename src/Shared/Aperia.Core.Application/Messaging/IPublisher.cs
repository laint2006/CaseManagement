using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aperia.Core.Domain.Entities;

namespace Aperia.Core.Application.Messaging
{
    public interface IPublisher
    {
        Task PublishAsync(OutboxMessage message);
    }
}