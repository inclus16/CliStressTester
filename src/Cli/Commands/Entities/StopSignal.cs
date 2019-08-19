using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.src.Cli.Commands.Entities
{
    enum StopSignal
    {
        BadRequest=1,
        InternalServerError=2,
        BadGateway=3,
        Manual=4
    }
}
