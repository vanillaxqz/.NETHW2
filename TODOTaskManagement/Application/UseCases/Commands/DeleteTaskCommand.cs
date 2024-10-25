﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands
{
    public class DeleteTaskCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
