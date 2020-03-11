using System;
using System.Collections.Generic;
using System.Text;

namespace LoLBoosting.Contracts
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
