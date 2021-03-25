using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Application.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
