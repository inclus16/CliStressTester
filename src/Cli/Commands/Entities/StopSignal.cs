﻿namespace StressCLI.src.Cli.Commands.Entities
{
    internal enum StopSignal
    {
        BadRequest = 1,
        InternalServerError = 2,
        BadGateway = 3,
        Manual = 4,
        TooManyRequests = 5
    }
}
